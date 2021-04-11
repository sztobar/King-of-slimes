using Kite;
using System.Collections;
using UnityEngine;

public class GameplayTutorialState : GameplayState
{
  [HideInInspector] public ScriptableTutorial data;

  private GameplayStateMachine stateMachine;
  private TutorialUI ui;
  private TutorialTextAppearSpeed textSpeed;
  private MenuInput input;

  public override void StateExit()
  {
    input.enabled = true;
    ui.Hide();
  }

  public override void Inject(GameplayManager gameplayManager)
  {
    stateMachine = gameplayManager.fsm;
    input = gameplayManager.input;
    ui = gameplayManager.tutorialUI;
    textSpeed = gameplayManager.tutorialTextAppearSpeed;
  }

  public override void StateStart()
  {
    Time.timeScale = 0f;
    input.enabled = true;
    ui.SetTutorial(data);
  }

  public override void StateUpdate()
  {
    textSpeed.InputUpdate();
    if (ui.IsFinished())
    {
      stateMachine.PopState();
    }
  }

}
