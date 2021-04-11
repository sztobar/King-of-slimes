using UnityEngine;

public abstract class MenuOptionBehavior : MonoBehaviour, IMenuOption {
  public abstract void OnConfirm();
  public abstract void OnDeselect();
  public abstract void OnSelect();
}
