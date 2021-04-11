using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameplayCutscene : MonoBehaviour
{
  public PlayableDirector director;

  [HideInInspector] public bool hasPlayed;
  private bool finished;

  public void CutsceneStart()
  {
    director.timeUpdateMode = DirectorUpdateMode.Manual;
    director.time = 0;
    director.Play();
    finished = false;
  }

  private void OnDirectorFinished()
  {
    finished = true;
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
    OnDirectorFinished();
  }

  public CameraState GetBeginCameraState()
  {
    CameraState state = new CameraState();
    foreach (PlayableBinding output in director.playableAsset.outputs)
    {
      if (state.segment)
        break;

      if (output.sourceObject is CameraTimelineTrack track)
      {
        foreach (TimelineClip clip in track.GetClips())
        {
          if (clip.asset is CameraTimelineClip cameraClip)
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
}