using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthChanged: UnityEvent <int, int>
{

}

public class ScoreChanged : UnityEvent<int>
{

}

public class GamePauseChanged : UnityEvent<bool>
{

}
