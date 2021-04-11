using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.3333333f, 0.5490196f, 0.172549f)]
[TrackClipType(typeof(CameraTimelineClip))]
[TrackBindingType(typeof(TimelineCamera))]
public class CameraTimelineTrack : TrackAsset
{
  public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) =>
    ScriptPlayable<CameraTimelineMixerBehaviour>.Create(graph, inputCount);
}
