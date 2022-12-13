using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _width = 16;
    [SerializeField] int _height = 16;
    [SerializeField] int _mineCount;
    public int Width => _width;
    public int Height => _height;
    private Board _board;
    private Cell[,] _state { get; }

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
        cells = new Cell[_width,_height];
        GenerateCell(cells);
        GenerateMines(cells);
        _board.Draw(cells);
    }

    private void GenerateCell(Cell[,] cells)
    {
        for(int i = 0;i < _width; i++)
        {
            for(int j = 0;j < _height; j++)
            {
                var cell = new Cell();
                cell.Position = new(i, j);
                cells[i, j] = cell;
            }
        }
    }
    private void GenerateMines(Cell[,] cells)
    {
        for(int i = 0;i < _mineCount; i++)
        {
            var x = Random.Range(0, Width);
            var y = Random.Range(0, Height);
            while (cells[x,y].CellType == Cell.Type.mine)
            {
                x++;
                if(x >= Width)
                {
                    x = 0;
                    y++;
                    if(y >= Height)
                    {
                        y = 0;
                    }
                }
            }
            cells[x, y].CellType = Cell.Type.mine;
            cells[x, y].Revealed = true;
        }
    }
}
