using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.3333333f, 0.5490196f, 0.172549f)]
[TrackClipType(typeof(GameplayCutsceneCameraClip))]
[TrackBindingType(typeof(GameplayCutsceneCamera))]
public class GameplayCutsceneCameraTrack : TrackAsset
{
  public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) =>
    ScriptPlayable<GameplayCutsceneCameraMixerBehaviour>.Create(graph, inputCount);
}
