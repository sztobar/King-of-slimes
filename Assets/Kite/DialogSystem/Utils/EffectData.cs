using System.Collections.Generic;
using System.Globalization;

public class EffectData {

  public TextEffectType effectType;
  public int startIndex;
  public int endIndex;
  public readonly Dictionary<string, string> parameters = new Dictionary<string, string>();

  public EffectData(EffectData original) {
    effectType = original.effectType;
    startIndex = original.startIndex;
    endIndex = original.endIndex;
    parameters = original.parameters;
  }
  public EffectData(EffectData original, int endIndex) {
    effectType = original.effectType;
    startIndex = original.startIndex;
    parameters = original.parameters;
    this.endIndex = endIndex;
  }

  public EffectData(TextEffectType effectType, int startIndex, int endIndex) {
    this.effectType = effectType;
    this.startIndex = startIndex;
    this.endIndex = endIndex;
  }

  public EffectData(TextEffectType effectType, int startIndex, int endIndex, Dictionary<string, string> parameters) {
    this.effectType = effectType;
    this.startIndex = startIndex;
    this.endIndex = endIndex;
    this.parameters = parameters;
  }

  public EffectData() {
  }

  public float ReadFloat(string key, float defaultValue) {
    if (parameters.ContainsKey(key)) {
      return float.Parse(parameters[key], CultureInfo.InvariantCulture);
    }
    return defaultValue;
  }
  }
