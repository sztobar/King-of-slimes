using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TextEffectAppearController {

  private readonly TMP_Text textMesh;
  private readonly bool initialized;

  private readonly List<ITextEffectAppear> effectAppearsList = new List<ITextEffectAppear>();
  private int currentAppearEffectIndex;

  private ITextEffectAppear CurrentAppearEffect =>
    effectAppearsList[currentAppearEffectIndex];

  public TextEffectAppearController(TMP_Text textMesh, List<EffectData> appearEffects) {
    this.textMesh = textMesh;
    PageBreakDecorator.AddPageBreaks(textMesh, appearEffects);
    foreach (EffectData effectData in appearEffects) {
      CreateTextEffectAppear(effectData);
    }
    currentAppearEffectIndex = 0;
    initialized = true;
  }

  private void CreateTextEffectAppear(EffectData effectData) {
    ITextEffectAppear textEffectAppear = null;
    switch (effectData.effectType) {
      case TextEffectType.Appear:
        textEffectAppear = new BaseTextEffectAppear(textMesh, effectData);
        break;
      case TextEffectType.Size:
        textEffectAppear = new SizeTextEffectAppear(textMesh, effectData);
        break;
      case TextEffectType.Pause:
        textEffectAppear = new PauseTextEffectAppear(effectData);
        break;
      case TextEffectType.PageBreak:
        textEffectAppear = new PageBreakTextEffectAppear();
        break;
      default:
        Debug.LogError($"Unknown Appear Effect of type {effectData.effectType}");
        break;
    }
    if (textEffectAppear != null) {
      effectAppearsList.Add(textEffectAppear);
    }
  }

  public void Update(float timeDelta) {
    if (!initialized) {
      return;
    }
    CurrentAppearEffect.Update(timeDelta);
    AppearTextEffectUpdateEffects();
  }

  public void AnimationUpdate() {
    CurrentAppearEffect.AnimationUpdate();
  }

  private void AppearTextEffectUpdateEffects() {
    if (CurrentAppearEffect.IsEffectEnded()) {
      currentAppearEffectIndex++;
    }
  }

  public void ForcePageFinish() {
    while (!IsOnPageBreak()) {
      CurrentAppearEffect.ForceUpdate();
      AppearTextEffectUpdateEffects();
    }
  }

  public bool IsOnPageBreak() {
    Type pageBreakType = typeof(PageBreakTextEffectAppear);
    Type currentEffectType = CurrentAppearEffect.GetType();
    return currentEffectType.Equals(pageBreakType);
  }

  public void StartNextPage() {
    currentAppearEffectIndex++;
  }
}
