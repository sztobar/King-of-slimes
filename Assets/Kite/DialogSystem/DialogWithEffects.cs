using System.Collections.Generic;

public class DialogWithEffects {

  private readonly List<EffectData> animationEffects;
  private readonly List<EffectData> appearEffects;
  private readonly string plainText;

  public string PlainText => plainText;

  public DialogWithEffects(string plainText, List<EffectData> appearEffects, List<EffectData> animationEffects) {
    this.plainText = plainText;
    this.appearEffects = appearEffects;
    this.animationEffects = animationEffects;
  }

  public DialogWithEffects(string plainText) {
    this.plainText = plainText;
    appearEffects = new List<EffectData>();
    animationEffects = new List<EffectData>();
  }

  public List<EffectData> GetAppearEffects() {
    return appearEffects;
  }

  public List<EffectData> GetAnimationEffects() {
    return animationEffects;
  }
}
