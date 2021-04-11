using Cinemachine;
using System;
using UnityEngine;

public class GameplayCutsceneCamera : MonoBehaviour, IGameplayComponent
{
  private static readonly int CUTSCENE_PRIORITY = 20;

  public CameraController controller;

  public FadeTransition fadeTransition;
  public GameplayCamera gameplayCamera;
  public bool instantRestore;

  private CameraSegment playSegment;
  private Transform playTarget;
  private bool initialized;

  public void SetState(CameraSegment segment, Vector3 position)
  {
    position.z = transform.position.z;
    transform.position = position;
    GameplayCameraState newState = new GameplayCameraState
    {
      segment = segment,
      target = transform
    };
    controller.SetTransition(null);
    controller.SetState(newState);
  }

  public void SetFadeState(GameplayCameraState from, GameplayCameraState to, float t)
  {
    controller.SetTransition(CameraFadeTransition.Create(from, to, t));
  }

  public void ResetState()
  {
    if (!Application.isPlaying)
    {
      controller.SetTransition(null);
      controller.ResetState();
    }
  }

  public void SetTargetPosition(CameraSegment segment, Vector3 position)
  {
    if (!initialized)
    {
      position.z = transform.position.z;
      transform.position = position;
      InitTimelineOverride(segment, transform);
    }

    if (segment != gameplayCamera.ActiveSegment)
      gameplayCamera.SelectSegmentInstant(segment, null);

    if (gameplayCamera.FollowTarget != null)
      gameplayCamera.SetFollowTargetInstant(null);

    position.z = transform.position.z;
    transform.position = position;
    gameplayCamera.ActiveSegment.SetCameraPosition(position);
  }

  public void SetFadeInAmount(float amount)
  {
    fadeTransition.SetFadeIn(amount);
  }

  public void SetFadeOutAmount(float amount)
  {
    fadeTransition.SetFadeOut(amount);
  }
  public void ClearFade()
  {
    fadeTransition.Clear();
  }

  public void InitTimelineOverride(CameraSegment cutsceneSegment, Transform target)
  {
    if (initialized)
      return;

    playSegment = gameplayCamera.ActiveSegment;
    playTarget = gameplayCamera.FollowTarget;
    if (Application.isPlaying)
    {
      gameplayCamera.TransitionToSegment(cutsceneSegment, target);
    }
    else
    {
      gameplayCamera.SelectSegmentInstant(cutsceneSegment, target);
    }
    initialized = true;
  }

  public void ResetTimelineOverride()
  {
    if (!initialized)
      return;

    if (Application.isPlaying)
    {
      if (instantRestore)
      {
        ClearFade();
        gameplayCamera.SelectSegmentInstant(playSegment, playTarget);
        playSegment.cam.virtualCam.ForceCameraPosition(playTarget.position, Quaternion.identity);
      }
      else
        gameplayCamera.RestoreSegment(playSegment, playTarget);
    }
    else
    {
      gameplayCamera.Reset();
    }
    playSegment = null;
    playTarget = null;
    initialized = false;
  }

  public void Inject(GameplayManager gameplayManager)
  {
  }
}