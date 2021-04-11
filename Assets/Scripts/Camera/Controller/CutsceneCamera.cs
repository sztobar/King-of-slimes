using UnityEngine;

public class CutsceneCamera : MonoBehaviour
{
  public CameraController controller;
  private GameplayCameraState state;
  private bool hasTransition;

  public GameplayCameraState GetState() => state;

  public void SetState(GameplayCameraState newState)
  {
    state = newState;
    controller.SetState(state);
  }

  public void TransitionTo(GameplayCameraState newState)
  {
    state = newState;
    controller.TransitionTo(state);

    hasTransition = controller.transition.HasTransition(); 
    if (hasTransition)
    {
      controller.transition.TransitionStart();
    }
  }

  public bool HasTransitionToCutscene() => hasTransition;

  public void TransitionToCutsceneUpdate(float dt)
  {
    controller.TransitionUpdate(dt);
    hasTransition = controller.HasTransition();
  }
}