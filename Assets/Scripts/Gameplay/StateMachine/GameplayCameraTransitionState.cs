using Kite;
using System.Collections;
using UnityEngine;

public class GameplayCameraTransitionState : GameplayState
{
  public CameraController cameraController;
  public CameraTransitionManager transitionManager;
  private GameplayStateMachine fsm;

  public override void StateUpdate()
  {
    if (cameraController.HasTransition())
    {
      float dt = Time.unscaledDeltaTime;
      cameraController.TransitionUpdate(dt);
    }
    else
    {
      fsm.PopState();
    }
  }

  public override void Inject(GameplayManager gameplayManager)
  {
    fsm = gameplayManager.fsm;
  }
}