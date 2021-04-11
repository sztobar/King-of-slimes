using Kite;
using System;
using System.Collections;
using UnityEngine;

[Obsolete]
public class GameplayCameraFadeState : GameplayState
{
  public FadeTransition fadeTransition;

  private GameplayCamera gameplayCamera;
  private GameplayStateMachine fsm;
  public bool positive = true;

  public override void StateStart()
  {
    fadeTransition.StartFadeIn();
  }

  public override void StateUpdate()
  {
    float sign = positive ? 1 : -1;
    float dt = Time.unscaledDeltaTime * sign;

    fadeTransition.TransitionUpdate(dt);

    if (fadeTransition.phase == FadeTransition.Phase.Faded)
    {
      // positive is needed for the next fade-out
      positive = true;
      gameplayCamera.ApplyPending();
      fadeTransition.StartFadeOut();
    }
    else if (fadeTransition.phase == FadeTransition.Phase.None)
    {
      fsm.PopState();
    }
  }

  public override void StateExit()
  {
    // positive is reset after state is finished
    positive = true;
  }

  public override void Inject(GameplayManager gameplayManager)
  {
    gameplayCamera = gameplayManager.camera;
    fsm = gameplayManager.fsm;
  }
}