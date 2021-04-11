using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameplayCutsceneCameraMixerBehaviour : PlayableBehaviour
{
  private GameplayCutsceneCamera cutsceneCamera;

  public override void OnPlayableDestroy(Playable playable)
  {
    if (cutsceneCamera)
    {
      cutsceneCamera.ResetTimelineOverride();
      cutsceneCamera = null;
    }
  }

  // blending logic inspired by cinemachine:
  // https://github.com/Unity-Technologies/com.unity.cinemachine/blob/5d6d08591076a1fcd5911a70ece75a4bab32fc16/Runtime/Timeline/CinemachineMixer.cs#L185
  public override void ProcessFrame(Playable playable, FrameData info, object playerData)
  {
    GameplayCutsceneCamera cutsceneCamera = playerData as GameplayCutsceneCamera;
    if (!cutsceneCamera)
      return;

    if (!this.cutsceneCamera)
      this.cutsceneCamera = cutsceneCamera;

    int activeInputs = 0;
    int inputIndexA = -1;
    int inputIndexB = -1;
    bool incomingIsA = false; // Assume that incoming clip is clip B
    float weightB = 1;

    int inputCount = playable.GetInputCount();
    for (int i = 0; i < inputCount; i++)
    {
      ScriptPlayable<GameplayCutsceneCameraBehaviour> playableInput = (ScriptPlayable<GameplayCutsceneCameraBehaviour>)playable.GetInput(i);
      GameplayCutsceneCameraBehaviour behaviour = playableInput.GetBehaviour();
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

    bool clearFade = true;
    if (activeInputs == 1)
    {
      var input = (ScriptPlayable<GameplayCutsceneCameraBehaviour>)playable.GetInput(inputIndexB);
      var behaviour = input.GetBehaviour();
      CameraSegment segment = behaviour.segment;
      Vector3 position = behaviour.target.position;
      cutsceneCamera.SetState(segment, position);
      //cutsceneCamera.SetTargetPosition(segment, position);
    }
    else if (activeInputs == 2)
    {
      var inputA = (ScriptPlayable<GameplayCutsceneCameraBehaviour>)playable.GetInput(inputIndexA);
      var behaviourA = inputA.GetBehaviour();
      var inputB = (ScriptPlayable<GameplayCutsceneCameraBehaviour>)playable.GetInput(inputIndexB);
      var behaviourB = inputB.GetBehaviour();
      if (behaviourA.segment == behaviourB.segment)
      {
        Vector3 position = Vector3.Lerp(behaviourA.target.position, behaviourB.target.position, weightB);
        CameraSegment segment = behaviourA.segment;
        cutsceneCamera.SetState(segment, position);
        //cutsceneCamera.SetTargetPosition(segment, targetPosition);
      }
      else
      {
        var stateA = behaviourA.GetState();
        var stateB = behaviourB.GetState();
        cutsceneCamera.SetFadeState(stateA, stateB, weightB);
        //clearFade = false;
        //if (weightB < 0.5f)
        //{
        //  cutsceneCamera.SetTargetPosition(behaviourA.segment, behaviourA.target.position);
        //  cutsceneCamera.SetFadeInAmount(weightB * 2);
        //}
        //else if (weightB >= 0.5f)
        //{
        //  cutsceneCamera.SetTargetPosition(behaviourB.segment, behaviourB.target.position);
        //  float fadeOutAmount = (weightB - 0.5f) * 2;
        //  cutsceneCamera.SetFadeOutAmount(fadeOutAmount);
        //}
      }
    }
    //if (clearFade)
    //  cutsceneCamera.ClearFade();
  }
}
