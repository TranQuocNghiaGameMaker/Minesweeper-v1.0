using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private InitialBoardSystem initialBoard;
    private VisualBoardSystem visualBoard;
    private UpdateBoardSystem updateBoard;
    // Start is called before the first frame update
    void Awake()
    {
        initialBoard = GetComponentInChildren<InitialBoardSystem>();
        visualBoard = GetComponentInChildren<VisualBoardSystem>();
        updateBoard = GetComponentInChildren<UpdateBoardSystem>();
        updateBoard.InitialSystem = initialBoard;
        updateBoard.VisualSystem = visualBoard;
    }
    void Start()
    {
        initialBoard.NewGame(visualBoard);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = visualBoard.Tilemap.WorldToCell(worldPosition);
            updateBoard.Flag(cellPosition);
        }
    }
}
