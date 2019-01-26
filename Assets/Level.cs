using Sirenix.OdinInspector;
using UnityEngine;

public enum LevelState
{
    None,
    Starting,
    Running,
    Complete
}

public class Level : MonoBehaviour
{
    [ShowInInspector]
    LevelState _state = LevelState.Starting;

    public LevelState GetState() { return _state; }
}
