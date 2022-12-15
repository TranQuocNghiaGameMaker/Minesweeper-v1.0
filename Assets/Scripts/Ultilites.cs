using UnityEngine;
using UnityEngine.Tilemaps;

public class Ultilites : MonoBehaviour
{
    public static Vector3Int GetTileAtWorldPoint(Camera cam, Tilemap tilemap)
    {
        Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return cellPosition;
    }
}
