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
        updateBoard.GetGameEvent(gameCondition._explodedEvent,gameCondition._winEvent);
    }
    void Start()
    {
        gameCondition._newGameEvent.Raise();
        visualBoard.Draw(initialBoard.State);
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputSystem.RestartButton)
        {
            gameCondition._newGameEvent.Raise();
            visualBoard.Draw(initialBoard.State);
        }
        if (gameCondition.GameOver) return;
        if (_inputSystem.RightClick)
        {
            cellPosition = Ultilites.GetTileAtWorldPoint(Camera.main, visualBoard.Tilemap);
            updateBoard.Flag(cellPosition);
            visualBoard.Draw(updateBoard._state);
        }
        if (_inputSystem.Click)
        {
            cellPosition = Ultilites.GetTileAtWorldPoint(Camera.main, visualBoard.Tilemap);
            updateBoard.Reveal(cellPosition);
            visualBoard.Draw(updateBoard._state);
            updateBoard.CheckWinCondition();
        }
        if (_inputSystem.doubleClick)
        {
            //reveal all unknown cell around
        }
    }
}
