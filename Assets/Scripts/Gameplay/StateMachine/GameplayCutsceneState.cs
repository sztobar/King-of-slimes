using Cinemachine;
using UnityEditor;
using UnityEngine;

public class GameplayCutsceneState : GameplayState
{
  public SkipCutsceneText skipCutsceneText;
  //public GameplayCutsceneCamera cutsceneCamera;
  public CutsceneCamera cutsceneCamera;
  public PlayCamera playCamera;

  [HideInInspector] public GameplayCutscene cutscene;
  private GameplayStateMachine fsm;
  private CutsceneInput cutsceneInput;

  public override void StateStart()
  {
    EnableInput();
    cutscene.CutsceneStart();
    cutsceneCamera.TransitionTo(cutscene.GetBeginCameraState());
  }

  private void EnableInput()
  {
    if (cutsceneInput == null)
      cutsceneInput = new CutsceneInput();

    cutsceneInput.EnableInputActions();
    cutsceneInput.OnAnyAction += OnAnyAction;
    cutsceneInput.OnSkipAction += OnSkipAction;
  }

  private void OnAnyAction()
  {
    if (!skipCutsceneText.IsShowing())
    {
      skipCutsceneText.StartShow();
    }
  }

  private void OnSkipAction()
  {
    if (skipCutsceneText.IsShowing())
    {
      cutscene.Skip();
    }
    else
    {
      OnAnyAction();
    }
  }

  public override void StatePause()
  {
    cutsceneInput.DisableInputActions();
  }

  public override void StateResume()
  {
    cutsceneInput.EnableInputActions();
  }

  public override void StateUpdate()
  {
    float dt = Time.unscaledDeltaTime;
    if (cutsceneCamera.HasTransitionToCutscene())
    {
      cutsceneCamera.TransitionToCutsceneUpdate(dt);
      return;
    }

    if (cutscene.IsCutsceneFinished())
    {
      fsm.PopState();
      playCamera.TransitionToSelf();
    }
    else
    {
      cutscene.CutsceneUpdate(dt);
    }
  }

  public override void StateExit()
  {
    skipCutsceneText.ForceHide();
    DisableInput();
  }

  public override void Inject(GameplayManager gameplayManager)
  {
    fsm = gameplayManager.fsm;
  }

  private void OnEnable()
  {
    if (fsm && fsm.IsHead(this))
    {
      EnableInput();
    }
  }

  private void OnDisable()
  {
    DisableInput();
  }

  private void DisableInput()
  {
    if (cutsceneInput != null)
    {
      cutsceneInput.DisableInputActions();
      cutsceneInput.OnSkipAction -= OnSkipAction;
      cutsceneInput.OnAnyAction -= OnAnyAction;
    }
  }
}
