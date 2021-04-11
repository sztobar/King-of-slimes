
using Kite;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour {

  [SerializeField]
  private AudioClip creditsMusic;

  public float creditsSpeed = 20f;
  public float confirmActionSpeedModifier = 10f;

  private InputActions input;
  private bool fasterSpeed = false;


  private void OnEnable() {
    if (input == null) {
      input = new InputActions();
      input.Menu.Confirm.performed += HandleConfirmPerformed;
      input.Menu.Confirm.canceled += HandleConfirmCanceled;
      input.Menu.Back.performed += HandleBackPerformed;
    }
    input.Menu.Enable();
  }

  private void OnDisable() {
    input.Menu.Disable();
  }

  private void HandleConfirmPerformed(InputAction.CallbackContext context) {
    fasterSpeed = true;
  }

  private void HandleConfirmCanceled(InputAction.CallbackContext context) {
    fasterSpeed = false;
  }

  private void HandleBackPerformed(InputAction.CallbackContext context) {
    SceneManager.LoadScene(GameplayScenes.StartMenu);
  }

  private void Awake() {
    //AudioSingleton.PlayMusic(creditsMusic);
  }

  void Update() {
    float dt = Time.deltaTime;
    float actualSpeed = creditsSpeed * (fasterSpeed ? confirmActionSpeedModifier : 1f);
    float vy = actualSpeed * dt;
    transform.Translate(new Vector2(0, vy));
  }
}
