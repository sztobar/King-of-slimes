using UnityEngine;
using UnityEngine.Timeline;
using UnityEditor.Timeline.Actions;
using System.Collections.Generic;
using Kite;

namespace KiteEditor
{
  [MenuEntry("Editing/Flip Curves X")]
  public class FlipCurvesXClipAction : ClipAction
  {
    public override bool Execute(IEnumerable<TimelineClip> timelineClips)
    {
      foreach (TimelineClip timelineClip in timelineClips)
      {
        AnimationClip curvesClip = timelineClip.curves;
        AnimationClipHelpers.FlipX(curvesClip);
      }
      return true;
    }

    public override ActionValidity Validate(IEnumerable<TimelineClip> clips)
    {
      bool valid = false;
      foreach(TimelineClip clip in clips)
      {
        valid = clip.curves;
      }
      return valid ? ActionValidity.Valid : ActionValidity.NotApplicable;
    }
  }
}