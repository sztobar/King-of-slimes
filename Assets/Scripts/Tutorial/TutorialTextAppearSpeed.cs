using System.Collections;
using UnityEngine;

public class TutorialTextAppearSpeed : MonoBehaviour, IGameplayComponent
{
  public float normalTextSpeed = 1f;
  public float fasterTextSpeed = 2f;

  public DialogFrameText dialogFrameText;
  private MenuInput input;

  public void Inject(GameplayManager gameplayManager)
  {
    input = gameplayManager.input;
    dialogFrameText = gameplayManager.tutorialUI.dialogFrameText;
  }

  public void InputUpdate()
  {
    dialogFrameText.TimeMultiplier = input.confirm.IsHeld() ? fasterTextSpeed : normalTextSpeed;
    if (input.back.IsPressed())
    {
      input.back.Use();
      dialogFrameText.OnCancel();
    }
    if (input.confirm.IsPressed())
    {
      input.confirm.Use();
      dialogFrameText.OnConfirm();
    }
  }
}
