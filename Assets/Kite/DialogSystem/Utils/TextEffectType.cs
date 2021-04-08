using System;

public enum TextEffectType {
  // animation effects
  Shake,
  Wiggle,
  Wave,
  Swing,
  // appear effects
  Pause,
  Appear,
  Fade,
  Size,
  // additional effects
  PageBreak
};

public static class TextEffectHelpers {

  private static TextEffectType[] appearTextEffects = new TextEffectType[] {
    TextEffectType.Pause,
    TextEffectType.Appear,
    TextEffectType.Fade,
    TextEffectType.Size,
    TextEffectType.PageBreak
  };


  private static TextEffectType[] autoCloseTextEffects = new TextEffectType[] {
    TextEffectType.Pause,
    TextEffectType.PageBreak
  };

  public static bool IsAppearTextEffect(this TextEffectType effect) =>
    Array.IndexOf(appearTextEffects, effect) != -1;

  public static bool IsAutoClosingTag(this TextEffectType effect) =>
    Array.IndexOf(autoCloseTextEffects, effect) != -1;
}
