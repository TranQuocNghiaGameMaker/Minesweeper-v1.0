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
        GenerateNumbers(cells);
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
        }
    }

    private void GenerateNumbers(Cell[,] cells)
    {
        for(int i = 0;i < Width; i++)
        {
            for(int j = 0;j < Height; j++)
            {
                var cell = cells[i,j];
                if(cell.CellType == Cell.Type.mine)
                {
                    continue;
                }
                cell.Number = CountMine(cells,i,j);
                if(cell.Number > 0)
                {
                    cell.CellType = Cell.Type.number;
                    cells[i, j] = cell;
                    cells[i, j].Revealed = true;
                }
            }
        }
    }

    private int CountMine(Cell[,] cells,int cellx, int celly)
    {
        int count = 0;
        for (int adjacentX = -1; adjacentX < 1; adjacentX++)
        {
            for (int adjacentY = -1; adjacentY < 1; adjacentY++)
            {
                if (adjacentX == 0 && adjacentY == 0) continue;
                var x = cellx + adjacentX;
                var y = celly + adjacentY;
                if(x < 0 || x > Width || y < 0 || y > Height) continue;
                if (cells[x,y].CellType == Cell.Type.mine)
                {
                    count++;
                }
            }
        }
        return count;
    }
}
