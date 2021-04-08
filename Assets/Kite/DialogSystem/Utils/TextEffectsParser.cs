using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Linq;

public class TextEffectsParser {

  private static readonly string OPEN_TAG_END = "[/";

  private TextEffectsParserConfig config;

  private List<EffectData> animationEffects;
  private List<EffectData> appearEffects;

  private EffectData openedAnimationEffect;
  private Stack<EffectData> appearEffectsStack;

  int tagsOffset;
  int plainTextLength;

  private string rawText;
  private string plainText;


  public static DialogWithEffects Parse(string inputText, TextEffectsParserConfig config = default) {
    TextEffectsParser parser = new TextEffectsParser(config);
    try {
      parser.ParseInput(inputText);
      return new DialogWithEffects(parser.plainText, parser.appearEffects, parser.animationEffects);
    } catch (Exception e) {
      Debug.LogError($"[DialogWithEffects]: Cannot parse input: `{inputText}`. {e.Message}");
    }
    return new DialogWithEffects(inputText);
  }

  private TextEffectsParser(TextEffectsParserConfig config) {
    this.config = config;
  }

  private void ParseInput(string inputText) {
    ResetState();
    rawText = inputText;
    Regex rx = GetPatternRegex();
    plainText = rx.Replace(inputText, "");
    plainTextLength = plainText.Length;
    UseParserConfig();
    MatchCollection matches = rx.Matches(inputText);

    foreach (Match match in matches) {
      ParseTextEffectTag(match);
    }

    CloseAppearEffectFromStack();
    CloseOpenedAnimationEffect();
  }

  private void ParseTextEffectTag(Match match) {
    GroupCollection groups = match.Groups;
    Group effectNameGroup = groups[1];
    TextEffectType effectType = ParseEffectType(effectNameGroup.Value);
    int indexInPlainText = match.Index - tagsOffset;

    if (IsOpeningTag(match)) {
      Group effectParamsGroup = groups[2];
      OnTagOpen(effectType, indexInPlainText, effectParamsGroup);
    } else {
      OntagClose(effectType, indexInPlainText);
    }
    tagsOffset += match.Value.Length;
  }

  private void OntagClose(TextEffectType effectType, int indexInPlainText) {
    if (effectType.IsAppearTextEffect()) {
      OnAppearEffectClose(effectType, indexInPlainText);
    } else {
      OnAnimationEffectClose(effectType, indexInPlainText);
    }
  }

  private void OnTagOpen(TextEffectType effectType, int indexInPlainText, Group effectParamsGroup) {
    EffectData newEffect = new EffectData(
      effectType: effectType,
      startIndex: indexInPlainText,
      endIndex: plainTextLength,
      parameters: ParseEffectParameters(effectParamsGroup.Captures)
    );

    if (effectType.IsAppearTextEffect()) {
      OnAppearEffectOpen(newEffect);
    } else {
      OnAnimationEffectOpen(newEffect);
    }
  }

  private void CloseOpenedAnimationEffect() {
    if (openedAnimationEffect != null) {
      openedAnimationEffect.endIndex = plainTextLength;
      animationEffects.Add(openedAnimationEffect);
      openedAnimationEffect = null;
    }
  }

  private void CloseAppearEffectFromStack() {
    if (appearEffectsStack.Count > 0) {
      EffectData effectsStackHead = appearEffectsStack.Pop();
      if (effectsStackHead.startIndex < plainTextLength) {
        appearEffects.Add(effectsStackHead);
      }
      appearEffectsStack.Clear();
    }
  }

  private void OnAppearEffectClose(TextEffectType effectType, int indexInPlainText) {
    AssertNotAutoCloseEffectClose(effectType);
    if (appearEffectsStack.Count == 0) {
      throw new Exception($"Cannot close {effectType} tag. No tag is opened");
    }
    EffectData currentEffectData = appearEffectsStack.Pop();

    if (currentEffectData.effectType != effectType) {
      throw new Exception($"Cannot close {effectType} tag. {currentEffectData.effectType} tag must be closed first.");
    }

    currentEffectData.endIndex = indexInPlainText;
    int effectLength = currentEffectData.endIndex - currentEffectData.startIndex;

    if (effectLength > 0) {
      appearEffects.Add(currentEffectData);
      if (appearEffectsStack.Count > 0) {
        EffectData previousEffectData = appearEffectsStack.Peek();
        previousEffectData.startIndex = indexInPlainText;
      }
    }
  }

  private void OnAnimationEffectClose(TextEffectType effectType, int indexInPlainText) {
    EffectData currentEffectData = openedAnimationEffect;
    openedAnimationEffect = null;

    if (currentEffectData == null) {
      throw new Exception($"Cannot close {effectType} tag. No tag is opened");
    }
    if (currentEffectData.effectType != effectType) {
      throw new Exception($"Cannot close {effectType} tag. {currentEffectData.effectType} tag must be closed first.");
    }
    currentEffectData.endIndex = indexInPlainText;
    int effectLength = currentEffectData.endIndex - currentEffectData.startIndex;

    if (effectLength > 0) {
      animationEffects.Add(currentEffectData);
    }
  }

  private void ResetState() {
    tagsOffset = 0;
    animationEffects = new List<EffectData>();
    openedAnimationEffect = null;
    appearEffects = new List<EffectData>();
    appearEffectsStack = new Stack<EffectData>();
  }

  private void UseParserConfig() { 
    if (config.defaultAppear is TextEffectConfig defaultAppear) {
      appearEffectsStack.Push(new EffectData(
        effectType: defaultAppear.effectType,
        startIndex: 0,
        endIndex: plainTextLength,
        parameters: defaultAppear.parameters
      ));
    }
  }

  private static void AssertNotAutoCloseEffectClose(TextEffectType effectType) {
    if (effectType.IsAutoClosingTag()) {
      throw new Exception($"{effectType} tag cannot be closed");
    }
  }

  private void OnAnimationEffectOpen(EffectData newEffect) {
    if (openedAnimationEffect != null) {
      throw new Exception($"{openedAnimationEffect.effectType} tag must be closed, before opening new tag.");
    }
    openedAnimationEffect = newEffect;
  }

  private void OnAppearEffectOpen(EffectData newEffect) {
    if (HasAppearEffectOnStack()) {
      AddAppearEffectFromStack(newEffect.startIndex);
    }
    if (newEffect.effectType.IsAutoClosingTag()) {
      AddAutoCloseAppearEffect(newEffect.startIndex, newEffect);
    } else {
      appearEffectsStack.Push(newEffect);
    }
  }

  private void AddAutoCloseAppearEffect(int indexInPlainText, EffectData newEffect) {
    newEffect.endIndex = indexInPlainText;
    appearEffects.Add(newEffect);
  }

  private void AddAppearEffectFromStack(int indexInPlainText) {
    EffectData currentEffectData = appearEffectsStack.Peek();
    if (currentEffectData.startIndex != indexInPlainText) {
      EffectData effectCopy = new EffectData(currentEffectData, endIndex: indexInPlainText);
      appearEffects.Add(effectCopy);
      currentEffectData.startIndex = indexInPlainText;
    }
  }

  private bool HasAppearEffectOnStack() {
    return appearEffectsStack.Count > 0;
  }

  private static Regex GetPatternRegex() {
    string pattern = @"\[\/?(" + GetTagsRegex() + @")( \w+=[\d\.]+)*\]";
    Regex rx = new Regex(pattern);
    return rx;
  }

  private static bool IsOpeningTag(Match match) {
    return !match.Value.StartsWith(OPEN_TAG_END);
  }

  private static TextEffectType ParseEffectType(string value) {
    return (TextEffectType)Enum.Parse(typeof(TextEffectType), value, true);
  }

  private static Dictionary<string, string> ParseEffectParameters(CaptureCollection capturedParams) {
    Dictionary<string, string> results = new Dictionary<string, string>(capturedParams.Count);
    for (int i = 0; i < capturedParams.Count; i++) {
      string[] parts = capturedParams[i].Value.Split('=');
      results.Add(parts[0].Trim(), parts[1].Trim());
    }
    return results;
  }

  public static string GetTagsRegex() {
    string[] effects = Enum.GetNames(typeof(TextEffectType));
    for (int i = 0; i < effects.Length; i++) {
      effects[i] = effects[i].ToLower();
    }
    return string.Join("|", effects);
  }
}
