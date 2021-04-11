using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class CameraTimelineBehaviour : PlayableBehaviour
{
  [HideInInspector] public CameraSegment segment;
  [HideInInspector] public Transform target;

  public bool IsValid => segment && target;

  public CameraState GetState() => new CameraState
  {
    segment = segment,
    target = target
  };
}
