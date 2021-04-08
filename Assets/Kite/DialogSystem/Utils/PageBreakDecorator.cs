using TMPro;
using System.Collections.Generic;

public static class PageBreakDecorator {

  public static List<int> GetPageEndIndexes(TMP_Text textMesh) {
    TMP_TextInfo textInfo = textMesh.textInfo;
    int pageCount = textInfo.pageCount;
    List<int> pageEndIndexes = new List<int>(pageCount);
    for (int i = 0; i < pageCount; i++) {
      TMP_PageInfo pageInfo = textInfo.pageInfo[i];
      int pageEndIndex = pageInfo.lastCharacterIndex + 1;
      pageEndIndexes.Add(pageEndIndex);
    }
    return pageEndIndexes;
  }

  public static void AddPageBreaks(TMP_Text textMesh, List<EffectData> effects) {
    List<int> pageEndIndexes = GetPageEndIndexes(textMesh);
    AddPageBreaks(pageEndIndexes, effects);
  }

  public static void AddPageBreaks(List<int> pageEndIndexes, List<EffectData> effects) {
    for (int i = 0; i < pageEndIndexes.Count; i++) {
      int pageBreakIndex = pageEndIndexes[i];
      int effectIndex = FindFirstEffectIndexAtTextIndex(effects, pageBreakIndex);
      if (effectIndex == -1) {
        continue;
      }
      EffectData afterPageBreakEffect = effects[effectIndex];
      EffectData pageBreakEffect = new EffectData(
        effectType: TextEffectType.PageBreak,
        startIndex: pageBreakIndex,
        endIndex: pageBreakIndex
      );
      if (afterPageBreakEffect.startIndex == pageBreakIndex) {
        effects.Insert(effectIndex, pageBreakEffect);
      } else if (afterPageBreakEffect.endIndex > pageBreakIndex) {
        EffectData beforeBreakEffect = new EffectData(afterPageBreakEffect, endIndex: pageBreakIndex);
        effects.Insert(effectIndex, beforeBreakEffect);
        effects.Insert(effectIndex + 1, pageBreakEffect);
        afterPageBreakEffect.startIndex = pageBreakIndex;
      } else {
        effects.Add(pageBreakEffect);
      }
    }
  }

  private static int FindFirstEffectIndexAtTextIndex(List<EffectData> effects, int pageBreakIndex) {
    int effectsCount = effects.Count;
    for (int i = 0; i < effectsCount; i++) {
      EffectData effect = effects[i];
      if (effect.startIndex == pageBreakIndex || effect.endIndex > pageBreakIndex) {
        return i;
      }
    }
    return effectsCount - 1;
  }
}
