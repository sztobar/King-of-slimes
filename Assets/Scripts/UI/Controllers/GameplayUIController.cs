using UnityEngine;
using System.Collections;

public class GameplayUIController : MonoBehaviour {

  public void Open() {
    gameObject.SetActive(true);
  }

  public void Close() {
    gameObject.SetActive(false);
  }
}
