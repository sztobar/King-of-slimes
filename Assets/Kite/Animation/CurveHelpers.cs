using UnityEditor;
using UnityEngine;

namespace Kite
{
  public static class CurveHelpers
  {
    public static void FlipX(AnimationCurve curve, float min, float max)
    {
      Keyframe[] keys = curve.keys;
      int keyCount = keys.Length;
      (curve.preWrapMode, curve.postWrapMode) = (curve.postWrapMode, curve.preWrapMode);

      for (int i = 0; i < keyCount; i++)
      {
        Keyframe keyframe = keys[i];
        float lerpValue = Mathf.InverseLerp(min, max, keyframe.time);
        keyframe.time = Mathf.Lerp(max, min, lerpValue);

        float inTangent = (keyframe.outTangent != Mathf.Infinity) ? -keyframe.outTangent : Mathf.Infinity;
        float outTangent = (keyframe.inTangent != Mathf.Infinity) ? -keyframe.inTangent : Mathf.Infinity;
        (keyframe.inTangent, keyframe.outTangent) = (inTangent, outTangent);

        if (keyframe.weightedMode == WeightedMode.In)
          keyframe.weightedMode = WeightedMode.Out;
        else if (keyframe.weightedMode == WeightedMode.Out)
          keyframe.weightedMode = WeightedMode.In;

        (keyframe.inWeight, keyframe.outWeight) = (keyframe.outWeight, keyframe.inWeight);

        keys[i] = keyframe;
      }
      curve.keys = keys;
    }

    public static void FlipX(AnimationCurve curve)
    {
      float firstKeyTime = GetFirstKeyframeTime(curve);
      float lastKeyTime = GetLastKeyframeTime(curve);
      FlipX(curve, firstKeyTime, lastKeyTime);
    }

    public static void FlipX01(AnimationCurve curve) =>
      FlipX(curve, 0, 1);

    public static float GetFirstKeyframeTime(AnimationCurve curve)
    {
      Keyframe[] keys = curve.keys;
      return keys.Length == 0 ? 0 : keys[0].time;
    }

    public static float GetLastKeyframeTime(AnimationCurve curve)
    {
      Keyframe[] keys = curve.keys;
      int keyCount = keys.Length;

      return keyCount == 0 ? 1 : keys[keyCount - 1].time;
    }

    public static (float min, float max) GetMinMax(AnimationCurve curve)
    {
      Keyframe[] keys = curve.keys;
      int keyCount = keys.Length;
      if (keyCount == 0)
        return (0, 1);

      float min = keys[0].value;
      float max = keys[0].value;
      for (int i = 1; i < keyCount; i++)
      {
        Keyframe keyframe = keys[i];
        float value = keyframe.value;

        if (value < min)
          min = value;
        else if (value > max)
          max = value;
      }
      return (min, max);
    }

    public static void FlipY(AnimationCurve curve)
    {
      (float min, float max) = GetMinMax(curve);
      FlipY(curve, min, max);
    }

    public static void FlipY(AnimationCurve curve, float min, float max)
    {
      Keyframe[] keys = curve.keys;
      int keyCount = keys.Length;

      for (int i = 0; i < keyCount; i++)
      {
        Keyframe keyframe = keys[i];
        float lerpValue = Mathf.InverseLerp(min, max, keyframe.value);
        keyframe.value = Mathf.Lerp(max, min, lerpValue);

        keyframe.inTangent = (keyframe.outTangent != Mathf.Infinity) ? -keyframe.inTangent : Mathf.Infinity;
        keyframe.outTangent = (keyframe.inTangent != Mathf.Infinity) ? -keyframe.outTangent : Mathf.Infinity;

        keys[i] = keyframe;
      }
      curve.keys = keys;
    }

    public static void FlipY01(AnimationCurve curve) =>
      FlipY(curve, 0, 1);
  }
}