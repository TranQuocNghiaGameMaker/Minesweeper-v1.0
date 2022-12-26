using UnityEngine;

internal interface IUpdateBoard
{
    GameEvent ExplodedEvent { get; set; }
    GameEvent WinEvent { get; set; }
    IInitialBoard InitialSystem { get; set; }
    Cell[,] _state { get; }

    void GetGameEvent(GameEvent eventA, GameEvent eventB);
    void Flag(Vector3Int cellPosition);
    void Reveal(Vector3Int cellPosition);
    void CheckWinCondition();
}