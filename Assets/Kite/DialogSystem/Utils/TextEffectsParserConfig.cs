using UnityEngine;
using System.Collections;

public struct TextEffectsParserConfig {

  public TextEffectConfig? defaultAppear;

  public TextEffectsParserConfig(TextEffectConfig defaultAppear) {
    this.defaultAppear = defaultAppear;
  }

}
