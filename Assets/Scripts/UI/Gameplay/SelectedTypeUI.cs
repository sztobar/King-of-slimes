using System;
using UnityEngine;
using UnityEngine.InputSystem;
using SelectedTypeUIAnimation = UnityAnimatorStates.SelectedTypeUI;

public class SelectedTypeUI : MonoBehaviour, PlayerUI.IInjectable {

  public SlimeMap<SlimeTypeUI> slimeTypesUI;
  public SpriteRenderer crownUI;
  public EasyAnimator easyAnimator;

  private PlayerUnitHandler unitHandler;
  private SlimeTypeUI selectedSlimeUI;

  public void Inject(PlayerUI playerUI) {
    unitHandler = playerUI.Controller.di.unitHandler;
    UpdateLayout();
    OnSelectChange(unitHandler.SelectedType);
  }

  public void UpdateUnit(PlayerUnitController unitController) {
    OnSelectChange(unitHandler.SelectedType);
  }

  private void OnSelectChange(SlimeType selectedType) {
    SlimeTypeUI newSelectedSlimeUI = slimeTypesUI[selectedType];

    crownUI.transform.SetParent(newSelectedSlimeUI.transform, worldPositionStays: false);
    if (selectedSlimeUI)
      selectedSlimeUI.Deselect();
    selectedSlimeUI = newSelectedSlimeUI;
    selectedSlimeUI.Select();

    foreach (SlimeType slimeType in SlimeTypeHelpers.GetEnumerable())
    {
      if (unitHandler.GetSelectable(slimeType).IsUnlocked)
        slimeTypesUI[slimeType].Show();
      else
        slimeTypesUI[slimeType].Hide();
    }
  }

  private void OnEnable()
  {
    InputSystem.onDeviceChange += OnDeviceChange;
  }

  private void OnDeviceChange(InputDevice device, InputDeviceChange change)
  {
    if (device is Gamepad)
      UpdateLayout();
  }

  private void UpdateLayout()
  {
    if (Gamepad.current != null)
      easyAnimator.Play(SelectedTypeUIAnimation.GamepadLayout);
    else
      easyAnimator.Play(SelectedTypeUIAnimation.KeyboardLayout);
    
    if (selectedSlimeUI)
      crownUI.transform.SetParent(selectedSlimeUI.transform, worldPositionStays: false);
  }

  private void OnDisable()
  {
    InputSystem.onDeviceChange -= OnDeviceChange;
  }
}
