using System;
using UnityEngine;

[Obsolete]
public class GameplayCamera : MonoBehaviour, IGameplayComponent
{
  private CameraSegment selectedSegment;
  private Transform followTarget;

  private CameraSegment pendingSegment;
  private Transform pendingFollowTarget;

  private GameplayManager gameplayManager;

  public CameraSegment ActiveSegment => selectedSegment;
  public Action OnChange { get; set; } = () => { };
  public Transform FollowTarget => followTarget;

  public GameplayCameraState GetState() => new GameplayCameraState
  {
    segment = selectedSegment,
    target = followTarget
  };

  public void SetState(GameplayCameraState state)
  {
    selectedSegment = state.segment;
    followTarget = state.target;
  }

  public void ApplyPending()
  {
    SelectSegmentInstant(pendingSegment, pendingFollowTarget);
    pendingSegment = null;
    pendingFollowTarget = null;
  }

  public void TransitionToSegment(CameraSegment newSegment, Transform followTarget, bool fadeIfSegmentChange = false)
  {
    if (!newSegment)
      return;

    if (!selectedSegment)
    {
      SelectSegmentInstant(newSegment, followTarget);
    }
    else if (newSegment == selectedSegment)
    {
      TransitionToTarget(followTarget);
    }
    else
    {
      if (fadeIfSegmentChange)
      {
        FadeTransitionToSegment(newSegment, followTarget);
      }
      else
      {
        SelectSegmentInstant(newSegment, followTarget);
      }
    }
  }

  // Restore camera after cutscene
  public void RestoreSegment(CameraSegment segment, Transform target)
  {
    if (gameplayManager.fadeTransition.IsPlaying())
    {
      if (gameplayManager.fadeTransition.IsFadeIn())
      {
        if (selectedSegment == segment)
        {
          // negative fade-in
          gameplayManager.fsm.cameraFadeState.positive = false;
          gameplayManager.fsm.PushCameraFade();
          segment.SetCameraTarget(target);
        }
        else
        {
          // finish fade-in
          // normal fade-out
          FadeTransitionToSegment(segment, target);
        }
      }
      else
      {
        if (selectedSegment == segment)
        {
          // finish fade-out
          gameplayManager.fsm.PushCameraFade();
          segment.SetCameraTarget(target);
        }
        else
        {
          // negative fade-out
          // positive fade-out to pending
          gameplayManager.fsm.cameraFadeState.positive = false;
          FadeTransitionToSegment(segment, target);
        }
      }
    }
    else if (gameplayManager.fsm.Head == gameplayManager.fsm.cameraCatchUpState)
    {
      gameplayManager.fsm.PopState();
      TransitionToSegment(segment, target, true);
    }
    else
    {
      TransitionToSegment(segment, target, true);
    }
  }

  private void FadeTransitionToSegment(CameraSegment newSegment, Transform followTarget)
  {
    pendingFollowTarget = followTarget;
    pendingSegment = newSegment;
    gameplayManager.fsm.PushCameraFade();
  }

  public void TransitionToTarget(Transform followTarget)
  {
    SetFollowTargetInstant(followTarget);
    gameplayManager.fsm.PushCameraCatchUp();
  }

  public void SelectSegmentInstant(CameraSegment newSegment, Transform follow)
  {
    if (selectedSegment && selectedSegment != newSegment)
      selectedSegment.SetCameraInactive();

    selectedSegment = newSegment;
    selectedSegment.SetCameraActive();
    SetFollowTargetInstant(follow);
    //selectedSegment.SetCameraPosition(follow);
    OnChange?.Invoke();
  }

  public void SetFollowTargetInstant(Transform followTarget)
  {
    selectedSegment.SetCameraTarget(followTarget);

    this.followTarget = followTarget;
  }

  public void Inject(GameplayManager gameplayManager)
  {
    this.gameplayManager = gameplayManager;
  }

  public void Reset()
  {
    if (selectedSegment)
      selectedSegment.SetCameraInactive();
    selectedSegment = null;
    followTarget = null;
  }
}