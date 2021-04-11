using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameplayCutscene : MonoBehaviour
{
  public PlayableDirector director;
  public GameplayCutsceneCamera cutsceneCamera;

  [HideInInspector] public bool hasPlayed;
  private bool finished;

  public void CutsceneStart()
  {
    director.timeUpdateMode = DirectorUpdateMode.Manual;
    director.time = 0;
    director.Play();
    finished = false;
    //InitCameraForCutscene();
    //hasPlayed = true;
  }

  private void OnDirectorFinished()
  {
    finished = true;
    //cutsceneCamera.ResetTimelineOverride();
  }

  public void CutsceneUpdate(float dt) {
    director.time += dt;
    director.Evaluate();
    if (director.time < director.playableAsset.duration)
    {
      director.time += Time.deltaTime;
    }
    else
    {
      director.Stop();
      OnDirectorFinished();
    }
  }

  public bool IsCutsceneFinished() => finished;

  internal void Skip()
  {
    director.time = director.playableAsset.duration;
    director.Stop();
    //director.Evaluate();
    OnDirectorFinished();
  }

  public GameplayCameraState GetBeginCameraState()
  {
    GameplayCameraState state = new GameplayCameraState();
    foreach (PlayableBinding output in director.playableAsset.outputs)
    {
      if (state.segment)
        break;

      if (output.sourceObject is GameplayCutsceneCameraTrack track)
      {
        foreach (TimelineClip clip in track.GetClips())
        {
          if (clip.asset is GameplayCutsceneCameraClip cameraClip)
          {
            IExposedPropertyTable graphResolver = director.playableGraph.GetResolver();
            state.segment = cameraClip.segment.Resolve(graphResolver);
            state.target = cameraClip.target.Resolve(graphResolver);
            break;
          }
        }
      }
    }
    return state;
  }

  private void InitCameraForCutscene()
  {
    CameraSegment segment = null;
    Transform target = null;
    foreach (PlayableBinding output in director.playableAsset.outputs)
    {
      if (segment)
        break;

      if (output.sourceObject is GameplayCutsceneCameraTrack track)
      {
        foreach (TimelineClip clip in track.GetClips())
        {
          if (clip.asset is GameplayCutsceneCameraClip cameraClip)
          {
            IExposedPropertyTable graphResolver = director.playableGraph.GetResolver();
            segment = cameraClip.segment.Resolve(graphResolver);
            target = cameraClip.target.Resolve(graphResolver);
            break;
          }
        }
      }
    }
    cutsceneCamera.InitTimelineOverride(segment, target);
  }
}