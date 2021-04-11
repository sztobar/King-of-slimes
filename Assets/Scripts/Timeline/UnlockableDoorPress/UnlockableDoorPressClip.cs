using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class UnlockableDoorPressClip : PlayableAsset, ITimelineClipAsset
{
  public UnlockableDoorPressBehaviour template = new UnlockableDoorPressBehaviour();

  public ClipCaps clipCaps => ClipCaps.Extrapolation | ClipCaps.Blending;

  public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
  {
    var playable = ScriptPlayable<UnlockableDoorPressBehaviour>.Create(graph, template);
    UnlockableDoorPressBehaviour clone = playable.GetBehaviour();
    return playable;
  }
}
