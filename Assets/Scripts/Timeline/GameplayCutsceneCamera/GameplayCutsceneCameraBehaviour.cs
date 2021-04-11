using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class GameplayCutsceneCameraBehaviour : PlayableBehaviour
{
  [HideInInspector] public CameraSegment segment;
  [HideInInspector] public Transform target;
  [Range(0, 1)] public float targetLerp;

  public bool IsValid => segment && target;

  public GameplayCameraState GetState() =>
  new GameplayCameraState
  {
    segment = segment,
    target = target
  };

  public override void OnPlayableCreate(Playable playable)
  {

  }
}
