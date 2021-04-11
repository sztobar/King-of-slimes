using Kite;
using System;
using System.Collections;
using UnityEngine;

public class GameplayStateMachine : StackFSM<GameplayState>, IGameplayComponent
{
  public GameplayPlayState play;
  public GameplayPauseState pause;
  public GameplayTutorialState tutorial;
  public GameplayCutsceneState cutscene;
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
      cutscene,
      cameraTransitionState,
    };
}
