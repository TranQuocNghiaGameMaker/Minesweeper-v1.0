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
        cell.Revealed = true;
        State[cellPosition.x, cellPosition.y] = cell;
        VisualBoardSystem.ChangeBoardAction(State);
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
