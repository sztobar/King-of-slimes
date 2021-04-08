using UnityEngine;
using System.Collections;
using System;

public class DialogFrameCursorNext : MonoBehaviour
{
  public virtual void Hide()
  {
    gameObject.SetActive(false);
  }

  public virtual void Show()
  {
    gameObject.SetActive(true);
  }
}
