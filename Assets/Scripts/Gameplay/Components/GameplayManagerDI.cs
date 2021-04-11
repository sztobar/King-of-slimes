using System.Collections;
using UnityEngine;

public class GameplayManagerDI : MonoBehaviour
{
  public PlayerController player;

  public MenuInput input;
  public FadeTransition fadeTransition;
  public GameplayStateMachine stateMachine;
  public TutorialUI tutorialUI;
  public TutorialTextAppearSpeed tutorialTextAppearSpeed;
  public GameplayCamera playerCamera;

  [HideInInspector]
  public GameplayManager manager;

  public void Init(GameplayManager manager)
  {
    //this.manager = manager;
    //foreach (var component in GetGameplayComponents())
    //  component.Inject(this);
  }

  private IGameplayComponent[] GetGameplayComponents() =>
    new IGameplayComponent[]
    {
      player,
      fadeTransition,
      playerCamera,
      tutorialUI,
      stateMachine,
      tutorialTextAppearSpeed
    };
}