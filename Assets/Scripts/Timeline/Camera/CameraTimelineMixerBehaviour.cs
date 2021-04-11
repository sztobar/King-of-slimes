using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CameraTimelineMixerBehaviour : PlayableBehaviour
{
  private TimelineCamera timelineCamera;

  public override void OnPlayableDestroy(Playable playable)
  {
    if (timelineCamera)
    {
      timelineCamera.ResetState();
      timelineCamera = null;
    }
  }

  // blending logic inspired by cinemachine:
  // https://github.com/Unity-Technologies/com.unity.cinemachine/blob/5d6d08591076a1fcd5911a70ece75a4bab32fc16/Runtime/Timeline/CinemachineMixer.cs#L185
  public override void ProcessFrame(Playable playable, FrameData info, object playerData)
  {
    TimelineCamera timelineCamera = playerData as TimelineCamera;
    if (!timelineCamera)
      return;

    if (!this.timelineCamera)
      this.timelineCamera = timelineCamera;

    int activeInputs = 0;
    int inputIndexA = -1;
    int inputIndexB = -1;
    bool incomingIsA = false; // Assume that incoming clip is clip B
    float weightB = 1;

    int inputCount = playable.GetInputCount();
    for (int i = 0; i < inputCount; i++)
    {
      ScriptPlayable<CameraTimelineBehaviour> playableInput = (ScriptPlayable<CameraTimelineBehaviour>)playable.GetInput(i);
      CameraTimelineBehaviour behaviour = playableInput.GetBehaviour();
      float weight = playable.GetInputWeight(i);
      if (behaviour.IsValid && weight > 0)
      {
        inputIndexA = inputIndexB;
        inputIndexB = i;
        weightB = weight;

        if (++activeInputs == 2)
        {
          // Deduce which clip is incoming (timeline doesn't know)
          var inputA = playable.GetInput(inputIndexA);
          var inputB = playableInput;
          double timeA = inputA.GetTime();
          double timeB = inputB.GetTime();
          // If same start time, longer clip is incoming
          if (timeA == timeB)
            incomingIsA = inputB.GetDuration() < inputA.GetDuration();
          else
            // Incoming has later start time (therefore earlier current time)
            incomingIsA = timeB >= timeA;
          break;
        }
      }
    }
    // left just in case I would need it
    // Special case: check for only one clip that's fading out - it must be outgoing
    //if (activeInputs == 1 && weightB < 1
    //  && playable.GetInput(inputIndexB).GetTime() > playable.GetInput(inputIndexB).GetDuration() / 2)
    //{
    //  incomingIsA = true;
    //}

    if (incomingIsA)
    {
      (inputIndexA, inputIndexB) = (inputIndexB, inputIndexA);
      weightB = 1 - weightB;
    }

    if (activeInputs == 1)
    {
      var input = (ScriptPlayable<CameraTimelineBehaviour>)playable.GetInput(inputIndexB);
      var behaviour = input.GetBehaviour();
      CameraSegment segment = behaviour.segment;
      Vector3 position = behaviour.target.position;
      timelineCamera.SetState(segment, position);
    }
    else if (activeInputs == 2)
    {
      var inputA = (ScriptPlayable<CameraTimelineBehaviour>)playable.GetInput(inputIndexA);
      var behaviourA = inputA.GetBehaviour();
      var inputB = (ScriptPlayable<CameraTimelineBehaviour>)playable.GetInput(inputIndexB);
      var behaviourB = inputB.GetBehaviour();
      if (behaviourA.segment == behaviourB.segment)
      {
        Vector3 position = Vector3.Lerp(behaviourA.target.position, behaviourB.target.position, weightB);
        CameraSegment segment = behaviourA.segment;
        timelineCamera.SetState(segment, position);
      }
      else
      {
        var stateA = behaviourA.GetState();
        var stateB = behaviourB.GetState();
        timelineCamera.SetFadeState(stateA, stateB, weightB);
      }
    }
  }
}
