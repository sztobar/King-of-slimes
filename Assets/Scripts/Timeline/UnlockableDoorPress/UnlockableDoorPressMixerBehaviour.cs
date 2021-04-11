using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class UnlockableDoorPressMixerBehaviour : PlayableBehaviour
{
  private bool firstFrameHappened;
  private UnlockableDoor unlockableDoor;
  private bool isOpening;

  public override void OnPlayableDestroy(Playable playable)
  {
    firstFrameHappened = false;
    if (unlockableDoor)
    {
      if (!Application.isPlaying)
        unlockableDoor.doorOpener.Reset();
      unlockableDoor = null;
    }
  }

  public override void ProcessFrame(Playable playable, FrameData info, object playerData)
  {
    UnlockableDoor unlockableDoor = playerData as UnlockableDoor;

    if (!unlockableDoor)
      return;

    this.unlockableDoor = unlockableDoor;
    isOpening = unlockableDoor.GetOpenStateOnPress(pressed: true);
    int inputCount = playable.GetInputCount();
    for (int i = 0; i < inputCount; i++)
    {
      float weight = playable.GetInputWeight(i);
      ScriptPlayable<UnlockableDoorPressBehaviour> input = (ScriptPlayable<UnlockableDoorPressBehaviour>)playable.GetInput(i);
      UnlockableDoorPressBehaviour behaviour = input.GetBehaviour();

      unlockableDoor.doorOpener.SetNormalizedTime(isOpening, weight);

      if (!firstFrameHappened)
      {
        firstFrameHappened = true;
      }
    }
  }
}
