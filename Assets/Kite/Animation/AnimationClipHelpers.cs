using UnityEditor;
using UnityEngine;

namespace Kite
{
  public static class AnimationClipHelpers
  {
    public static void FlipX(AnimationClip clip)
    {
      float clipLength = clip.length;
      FlipXFloatCurve(clip, clipLength);
      FlipXObjectRefCurve(clip, clipLength);
      FlipXEvents(clip, clipLength);
    }

    public static void FlipY(AnimationClip clip)
    {
      float clipLength = clip.length;
      FlipYFloatCurve(clip, clipLength);
    }

    private static void FlipYFloatCurve(AnimationClip clip, float clipLength)
    {
      EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(clip);
      foreach (EditorCurveBinding curveBinding in curveBindings)
      {
        AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, curveBinding);
        CurveHelpers.FlipY(curve);
        AnimationUtility.SetEditorCurve(clip, curveBinding, curve);
      }
    }

    private static void FlipXEvents(AnimationClip clip, float clipLength)
    {
      AnimationEvent[] events = AnimationUtility.GetAnimationEvents(clip);
      if (events.Length > 0)
      {
        for (int i = 0; i < events.Length; i++)
        {
          events[i].time = clipLength - events[i].time;
        }
        AnimationUtility.SetAnimationEvents(clip, events);
      }
    }

    private static void FlipXObjectRefCurve(AnimationClip clip, float clipLength)
    {
      EditorCurveBinding[] objectRefCurveBindings = AnimationUtility.GetObjectReferenceCurveBindings(clip);
      foreach (EditorCurveBinding curveBinding in objectRefCurveBindings)
      {
        ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(clip, curveBinding);
        for (int i = 0; i < keyframes.Length; i++)
        {
          keyframes[i].time = clipLength - keyframes[i].time;
        }
        AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyframes);
      }
    }

    private static void FlipXFloatCurve(AnimationClip clip, float clipLength)
    {
      EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(clip);
      foreach (EditorCurveBinding curveBinding in curveBindings)
      {
        AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, curveBinding);
        CurveHelpers.FlipX(curve, 0, clipLength);
        AnimationUtility.SetEditorCurve(clip, curveBinding, curve);
      }
    }
  }
}