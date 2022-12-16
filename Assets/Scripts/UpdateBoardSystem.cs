using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBoardSystem : MonoBehaviour
{
    public InitialBoardSystem InitialSystem { get; set; }
    public Cell[,] State => InitialSystem.State;
    // Update is called once per frame
    public void Flag(Vector3Int cellPosition)
    {
        Cell cell = GetCell(cellPosition.x, cellPosition.y);
        if(cell.CellType == Cell.Type.invalid || cell.Revealed) return;
        cell.Flagged = !cell.Flagged;
        State[cellPosition.x, cellPosition.y] = cell;
        VisualBoardSystem.ChangeBoardAction(State);
    }
    public void Reveal(Vector3Int cellPosition)
    {
        Cell cell = GetCell(cellPosition.x, cellPosition.y);
        if (cell.CellType == Cell.Type.invalid || cell.Revealed || cell.Flagged) return;
        if(cell.CellType == Cell.Type.none)
        {
            Flood(cell);
        }
        cell.Revealed = true;
        State[cellPosition.x, cellPosition.y] = cell;
        VisualBoardSystem.ChangeBoardAction(State);
    }

    private void Flood(Cell cell)
    {
        if (cell.Revealed) return;
        if (cell.CellType == Cell.Type.mine || cell.CellType == Cell.Type.invalid) return;

        cell.Revealed = true;
        State[cell.Position.x, cell.Position.y] = cell;
        if (cell.CellType == Cell.Type.none)
        {
            Flood(GetCell(cell.Position.x, cell.Position.y + 1));
            Flood(GetCell(cell.Position.x, cell.Position.y - 1));
            Flood(GetCell(cell.Position.x - 1, cell.Position.y));
            Flood(GetCell(cell.Position.x + 1, cell.Position.y));
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
    private bool IsValid(int x, int y)
    {
        return x >= 0 && x < InitialSystem.Width && y >= 0 && y < InitialSystem.Height;
    }
}
