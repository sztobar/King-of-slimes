using System;
using System.Collections;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

  [SerializeField] private PlayerController player;
  [SerializeField] private HeartsCounterUI heartsCounter;
  [SerializeField] private SelectedTypeUI selectedType;
  [SerializeField] private BreathBarUI breathBar;

  private PlayerUnitHandler unitHandler;
  private PlayerUnitController selectedUnitController;

  public delegate void OnUnitUpdate(PlayerUnitController unitController);
  public event Action OnUpdateUI = delegate { };

  public PlayerUnitController SelectedUnitController => selectedUnitController;
  public PlayerController Controller => player;

  private void Start() {
    unitHandler = player.di.unitHandler;
    selectedUnitController = unitHandler.GetSelectedUnitController();
    unitHandler.OnSelectChange += HandleSelectChange;
    unitHandler.OnMergeChange += HandleMergeChange;

    heartsCounter.Inject(this);
    selectedType.Inject(this);
    breathBar.Inject(this);
  }

  private void HandleSelectChange(SlimeType type) {
    UpdateUI(unitHandler.GetSelectedUnitController());
  }

  private void HandleMergeChange() {
    UpdateUI(unitHandler.GetSelectedUnitController());
  }

  private void UpdateUI(PlayerUnitController unitController) {
    selectedUnitController = unitController;
    heartsCounter.UpdateUnit(unitController);
    selectedType.UpdateUnit(unitController);
    breathBar.UpdateUnit(unitController);
  }

  public interface IInjectable {
    void Inject(PlayerUI playerUI);
    void UpdateUnit(PlayerUnitController unitController);
  }
}