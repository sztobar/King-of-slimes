using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class GameplayManager : MonoBehaviour
{
  public static GameplayManager instance;

  public PlayerController player;
  public MenuInput input;
  public FadeTransition fadeTransition;
  
  [FormerlySerializedAs("stateMachine")]
  public GameplayStateMachine fsm;
  
  public TutorialUI tutorialUI;
  public TutorialTextAppearSpeed tutorialTextAppearSpeed;
  public PlayCamera playCamera;
  public CameraController cameraController;

  private void Awake()
  {
    if (!instance)
    {
      instance = this;
      Init();
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private void Init()
  {
    foreach (IGameplayComponent component in GetGameplayComponents())
      component.Inject(this);
  }

  private IGameplayComponent[] GetGameplayComponents() =>
    new IGameplayComponent[]
    {
      player,
      tutorialUI,
      fsm,
      tutorialTextAppearSpeed,
    };
}
