using TMPro;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class TextEffectAnimationController {

  [SerializeField] private List<WaveEffectAnimation> waveEffects = new List<WaveEffectAnimation>();
  [SerializeField] private List<ShakeEffectAnimation> shakeEffects = new List<ShakeEffectAnimation>();
  [SerializeField] private List<WiggleEffectAnimation> wiggleEffects = new List<WiggleEffectAnimation>();
  [SerializeField] private List<SwingEffectAnimation> swingEffects = new List<SwingEffectAnimation>();

  private readonly TMP_Text textMesh;
  private bool initialized;

  public TextEffectAnimationController(TMP_Text textMesh, IEnumerable<EffectData> effectsInput) {
    this.textMesh = textMesh;
    foreach (EffectData effect in effectsInput) {
      CreateEffect(effect);
    }
    initialized = true;
  }

  public void CreateEffect(EffectData data) {
    switch (data.effectType) {
      case TextEffectType.Shake:
        shakeEffects.Add(new ShakeEffectAnimation(textMesh, data));
        break;
      case TextEffectType.Wave:
        waveEffects.Add(new WaveEffectAnimation(textMesh, data));
        break;
      case TextEffectType.Wiggle:
        wiggleEffects.Add(new WiggleEffectAnimation(textMesh, data));
        break;
      case TextEffectType.Swing:
        swingEffects.Add(new SwingEffectAnimation(textMesh, data));
        break;
      default:
        break;
    }
  }

  public void Update() {
    if (!initialized) {
      return;
    }
    for (int i = 0; i < waveEffects.Count; i++) {
      waveEffects[i].Update();
    }
    for (int i = 0; i < shakeEffects.Count; i++) {
      shakeEffects[i].Update();
    }
    for (int i = 0; i < wiggleEffects.Count; i++) {
      wiggleEffects[i].Update();
    }
    for (int i = 0; i < swingEffects.Count; i++) {
      swingEffects[i].Update();
    }
  }

  // from https://youtu.be/nqAHJmpWLBg?t=901
  //public void AutoCreateTextAnimators() {
  //  Dictionary<Effect, ITextEffectAnimator> textEffects = new Dictionary<Effect, ITextEffectAnimator>();
  //  IEnumerable<Type> textEffectTypes = System.Reflection.Assembly.GetAssembly(typeof(ITextEffectAnimator)).GetTypes()
  //    .Where(t => typeof(ITextEffectAnimator).IsAssignableFrom(t) && t.IsAbstract == false);
  //  foreach(Type textEffectType in textEffectTypes) {
  //    ITextEffectAnimator textEffect = Activator.CreateInstance(textEffectType) as ITextEffectAnimator;
  //    textEffects.Add(textEffect.Effect, textEffect);
  //  }
  //}
}
