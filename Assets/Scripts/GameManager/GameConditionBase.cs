using UnityEngine;

public abstract class GameConditionBase: MonoBehaviour
{
    public GameEvent _explodedEvent;
    public GameEvent _newGameEvent;
    public GameEvent _winEvent;

    public abstract bool GameOver { get; set; }
}