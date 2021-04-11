using UnityEngine;
using System.Collections;

public interface IMenuOption {

  void OnSelect();

  void OnDeselect();

  void OnConfirm();
}
