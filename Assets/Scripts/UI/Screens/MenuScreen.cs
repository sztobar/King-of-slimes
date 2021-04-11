using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Screens {
  public class MenuScreen : MonoBehaviour {

    [SerializeField]
    private int selectedOptionIndex = 0;

    [SerializeField]
    private MenuOptionBehavior[] options;

    private InputActions input;
    //private ThrottleAxis upDownInput;
    private readonly ThrottleAxis upDownInput = new ThrottleAxis();

    private MenuOptionBehavior SelectedOption => options[selectedOptionIndex];

    public void Open() {
      gameObject.SetActive(true);
    }

    public void Close() {
      gameObject.SetActive(false);
    }

    public void SetSelectedOptionIndex(int value) {
      if (selectedOptionIndex != value) {
        SelectedOption.OnDeselect();
        selectedOptionIndex = Mathf.Clamp(value, 0, options.Length - 1);
        SelectedOption.OnSelect();
      }
    }

    private void OnEnable() {
      if (input == null) {
        input = new InputActions();
        upDownInput.OnEmit += HandleUpDownInput;
        input.Menu.UpDownMovement.performed += upDownInput.OnInputAction;
        input.Menu.UpDownMovement.canceled += upDownInput.OnInputAction;
        input.Menu.UpDownMovement.started += upDownInput.OnInputAction;
        input.Menu.Confirm.performed += HandleConfirmPerformed;
        input.Menu.Back.performed += (_) => { Debug.Log("input.Menu.Back.performed"); };
      }
      input.Menu.Enable();
      SelectedOption.OnSelect();
    }

    private void HandleConfirmPerformed(InputAction.CallbackContext obj) {
      SelectedOption.OnConfirm();
    }

    private void HandleUpDownInput(float val) {
      int direction = (int)Mathf.Sign(val);
      SetSelectedOptionIndex(selectedOptionIndex - direction);
    }

    private void OnDisable() {
      input.Menu.Disable();
      SelectedOption.OnDeselect();
    }
  }
}