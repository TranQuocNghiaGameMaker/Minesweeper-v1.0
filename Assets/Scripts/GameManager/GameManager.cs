using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] InputSystemSO _inputSystem;
    private IInitialBoard initialBoard;
    private IVisualBoard visualBoard;
    private IUpdateBoard updateBoard;
    private GameConditionBase gameCondition;
    private Vector3Int cellPosition;
    void Awake()
    {
        initialBoard = GetComponentInChildren<IInitialBoard>();
        visualBoard = GetComponentInChildren<IVisualBoard>();
        updateBoard = GetComponentInChildren<IUpdateBoard>();
        gameCondition = GetComponentInChildren<GameConditionBase>();
        updateBoard.InitialSystem = initialBoard;
    }
    void Start()
    {
        NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputSystem.RestartButton)
        {
            NewGame();
        }
        if (updateBoard.GameOver) return;
        if (updateBoard.GameWin) return;
        if (_inputSystem.RightClick)
        {
            cellPosition = Ultilites.GetTileAtWorldPoint(Camera.main, visualBoard.Tilemap);
            updateBoard.Flag(cellPosition);
            visualBoard.Draw(updateBoard.State);
        }
        if (_inputSystem.Click)
        {
            cellPosition = Ultilites.GetTileAtWorldPoint(Camera.main, visualBoard.Tilemap);
            updateBoard.Reveal(cellPosition);
            CheckGameState();
            visualBoard.Draw(updateBoard.State);
        }
        if (_inputSystem.doubleClick)
        {
            cellPosition = Ultilites.GetTileAtWorldPoint(Camera.main, visualBoard.Tilemap);
            updateBoard.RevealNeighbor(cellPosition);
            CheckGameState();
            visualBoard.Draw(updateBoard.State);
        }

    }
    private void NewGame()
    {
        gameCondition._newGameEvent.Raise();
        visualBoard.Draw(initialBoard.State);
    }
    private void CheckGameState()
    {
        if (updateBoard.CheckWinCondition())
        {
            gameCondition._winEvent.Raise();
        }
        if (updateBoard.GameOver)
        {
            gameCondition._explodedEvent.Raise();
        }
    }
}
