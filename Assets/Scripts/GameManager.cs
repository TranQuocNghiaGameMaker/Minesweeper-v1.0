using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int width = 16;
    [SerializeField] int height = 16;
    public int Width => width;
    public int Height => height;
    private Board _board;
    private Cell[,] _state;

    // Start is called before the first frame update
    private void Awake()
    {
        _board = GetComponentInChildren<Board>();
    }
    void Start()
    {
        NewGame(_state);
    }

    private void NewGame(Cell[,] cells)
    {
        cells = new Cell[width,height];
        GenerateCell(cells);
        _board.Draw(cells);
    }

    private void GenerateCell(Cell[,] cells)
    {
        for(int i = 0;i < width; i++)
        {
            for(int j = 0;j < height; j++)
            {
                var cell = new Cell();
                cell.Position = new(i, j);
                cells[i, j] = cell;
            }
        }
    }
}
