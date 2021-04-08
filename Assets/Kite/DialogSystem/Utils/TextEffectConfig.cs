using System.Collections.Generic;

public struct TextEffectConfig {
  
  public TextEffectType effectType;
  public Dictionary<string, string> parameters;

  public TextEffectConfig(TextEffectType effectType) {
    this.effectType = effectType;
    parameters = new Dictionary<string, string>();
  }

  public TextEffectConfig(TextEffectType effectType, Dictionary<string, string> parameters) {
    this.effectType = effectType;
    this.parameters = parameters;
  }
}
