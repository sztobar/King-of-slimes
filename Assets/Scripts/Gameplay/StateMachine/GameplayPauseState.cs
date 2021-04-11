using Kite;
using System.Collections;
using UnityEngine;

public class GameplayPauseState : GameplayState
{
  public override void StateStart()
  {
    Time.timeScale = 0f;
  }
}
