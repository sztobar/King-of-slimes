using Kite;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  public static class AnimationCurveHelpers
  {
    public static void SetCurveEnd(SerializedProperty prop, float value)
    {
      AnimationCurve toCurve = prop.animationCurveValue;
      Keyframe[] keyframes = toCurve.keys;
      if (keyframes.Length == 0)
        return;

      keyframes[keyframes.Length - 1].value = value;
      toCurve.keys = keyframes;
      prop.animationCurveValue = toCurve;
    }

    public static void SetCurveStart(SerializedProperty prop, float value)
    {
      AnimationCurve toCurve = prop.animationCurveValue;
      Keyframe[] keyframes = toCurve.keys;
      if (keyframes.Length == 0)
        return;

      keyframes[0].value = value;
      toCurve.keys = keyframes;
      prop.animationCurveValue = toCurve;
    }

    public static float GetLastKeyValue(SerializedProperty prop) =>
      GetLastKeyValue(prop.animationCurveValue);

    private static float GetLastKeyValue(AnimationCurve curve)
    {
      Keyframe[] keyframes = curve.keys;
      if (keyframes.Length == 0)
        return 1;

      return keyframes[keyframes.Length - 1].value;
    }


    public static float GetFirstKeyValue(SerializedProperty prop) =>
      GetFirstKeyValue(prop.animationCurveValue);

    private static float GetFirstKeyValue(AnimationCurve curve)
    {
      Keyframe[] keyframes = curve.keys;
      if (keyframes.Length == 0)
        return 0;

      return keyframes[0].value;
    }
  }
}