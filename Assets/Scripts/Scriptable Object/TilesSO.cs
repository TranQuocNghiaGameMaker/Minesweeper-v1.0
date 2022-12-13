using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tiles",menuName = "Scriptable Object")]
public class TilesSO : ScriptableObject
{
    public Tile TileMine;
    public Tile TileEmpty;
    public Tile TileExploded;
    public Tile TileFlag;
    public Tile TileUnknown;
    public List<Tile> NumberTile;

    [ContextMenu("Load Component")]
    private void LoadComponents()
    {
        NumberTile = new();
        TileMine = Resources.Load<Tile>("TileMine");
        TileEmpty = Resources.Load<Tile>("TileEmpty");
        TileExploded = Resources.Load<Tile>("TileExploded");
        TileFlag = Resources.Load<Tile>("TileFlag");
        TileUnknown = Resources.Load<Tile>("TileUnknown");

        for(int i = 1;i <= 8; i++)
        {
            var tile = Resources.Load<Tile>("Tile" + i);
            NumberTile.Add(tile);
        }
    }
}
