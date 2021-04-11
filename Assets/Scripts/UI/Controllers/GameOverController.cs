using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Controllers {
  public class GameOverController : MonoBehaviour {

    [SerializeField]
    private float delayToRestart;

    private InputActions inputActions;
    private float timeElapsedLeft;

    public void Open() {
      gameObject.SetActive(true);
      timeElapsedLeft = delayToRestart;
    }

    public void Close() {
      gameObject.SetActive(false);
    }

    private void Update() {
      if (timeElapsedLeft > 0) {
        timeElapsedLeft -= Time.unscaledDeltaTime;
      }
    }

    private void OnEnable() {
      if (inputActions == null) {
        inputActions = new InputActions();
        inputActions.Menu.Confirm.performed += HandleConfirmPerformed;
      }
      inputActions.Menu.Enable();
    }

    private void OnDisable() {
      inputActions.Menu.Disable();
    }

    private void HandleConfirmPerformed(InputAction.CallbackContext context) {
      if (timeElapsedLeft <= 0) {
        //GameplayManager.Instance.RestartLevel();
      }
    }
  }
}