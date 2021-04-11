using UnityEngine;
using UnityEngine.Events;

public class BaseMenuOptionBehavior : MenuOptionBehavior {

  [SerializeField]
  private Transform selectedIndicator;

  [SerializeField]
  private UnityEvent EventOnConfirm;

  private bool isSelected = false;

  public override void OnConfirm() {
    EventOnConfirm.Invoke();
  }

  public override void OnDeselect() {
    isSelected = false;
  }

  public override void OnSelect() {
    isSelected = true;
    selectedIndicator.position = new Vector3(selectedIndicator.position.x, transform.position.y);
  }

  private void Update() {
    if (isSelected) {
      selectedIndicator.position = new Vector3(selectedIndicator.position.x, transform.position.y);
    }
  }
}
