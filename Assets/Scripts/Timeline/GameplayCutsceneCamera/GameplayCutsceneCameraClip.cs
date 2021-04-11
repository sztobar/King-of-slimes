using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class GameplayCutsceneCameraClip : PlayableAsset, ITimelineClipAsset
{
  public GameplayCutsceneCameraBehaviour template = new GameplayCutsceneCameraBehaviour();
  public ExposedReference<CameraSegment> segment;
  public ExposedReference<Transform> target;

  public ClipCaps clipCaps => ClipCaps.Extrapolation | ClipCaps.Blending;

  public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
  {
    ScriptPlayable<GameplayCutsceneCameraBehaviour> playable = ScriptPlayable<GameplayCutsceneCameraBehaviour>.Create(graph, template);
    GameplayCutsceneCameraBehaviour clone = playable.GetBehaviour();
    IExposedPropertyTable graphResolver = graph.GetResolver();
    clone.segment = segment.Resolve(graphResolver);
    clone.target = target.Resolve(graphResolver);
    return playable;
  }
}
