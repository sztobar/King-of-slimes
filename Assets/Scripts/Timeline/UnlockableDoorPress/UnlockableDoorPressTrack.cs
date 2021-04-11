using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.1882353f, 0.3254902f, 0.5686275f)]
[TrackClipType(typeof(UnlockableDoorPressClip))]
[TrackBindingType(typeof(UnlockableDoor))]
public class UnlockableDoorPressTrack : TrackAsset
{
  public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) =>
    ScriptPlayable<UnlockableDoorPressMixerBehaviour>.Create(graph, inputCount);
}
