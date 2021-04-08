using Kite;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  public static class SerializedEnvelopeHelpers
  {
    public static void UpdateAttackCurve(SerializedProperty envProp, AnimationCurve curve)
    {
      var attackCurveProp = envProp.FindPropertyRelative(nameof(Envelope.attackCurve));
      attackCurveProp.animationCurveValue = curve;

      var attackEndValue = AnimationCurveHelpers.GetLastKeyValue(attackCurveProp);

      var decayCurveProp = envProp.FindPropertyRelative(nameof(Envelope.decayCurve));
      AnimationCurveHelpers.SetCurveStart(decayCurveProp, attackEndValue);

      var decayTimeProp = envProp.FindPropertyRelative(nameof(Envelope.decayTime));
      if (decayTimeProp.floatValue == 0)
      {
        AnimationCurveHelpers.SetCurveEnd(decayCurveProp, attackEndValue);

        var sustainValueProp = envProp.FindPropertyRelative(nameof(Envelope.sustainValue));
        sustainValueProp.floatValue = attackEndValue;

        var relaseCurveProp = envProp.FindPropertyRelative(nameof(Envelope.releaseCurve));
        AnimationCurveHelpers.SetCurveStart(relaseCurveProp, attackEndValue);
      }
    }

    public static void UpdateDecayCurve(SerializedProperty envProp, AnimationCurve curve)
    {
      var decayCurveProp = envProp.FindPropertyRelative(nameof(Envelope.decayCurve));
      decayCurveProp.animationCurveValue = curve;

      var decayEndValue = AnimationCurveHelpers.GetLastKeyValue(decayCurveProp);
      var decayStartValue = AnimationCurveHelpers.GetFirstKeyValue(decayCurveProp);

      var attackCurveProp = envProp.FindPropertyRelative(nameof(Envelope.attackCurve));
      AnimationCurveHelpers.SetCurveEnd(attackCurveProp, decayStartValue);

      var sustainValueProp = envProp.FindPropertyRelative(nameof(Envelope.sustainValue));
      sustainValueProp.floatValue = decayEndValue;

      var relaseCurveProp = envProp.FindPropertyRelative(nameof(Envelope.releaseCurve));
      AnimationCurveHelpers.SetCurveStart(relaseCurveProp, decayEndValue);
    }

    public static void UpdateSustainValue(SerializedProperty envProp, float sustainValue)
    {
      SerializedProperty sustainValueProp = envProp.FindPropertyRelative(nameof(Envelope.sustainValue));
      sustainValueProp.floatValue = sustainValue;

      var relaseCurveProp = envProp.FindPropertyRelative(nameof(Envelope.releaseCurve));
      AnimationCurveHelpers.SetCurveStart(relaseCurveProp, sustainValue);

      var decayCurveProp = envProp.FindPropertyRelative(nameof(Envelope.decayCurve));
      AnimationCurveHelpers.SetCurveEnd(decayCurveProp, sustainValue);

      var decayTimeProp = envProp.FindPropertyRelative(nameof(Envelope.decayTime));
      if (decayTimeProp.floatValue == 0)
      {
        AnimationCurveHelpers.SetCurveStart(decayCurveProp, sustainValue);

        var attackCurveProp = envProp.FindPropertyRelative(nameof(Envelope.attackCurve));
        AnimationCurveHelpers.SetCurveEnd(attackCurveProp, sustainValue);
      }
    }

    public static void UpdateReleaseCurve(SerializedProperty envProp, AnimationCurve curve)
    {
      var relaseCurveProp = envProp.FindPropertyRelative(nameof(Envelope.releaseCurve));
      relaseCurveProp.animationCurveValue = curve;

      var releaseStartValue = AnimationCurveHelpers.GetFirstKeyValue(relaseCurveProp);

      var sustainValueProp = envProp.FindPropertyRelative(nameof(Envelope.sustainValue));
      sustainValueProp.floatValue = releaseStartValue;

      var decayCurveProp = envProp.FindPropertyRelative(nameof(Envelope.decayCurve));
      AnimationCurveHelpers.SetCurveEnd(decayCurveProp, releaseStartValue);

      var decayTimeProp = envProp.FindPropertyRelative(nameof(Envelope.decayTime));
      if (decayTimeProp.floatValue == 0)
      {
        AnimationCurveHelpers.SetCurveStart(decayCurveProp, releaseStartValue);

        var attackCurveProp = envProp.FindPropertyRelative(nameof(Envelope.attackCurve));
        AnimationCurveHelpers.SetCurveEnd(attackCurveProp, releaseStartValue);
      }
    }

    public static Envelope RetrieveEnvelope(SerializedProperty property)
    {
      return new Envelope
      {
        attackCurve = property.FindPropertyRelative(nameof(Envelope.attackCurve)).animationCurveValue,
        attackTime = property.FindPropertyRelative(nameof(Envelope.attackTime)).floatValue,
        decayTime = property.FindPropertyRelative(nameof(Envelope.decayTime)).floatValue,
        decayCurve = property.FindPropertyRelative(nameof(Envelope.decayCurve)).animationCurveValue,
        sustainTime = property.FindPropertyRelative(nameof(Envelope.sustainTime)).floatValue,
        sustainValue = property.FindPropertyRelative(nameof(Envelope.sustainValue)).floatValue,
        holdSustain = property.FindPropertyRelative(nameof(Envelope.holdSustain)).boolValue,
        releaseTime = property.FindPropertyRelative(nameof(Envelope.releaseTime)).floatValue,
        releaseCurve = property.FindPropertyRelative(nameof(Envelope.releaseCurve)).animationCurveValue,
      };
    }
  }
}