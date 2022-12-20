using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    }
    void Start()
    {
        initialBoard.NewGame();
    }

    // Update is called once per frame
    void Update()
    {
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
