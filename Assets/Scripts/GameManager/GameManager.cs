using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameEvent _explodedEvent;
    [SerializeField] GameEvent _winEvent;
    [SerializeField] GameEvent _newGameEvent;

    private InitialBoardSystem initialBoard;
    private VisualBoardSystem visualBoard;
    private UpdateBoardSystem updateBoard;
    private GameCondition gameCondition;
    private Vector3Int cellPosition;
    void Awake()
    {
        initialBoard = GetComponentInChildren<InitialBoardSystem>();
        visualBoard = GetComponentInChildren<VisualBoardSystem>();
        updateBoard = GetComponentInChildren<UpdateBoardSystem>();
        gameCondition = GetComponentInChildren<GameCondition>();
        updateBoard.InitialSystem = initialBoard;
        updateBoard.GetGameEvent(_explodedEvent, _winEvent);
    }
    void Start()
    {
        initialBoard.NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _newGameEvent.Raise();
        }
        if (gameCondition.GameOver) return;
        if (Input.GetMouseButtonDown(1))
        {
            cellPosition = Ultilites.GetTileAtWorldPoint(Camera.main, visualBoard.Tilemap);
            updateBoard.Flag(cellPosition);
        }
        if (Input.GetMouseButtonDown(0))
        {
            cellPosition = Ultilites.GetTileAtWorldPoint(Camera.main, visualBoard.Tilemap);
            updateBoard.Reveal(cellPosition);
            updateBoard.CheckWinCondition();
        }
    }
}
