using UnityEngine.Tilemaps;

internal interface IVisualBoard
{
    Tilemap Tilemap { get; }

    void Draw(Cell[,] state);
}