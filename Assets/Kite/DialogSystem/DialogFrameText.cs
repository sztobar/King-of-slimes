using UnityEngine;
using TMPro;
using System;

public class DialogFrameText : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI textMesh;
  [SerializeField] private DialogFrameCursorNext dialogFrameCursorNext;

  [SerializeField] private TextEffectAnimationController textEffectAnimationController;
  private TextEffectAppearController textEffectAppearController;
  private DialogWithEffects dialogWithEffects;

  private bool finished;
  private bool showingText;
  private bool hasNextPage;
  private int currentPage;

  public bool IsFinished() => finished;

  public float TimeMultiplier { get; set; }

  public void OnCancel()
  {
    if (showingText)
    {
      FinishPage();
    }
    else
    {
      OnConfirm();
    }
  }

  public void OnConfirm()
  {
    if (!showingText)
    {
      if (hasNextPage)
      {
        StartNextPage();
      }
      else
      {
        finished = true;
      }
    }
  }

  public void StartText(string text)
  {
    TextEffectsParserConfig config = new TextEffectsParserConfig(
      defaultAppear: new TextEffectConfig(TextEffectType.Appear)
    );
    dialogWithEffects = TextEffectsParser.Parse(text, config);
    textMesh.SetText(dialogWithEffects.PlainText);
    textMesh.ForceMeshUpdate();

    textEffectAnimationController = new TextEffectAnimationController(
      textMesh,
      dialogWithEffects.GetAnimationEffects()
    );
    textEffectAppearController = new TextEffectAppearController(
      textMesh,
      dialogWithEffects.GetAppearEffects()
    );

    StartFirstPage();
  }

  private void FinishPage()
  {
    textEffectAppearController.ForcePageFinish();
    showingText = false;
    dialogFrameCursorNext.Show();

    TMP_TextInfo textInfo = textMesh.textInfo;
    hasNextPage = currentPage < textInfo.pageCount - 1;
  }

  private void OnPageFinished()
  {
    showingText = false;
    dialogFrameCursorNext.Show();
  }

  private void StartFirstPage()
  {
    finished = false;
    currentPage = 0;
    OnPageChange();
  }

  private void StartNextPage()
  {
    currentPage++;
    OnPageChange();
    textEffectAppearController.StartNextPage();
  }

  private void OnPageChange()
  {
    textMesh.pageToDisplay = currentPage + 1;
    TMP_TextInfo textInfo = textMesh.textInfo;

    hasNextPage = currentPage < textInfo.pageCount - 1;
    textMesh.maxVisibleCharacters = 0;
    showingText = true;
    dialogFrameCursorNext.Hide();
  }

  private void Update()
  {
    if (showingText)
    {
      float dt = Time.unscaledDeltaTime * TimeMultiplier;
      textEffectAppearController.Update(dt);
      if (textEffectAppearController.IsOnPageBreak())
      {
        OnPageFinished();
      }
      textMesh.ForceMeshUpdate();
      textEffectAppearController.AnimationUpdate();
      textEffectAnimationController.Update();
    }
    else
    {
      textMesh.ForceMeshUpdate();
      textEffectAnimationController.Update();
    }
  }
}
