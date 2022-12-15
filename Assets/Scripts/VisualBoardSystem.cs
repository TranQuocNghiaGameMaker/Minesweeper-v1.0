using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VisualBoardSystem : MonoBehaviour
{
    [SerializeField] Tilemap _tilemap;
    [SerializeField] TilesSO _tileLibrary;
    public Tilemap Tilemap => _tilemap;
    public void Draw(Cell[,] state)
    {
        int width = state.GetLength(0);
        int height = state.GetLength(1);
        for(int i = 0;i < width; i++)
        {
            for(int j = 0;j < height; j++)
            {
                Cell cell = state[i, j];
                Tilemap.SetTile(cell.Position, GetTile(cell,_tileLibrary));
            }
        }
    }

    private Tile GetTile(Cell cell,TilesSO tileLibrary)
    {
        if (cell.Flagged)
        {
            return tileLibrary.TileFlag;
        }
        else if (cell.Revealed)
        {
            return GetCellRevealed(cell,tileLibrary);
        }
        else
        {
            return tileLibrary.TileUnknown;
        }
    }

    private Tile GetCellRevealed(Cell cell,TilesSO tileLibrary)
    {
        switch (cell.CellType)
        {
            case Cell.Type.none: return tileLibrary.TileEmpty;
            case Cell.Type.mine: return tileLibrary.TileMine;
            case Cell.Type.number:return GetNumberCell(cell,tileLibrary);
            default: return null;
        }
    }
    private Tile GetNumberCell(Cell cell, TilesSO tileLibrary)
    {
        return tileLibrary.NumberTile[cell.Number - 1];
    }
}
