using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class CameraTimelineClip : PlayableAsset, ITimelineClipAsset
{
  public CameraTimelineBehaviour template = new CameraTimelineBehaviour();
  public ExposedReference<CameraSegment> segment;
  public ExposedReference<Transform> target;

  public ClipCaps clipCaps => ClipCaps.Blending;

  public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
  {
    ScriptPlayable<CameraTimelineBehaviour> playable = ScriptPlayable<CameraTimelineBehaviour>.Create(graph, template);
    CameraTimelineBehaviour clone = playable.GetBehaviour();
    IExposedPropertyTable graphResolver = graph.GetResolver();
    clone.segment = segment.Resolve(graphResolver);
    clone.target = target.Resolve(graphResolver);
    return playable;
  }
}
