using Cinemachine;
using UnityEngine;

public class GameplayPlayState : GameplayState
{
  public CinemachineBrain cinemachineBrain;

  private PlayerController player;

  public override void StateExit()
  {
    //player.di.input.enabled = false;
    player.input.enabled = false;
  }

  public override void StateStart()
  {
    Time.timeScale = 1f;
    //player.di.input.enabled = true;
    player.input.enabled = true;
  }

  public override void StatePause()
  {
    Time.timeScale = 0f;
    //player.di.input.enabled = false;
    player.input.Pause();
    cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;
  }

  public override void StateResume()
  {
    Time.timeScale = 1f;
    //player.di.input.enabled = true;
    player.input.Resume();
    cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
  }

  public override void Inject(GameplayManager gameplayManager)
  {
    player = gameplayManager.player;
  }
}