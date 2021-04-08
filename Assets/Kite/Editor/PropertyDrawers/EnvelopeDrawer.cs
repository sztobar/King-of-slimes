using Kite;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  // Inpired by:
  // https://github.com/keijiro/MidiAnimationTrack
  // https://github.com/keijiro/MidiAnimationTrack/blob/c6365868ab590a88d2f0be5a26cee892783af14e/Packages/jp.keijiro.klak.timeline.midi/Editor/MidiEnvelopeDrawer.cs#L76
  [CustomPropertyDrawer(typeof(Envelope))]
  public class EnvelopeDrawer : PropertyDrawer
  {
    private static readonly float graphHeight = 40;
    private static readonly float minGraphWidth = 20;
    private static float dragStartValue;

    public static readonly Color curveColor = Color.green;
    public static readonly Color attackColor = Color.red;
    public static readonly Color decayColor = Color.yellow;
    public static readonly Color sustainColor = Color.green;
    public static readonly Color releaseColor = Color.magenta;

    public static readonly Color curveBackgroundColor = new Color(0.337f, 0.337f, 0.337f, 1f);

    public static readonly Rect ranges = new Rect(Vector2.zero, Vector2.one);
    private static readonly int sustainHash = "sustain".GetHashCode();

    private bool foldoutShown = true;

    private float GetHeight()
    {
      var line = EditorGUIUtility.singleLineHeight;
      var space = EditorGUIUtility.standardVerticalSpacing;
      float foldout = line + space;
      if (!foldoutShown)
      {
        return foldout;
      }

      var timeFields = line;
      if (!EditorGUIUtility.wideMode)
        timeFields *= 2;
      timeFields += space;

      float graph = 3 * space + graphHeight;
      float graphCaption = line + space;

      float hold = line;

      float height = foldout + timeFields + graph + graphCaption + hold;
      return height;
    }

    private static readonly GUIContent[] adsrLabels = {
      new GUIContent("A"),
      new GUIContent("D"),
      new GUIContent("S"),
      new GUIContent("R")
    };

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => GetHeight();

    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
      var line = EditorGUIUtility.singleLineHeight;
      var space = EditorGUIUtility.standardVerticalSpacing;

      Rect foldoutRect = rect;
      foldoutRect.height = line;
      foldoutShown = EditorGUI.Foldout(foldoutRect, foldoutShown, label, true);
      
      if (foldoutShown)
      {
        EditorGUI.indentLevel++;
        rect = EditorGUI.IndentedRect(rect);
        EditorGUI.indentLevel--;

        Rect timeFieldsRect = rect;
        timeFieldsRect.height = line;
        timeFieldsRect.y = foldoutRect.yMax + space;
        timeFieldsRect = DrawEnvelopeParameterFields(timeFieldsRect, property, label);

        Rect graphRect = rect;
        graphRect.y = timeFieldsRect.yMax + (3 * space);
        graphRect.height = graphHeight;
        DrawGraph(property, graphRect);
        float graphCaption = line + space;

        Rect holdRect = rect;
        holdRect.y = graphRect.yMax + graphCaption + space;
        holdRect.height = line;
        DrawSustainValue(holdRect, property);
      }
    }

    static void DrawSustainValue(Rect rect, SerializedProperty prop)
    {
      float gap = 12;
      float checkboxWidth = 64;
      Rect valueRect = rect;
      valueRect.width = rect.width - checkboxWidth - gap;
      EditorGUI.BeginChangeCheck();
      SerializedProperty sustainValueProp = prop.FindPropertyRelative(nameof(Envelope.sustainValue));
      EditorGUI.PropertyField(valueRect, sustainValueProp);
      if (EditorGUI.EndChangeCheck())
      {
        float safeValue = Mathf.Clamp01(sustainValueProp.floatValue);
        SerializedEnvelopeHelpers.UpdateSustainValue(prop, safeValue);
      }

      Rect checkboxRect = rect;
      checkboxRect.width = checkboxWidth;
      checkboxRect.x = valueRect.xMax + gap;

      var label = new GUIContent("Hold");
      SerializedProperty holdSustainProp = prop.FindPropertyRelative(nameof(Envelope.holdSustain));

      Rect checkboxLabelRect = checkboxRect;
      EditorGUI.BeginProperty(rect, label, holdSustainProp);
      EditorGUI.LabelField(checkboxLabelRect, label);
      EditorGUI.EndProperty();

      Rect checkboxValueRect = checkboxRect;
      checkboxValueRect.width *= 0.4f;
      checkboxValueRect.x = rect.xMax - 16;
      EditorGUI.PropertyField(checkboxValueRect, holdSustainProp, GUIContent.none);
    }

    static Rect DrawEnvelopeParameterFields(Rect rect, SerializedProperty prop, GUIContent label)
    {
      EditorGUI.LabelField(rect, new GUIContent("Time"));

      if (EditorGUIUtility.wideMode)
      {
        rect.x += EditorGUIUtility.labelWidth;
        rect.width -= EditorGUIUtility.labelWidth;
      }
      else
      {
        // Narrow mode: Move to the next line.
        rect.y += rect.height;

        // Indent the following controls.
        EditorGUI.indentLevel++;
        rect = EditorGUI.IndentedRect(rect);
        EditorGUI.indentLevel--;
      }

      // Field rect
      var r = rect;
      r.width = (r.width - 6) / 4;

      // Change the label width in the following fields.
      var originalLabelWidth = EditorGUIUtility.labelWidth;
      EditorGUIUtility.labelWidth = 12;

      SerializedProperty[] timeProperties = new SerializedProperty[]
      {
        prop.FindPropertyRelative(nameof(Envelope.attackTime)),
        prop.FindPropertyRelative(nameof(Envelope.decayTime)),
        prop.FindPropertyRelative(nameof(Envelope.sustainTime)),
        prop.FindPropertyRelative(nameof(Envelope.releaseTime)),
      };
      for (var i = 0; i < 4; i++)
      {
        var timeProperty = timeProperties[i];

        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(r, timeProperty, adsrLabels[i]);

        if (EditorGUI.EndChangeCheck())
          timeProperty.floatValue = Mathf.Max(0, timeProperty.floatValue); // S

        r.x += r.width + 2;
      }
      EditorGUIUtility.labelWidth = originalLabelWidth;
      return rect;
    }

    static void DrawGraph(SerializedProperty property, Rect rect)
    {
      float width = rect.width;
      float attackTime = property.FindPropertyRelative(nameof(Envelope.attackTime)).floatValue;
      if (attackTime > 0)
        width -= minGraphWidth;

      float decayTime = property.FindPropertyRelative(nameof(Envelope.decayTime)).floatValue;
      if (decayTime > 0)
        width -= minGraphWidth;

      bool holdSustain = property.FindPropertyRelative(nameof(Envelope.holdSustain)).boolValue;
      float sustainTime = property.FindPropertyRelative(nameof(Envelope.sustainTime)).floatValue;
      if (sustainTime > 0 || holdSustain)
        width -= minGraphWidth;

      float releaseTime = property.FindPropertyRelative(nameof(Envelope.releaseTime)).floatValue;
      if (releaseTime > 0)
        width -= minGraphWidth;

      float totalTime = attackTime + decayTime + sustainTime + releaseTime;
      float totalWidth = width / totalTime;
      if (attackTime > 0)
      {
        rect = DrawAttackGraph(property, rect, totalWidth);
      }
      if (decayTime > 0)
      {
        rect = DrawDecayGraph(property, rect, totalWidth);
      }
      if (sustainTime > 0 || holdSustain)
      {
        rect = DrawSustainGrap(property, rect, totalWidth);
      }
      if (releaseTime > 0)
      {
        DrawReleaseGraph(property, rect, totalWidth);
      }
    }

    private static Rect DrawSustainGrap(SerializedProperty property, Rect rect, float totalWidth)
    {
      var sustainTime = property.FindPropertyRelative(nameof(Envelope.sustainTime)).floatValue;
      rect.width = sustainTime * totalWidth + minGraphWidth;
      var sustainValue = property.FindPropertyRelative(nameof(Envelope.sustainValue)).floatValue;
      DragSustainValue(rect, property);
      EditorGUIUtility.DrawCurveSwatch(
        rect,
        AnimationCurve.Constant(0, 1, sustainValue),
        null,
        sustainColor,
        curveBackgroundColor,
        ranges
      );
      DrawGraphCaption(rect, property.FindPropertyRelative(nameof(Envelope.sustainValue)), new GUIContent("Sustain"));

      rect.x += rect.width;
      return rect;
    }

    private static void DragSustainValue(Rect rect, SerializedProperty property)
    {
      Event evt = Event.current;
      int id = GUIUtility.GetControlID(sustainHash, FocusType.Passive, rect);
      switch (evt.GetTypeForControl(id))
      {
        case EventType.MouseDown:
          if (rect.Contains(evt.mousePosition) && evt.button == 0)
          {
            GUIUtility.hotControl = id;
            dragStartValue = property.FindPropertyRelative(nameof(Envelope.sustainValue)).floatValue;
          }
          break;
        case EventType.MouseUp:
          if (GUIUtility.hotControl == id)
          {
            GUIUtility.hotControl = 0;
          }
          break;
        case EventType.KeyDown:
          if (GUIUtility.hotControl == id && evt.keyCode == KeyCode.Escape)
          {
            SerializedEnvelopeHelpers.UpdateSustainValue(property, dragStartValue);
            GUI.changed = true;
            GUIUtility.hotControl = 0;
            evt.Use();
          }
          break;
        case EventType.Repaint:
          EditorGUIUtility.AddCursorRect(rect, MouseCursor.ResizeVertical);
          break;
      }

      if (evt.isMouse && GUIUtility.hotControl == id)
      {
        float relativeY = evt.mousePosition.y - rect.y;
        float value = Mathf.Clamp01(1 - (relativeY / rect.height));
        SerializedEnvelopeHelpers.UpdateSustainValue(property, value);
        GUI.changed = true;
        Event.current.Use();
      }
    }

    private static Rect DrawAttackGraph(SerializedProperty property, Rect rect, float totalWidth)
    {
      EditorGUI.BeginChangeCheck();
      var attackTime = property.FindPropertyRelative(nameof(Envelope.attackTime)).floatValue;
      rect.width = attackTime * totalWidth + minGraphWidth;
      var inputCurve = property.FindPropertyRelative(nameof(Envelope.attackCurve)).animationCurveValue;
      AnimationCurve curveValue = EditorGUI.CurveField(
        rect,
        inputCurve,
        attackColor,
        ranges
      );
      if (EditorGUI.EndChangeCheck())
      {
        SerializedEnvelopeHelpers.UpdateAttackCurve(property, curveValue);
      }
      DrawGraphCaption(rect, property.FindPropertyRelative(nameof(Envelope.attackCurve)), new GUIContent("Attack"));
      
      rect.x += rect.width;
      return rect;
    }

    private static void DrawGraphCaption(Rect rect, SerializedProperty property, GUIContent label)
    {
      var line = EditorGUIUtility.singleLineHeight;
      rect.yMin = rect.yMax;
      rect.height = line;
      EditorGUI.BeginProperty(rect, label, property);
      EditorGUI.LabelField(rect, label);
      EditorGUI.EndProperty();
    }

    private static Rect DrawDecayGraph(SerializedProperty property, Rect rect, float totalWidth)
    {
      EditorGUI.BeginChangeCheck();
      var decayTime = property.FindPropertyRelative(nameof(Envelope.decayTime)).floatValue;
      rect.width = decayTime * totalWidth + minGraphWidth;
      var inputCurve = property.FindPropertyRelative(nameof(Envelope.decayCurve)).animationCurveValue;
      AnimationCurve curveValue = EditorGUI.CurveField(
        rect,
        inputCurve,
        decayColor,
        ranges
      );
      if (EditorGUI.EndChangeCheck())
      {
        SerializedEnvelopeHelpers.UpdateDecayCurve(property, curveValue);
      }
      DrawGraphCaption(rect, property.FindPropertyRelative(nameof(Envelope.decayCurve)), new GUIContent("Decay"));

      rect.x += rect.width;
      return rect;
    }

    private static Rect DrawReleaseGraph(SerializedProperty property, Rect rect, float totalWidth)
    {
      EditorGUI.BeginChangeCheck();
      var releaseTime = property.FindPropertyRelative(nameof(Envelope.releaseTime)).floatValue;
      rect.width = releaseTime * totalWidth + minGraphWidth;
      var inputCurve = property.FindPropertyRelative(nameof(Envelope.releaseCurve)).animationCurveValue;
      AnimationCurve curveValue = EditorGUI.CurveField(
        rect,
        inputCurve,
        releaseColor,
        ranges
      );
      if (EditorGUI.EndChangeCheck())
      {
        SerializedEnvelopeHelpers.UpdateReleaseCurve(property, curveValue);
      }
      DrawGraphCaption(rect, property.FindPropertyRelative(nameof(Envelope.releaseCurve)), new GUIContent("Release"));

      rect.x += rect.width;
      return rect;
    }
  }
}