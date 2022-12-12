using UnityEngine;

public struct Cell  
{
    public enum Type
    {
        none,
        number,
        mine
    }
    public Type CellType;
    public Vector3Int Position;
    public int Number;
    public bool Revealed;
    public bool Flagged;
    public bool Exploded;
}
