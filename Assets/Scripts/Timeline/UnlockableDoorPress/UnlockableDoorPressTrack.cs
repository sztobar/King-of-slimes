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

  //  public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
  //  {
  //#if UNITY_EDITOR
  //    var comp = director.GetGenericBinding(this) as UnlockableDoor;
  //    if (comp == null)
  //      return;
  //    var so = new UnityEditor.SerializedObject(comp);
  //    var iter = so.GetIterator();
  //    while (iter.NextVisible(true))
  //    {
  //      if (iter.hasVisibleChildren)
  //        continue;
  //      driver.AddFromName<UnlockableDoor>(comp.gameObject, iter.propertyPath);
  //    }
  //#endif
  //    base.GatherProperties(director, driver);
  //  }
}
