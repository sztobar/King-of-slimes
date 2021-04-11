using UnityEngine;
using UnityEngine.UI;
using PoisonState = PlayerPoisonModule.PoisonState;

public class BreathBarUI : MonoBehaviour, PlayerUI.IInjectable {

  [SerializeField] private Image bgBar;
  [SerializeField] private Image breathBar;
  [SerializeField] private Image intervalBar;


  private PlayerPoisonModule poison;
  private Vector2 fullBarSize;

  private void Awake() {
    fullBarSize = breathBar.rectTransform.sizeDelta;
  }

  public void Inject(PlayerUI playerUI) {
    UpdateUnit(playerUI.SelectedUnitController);
  }

  public void UpdateUnit(PlayerUnitController unitController) {
    poison = unitController.di.poison;
  }

  private void Update() {
    PoisonState poisonState = poison.State;
    switch (poisonState) {
      case PoisonState.None:
        HideBars();
        break;
      case PoisonState.InPoisonBeforeHit:
      case PoisonState.OutOfPoison:
        ShowBreathBar();
        break;
      case PoisonState.InPoisonInterval:
      default:
        ShowIntervalBar();
        break;
    }
  }

  private void ShowIntervalBar() {
    float poisonBreathNormal = poison.GetBreathNormal();

    bgBar.enabled = true;

    breathBar.enabled = true;
    breathBar.rectTransform.sizeDelta = fullBarSize;

    intervalBar.enabled = true;
    intervalBar.rectTransform.sizeDelta = new Vector2(fullBarSize.x * poisonBreathNormal, fullBarSize.y);
  }

  private void ShowBreathBar() {
    float poisonBreathNormal = poison.GetBreathNormal();

    bgBar.enabled = true;

    breathBar.enabled = true;
    breathBar.rectTransform.sizeDelta = new Vector2(fullBarSize.x * poisonBreathNormal, fullBarSize.y);

    intervalBar.enabled = false;
  }

  private void HideBars() {
    bgBar.enabled = false;
    breathBar.enabled = false;
    intervalBar.enabled = false;
  }
}
