using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBoardSystem : MonoBehaviour,IUpdateBoard
{
    public IInitialBoard InitialSystem { get; set; }
    public Cell[,] _state => InitialSystem.State;

    public GameEvent ExplodedEvent { get; set; }
    public GameEvent WinEvent { get; set; }

    public void GetGameEvent(GameEvent eventA, GameEvent eventB)
    {
        ExplodedEvent = eventA;
        WinEvent = eventB;
    }
    public void Flag(Vector3Int cellPosition)
    {
        Cell cell = GetCell(cellPosition.x, cellPosition.y);
        if(cell.CellType == Cell.Type.invalid || cell.Revealed) return;
        cell.Flagged = !cell.Flagged;
        _state[cellPosition.x, cellPosition.y] = cell;
    }
    public void Reveal(Vector3Int cellPosition)
    {
        Cell cell = GetCell(cellPosition.x, cellPosition.y);
        if (cell.CellType == Cell.Type.invalid || cell.Revealed || cell.Flagged) return;
        switch (cell.CellType)
        {
            case Cell.Type.mine:
                cell.Exploded = true;
                ExplodedEvent.Raise();
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

    public void CheckWinCondition()
    {
        for(int i = 0; i < InitialSystem.Width; i++)
        {
            for(int j = 0; j < InitialSystem.Height; j++)
            {
                Cell cell = _state[i, j];
                if(cell.CellType != Cell.Type.mine && !cell.Revealed)
                {
                    return;
                }
            }
        }
        WinEvent.Raise();
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
        _state[cellPosition.x, cellPosition.y] = cell;
    }
    public void RevealAllMines()
    {
        for(int i = 0;i < InitialSystem.Width; i++)
        {
            for(int j = 0;j < InitialSystem.Height; j++)
            {
                var cell = _state[i, j];
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
            return _state[x, y];
        }
        else
        {
            return new Cell();
        }
    }
    private bool IsValid(int x, int y)
    {
        return x >= 0 && x < InitialSystem.Width && y >= 0 && y < InitialSystem.Height;
    }

}
