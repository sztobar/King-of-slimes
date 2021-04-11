using Kite;
using System;
using System.Collections;
using UnityEngine;

public class GameplayStateMachine : StackFSM<GameplayState>, IGameplayComponent
{
  public GameplayPlayState play;
  public GameplayPauseState pause;
  public GameplayFadeState fade;
  public GameplayTutorialState tutorial;
  public GameplayCutsceneState cutscene;
  public GameplayCameraCatchUpState cameraCatchUpState;
  public GameplayCameraFadeState cameraFadeState;
  public GameplayCameraTransitionState cameraTransitionState;

  public void PushTutorial(ScriptableTutorial data)
  {
    tutorial.data = data;
    PushState(tutorial);
  }

  internal void PushCutscene(GameplayCutscene cutsceneToPlay) {
    cutscene.cutscene = cutsceneToPlay;
    PushState(cutscene);
  }

  public void StartFade(Action callback, bool stopTime = false)
  {
    fade.betweenFadeCallback = callback;
    fade.stopTime = stopTime;
    PushState(fade);
  }

  public void PushCameraCatchUp() => PushState(cameraCatchUpState);
  public void PushCameraFade() => PushState(cameraFadeState);
  public void PushCameraTransition() => PushState(cameraTransitionState);

  public void Inject(GameplayManager gameplayManager)
  {
    foreach (GameplayState state in GetStates())
      state.Inject(gameplayManager);

    SetState(play);
  }

  private GameplayState[] GetStates() =>
    new GameplayState[]
    {
      play,
      tutorial,
      pause,
      fade,
      cutscene,
      cameraCatchUpState,
      cameraFadeState,
      cameraTransitionState,
    };
}
