using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBoardSystem : MonoBehaviour,IUpdateBoard
{
    public IInitialBoard InitialSystem { get; set; }
    public Cell[,] State => InitialSystem.State;
    public bool GameOver { get; set; } = false;
    public bool GameWin { get; set; } = false;
    public void Flag(Vector3Int cellPosition)
    {
        Cell cell = GetCell(cellPosition.x, cellPosition.y);
        if(cell.CellType == Cell.Type.invalid || cell.Revealed) return;
        cell.Flagged = !cell.Flagged;
        State[cellPosition.x, cellPosition.y] = cell;
    }
    public void Reveal(Vector3Int cellPosition)
    {
        Cell cell = GetCell(cellPosition.x, cellPosition.y);
        if (cell.CellType == Cell.Type.invalid || cell.Revealed || cell.Flagged) return;
        switch (cell.CellType)
        {
            case Cell.Type.mine:
                cell.Exploded = true;
                GameOver = true;
                RevealCell(cellPosition, cell);
                break;
            case Cell.Type.none:
                Flood(cell);
                break;
            default:
                RevealCell(cellPosition, cell);
                break;
        }
        
    }

    public bool CheckWinCondition()
    {
        for(int i = 0; i < InitialSystem.Width; i++)
        {
            for(int j = 0; j < InitialSystem.Height; j++)
            {
                Cell cell = State[i, j];
                if(cell.CellType != Cell.Type.mine && !cell.Revealed)
                {
                    return false;
                }
            }
        }
        return true;
    }
    private void Flood(Cell cell)
    {
        if (cell.Revealed) return;
        if (cell.CellType == Cell.Type.mine || cell.CellType == Cell.Type.invalid) return;
        var cellPosition = new Vector3Int(cell.Position.x, cell.Position.y);
        RevealCell(cellPosition, cell);
        if (cell.CellType == Cell.Type.none)
        {
            Flood(GetCell(cell.Position.x, cell.Position.y + 1));
            Flood(GetCell(cell.Position.x, cell.Position.y - 1));
            Flood(GetCell(cell.Position.x - 1, cell.Position.y));
            Flood(GetCell(cell.Position.x + 1, cell.Position.y));
        }
    }
    private void RevealCell(Vector3Int cellPosition, Cell cell)
    {
        cell.Revealed = true;
        State[cellPosition.x, cellPosition.y] = cell;
    }
    public void RevealAllMines()
    {
        for(int i = 0;i < InitialSystem.Width; i++)
        {
            for(int j = 0;j < InitialSystem.Height; j++)
            {
                var cell = State[i, j];
                if(cell.CellType == Cell.Type.mine)
                {
                    RevealCell(new Vector3Int(i, j), cell);
                }
            }
        }
    }

    private Cell GetCell(int x, int y)
    {
        if (IsValid(x, y))
        {
            return State[x, y];
        }
        else
        {
            return new Cell();
        }
    }
    public void RevealNeighbor(Vector3Int cellPosition)
    {
        Cell cell = GetCell(cellPosition.x, cellPosition.y);
        if (!cell.Revealed) return;
        if (cell.CellType == Cell.Type.mine || cell.CellType == Cell.Type.invalid) return;
        if (cell.Number != CountFlag(cellPosition.x, cellPosition.y)) return;
        for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
        {
            for (int adjacentY = -1; adjacentY <= 1; adjacentY++)
            {
                if (adjacentX == 0 && adjacentY == 0) continue;
                var x = cellPosition.x + adjacentX;
                var y = cellPosition.y + adjacentY;
                if (x < 0 || x >= InitialSystem.Width || y < 0 || y >= InitialSystem.Height) continue;
                Reveal(new Vector3Int(x,y));
            }
        }
    }
    private int CountFlag(int cellx, int celly)
    {
        int count = 0;

        for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
        {
            for (int adjacentY = -1; adjacentY <= 1; adjacentY++)
            {
                if (adjacentX == 0 && adjacentY == 0) continue;
                var x = cellx + adjacentX;
                var y = celly + adjacentY;
                if (x < 0 || x >= InitialSystem.Width || y < 0 || y >= InitialSystem.Height) continue;
                if (State[x, y].Flagged)
                {
                    count++;
                }
            }
        }
        return count;
    }
    private bool IsValid(int x, int y)
    {
        return x >= 0 && x < InitialSystem.Width && y >= 0 && y < InitialSystem.Height;
    }

}
