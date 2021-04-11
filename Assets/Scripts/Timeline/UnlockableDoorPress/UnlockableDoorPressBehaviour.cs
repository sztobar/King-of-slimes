using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class UnlockableDoorPressBehaviour : PlayableBehaviour
{
  [Range(0, 1)] public float openedPercent;

  public override void OnPlayableCreate(Playable playable)
  {

  }
}
