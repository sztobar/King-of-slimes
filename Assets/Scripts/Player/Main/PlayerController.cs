using UnityEngine;

public class PlayerController : MonoBehaviour, IGameplayComponent
{
  public PlayerDI di;
  public PlayerInput input;
  public bool godMode;

  public void Inject(GameplayManager gameplayManager)
  {
#if !UNITY_EDITOR
    godMode = false;
#endif
    di.Inject(this);
  }
}
