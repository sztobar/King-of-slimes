using System;
using System.Collections;
using UnityEngine;

public class TutorialUI : MonoBehaviour, IGameplayComponent
{
  public DialogFrameText dialogFrameText;

  public void Inject(GameplayManager gameplayManager)
  {
  }

  public void SetTutorial(ScriptableTutorial data)
  {
    gameObject.SetActive(true);
    dialogFrameText.StartText(data.text);
  }

  internal bool IsFinished() =>
    dialogFrameText.IsFinished();

  internal void Hide()
  {
    gameObject.SetActive(false);
  }
}
