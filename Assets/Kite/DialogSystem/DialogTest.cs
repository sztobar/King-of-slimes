using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogTest : MonoBehaviour {

  [SerializeField, TextArea] private string textContent;
  [SerializeField] private DialogFrameText dialogFrameText;
  [SerializeField] private Button button;

  private void Awake() {
    button.onClick.AddListener(ButtonClickedEvent);
  }

  public void ButtonClickedEvent() {
    dialogFrameText.StartText(textContent);
  }

}
