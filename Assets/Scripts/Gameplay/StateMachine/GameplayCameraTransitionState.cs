using Kite;
using System.Collections;
using UnityEngine;

public class GameplayCameraTransitionState : GameplayState
{
  public CameraController cameraController;
  public CameraTransitionManager transitionManager;
  private GameplayStateMachine fsm;

  public override void StateStart()
  {
    //transitionManager.TransitionStart();
    //cameraController.SetState(transitionManager.GetState());
  }

  public override void StateUpdate()
  {
    if (cameraController.HasTransition())
    {
      float dt = Time.unscaledDeltaTime;
      cameraController.TransitionUpdate(dt);
      //cameraController.SetState(transitionManager.GetState());
      //transitionManager.TransitionUpdate(dt);
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