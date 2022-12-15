using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class InitialBoardSystem : MonoBehaviour
{
    [SerializeField] int _width = 16;
    [SerializeField] int _height = 16;
    [SerializeField] int _mineCount;
    public int Width => _width;
    public int Height => _height;
    private Cell[,] _state;
    public Cell[,] State => _state;

    public void NewGame()
    {
        _state = new Cell[_width,_height];
        GenerateCell();
        GenerateMines();
        GenerateNumbers();
        VisualBoardSystem.ChangeBoardAction(_state);
    }

    

    private void GenerateCell()
    {
        for(int i = 0;i < _width; i++)
        {
            for(int j = 0;j < _height; j++)
            {
                var cell = new Cell() { CellType = Cell.Type.none};
                cell.Position = new(i, j);
                State[i, j] = cell;

            }
        }
    }
    private void GenerateMines()
    {
        for(int i = 0;i < _mineCount; i++)
        {
            var x = Random.Range(0, Width);
            var y = Random.Range(0, Height);
            while (State[x,y].CellType == Cell.Type.mine)
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
            State[x, y].CellType = Cell.Type.mine;
        }
    }

    private void GenerateNumbers()
    {
        for(int i = 0;i < Width; i++)
        {
            for(int j = 0;j < Height; j++)
            {
                var cell = State[i,j];
                if(cell.CellType == Cell.Type.mine)
                {
                    continue;
                }
                cell.Number = CountMine(i,j);
                if(cell.Number > 0)
                {
                    cell.CellType = Cell.Type.number;
                    State[i, j] = cell;
                }
            }
        }
    }

    private int CountMine(int cellx, int celly)
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
                if (State[x,y].CellType == Cell.Type.mine)
                {
                    count++;
                }
            }
        }
        return count;
    }

    

    
}
