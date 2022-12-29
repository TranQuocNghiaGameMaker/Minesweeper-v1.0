using UnityEngine;

internal interface IUpdateBoard
{
    IInitialBoard InitialSystem { get; set; }
    Cell[,] State { get; }
    bool GameOver { get; set; }

    void Flag(Vector3Int cellPosition);
    void Reveal(Vector3Int cellPosition);
    bool CheckWinCondition();
    void RevealNeighbor(Vector3Int cellPosition);
}