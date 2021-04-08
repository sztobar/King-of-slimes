using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogFrameController : MonoBehaviour {

  [SerializeField] private float normalTextSpeed = 1f;
  [SerializeField] private float fasterTextSpeed = 2f;

  [SerializeField] private DialogFrameBaseInput input;
  [SerializeField] private DialogFrameText text;


  private void Update() {
    text.TimeMultiplier = input.ConfirmHeld ? fasterTextSpeed : normalTextSpeed;
    if (input.CancelPressed) {
      text.OnCancel();
    }
    if (input.ConfirmPressed) {
      text.OnConfirm();
    }
  }

}
