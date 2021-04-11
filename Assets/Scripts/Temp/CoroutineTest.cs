using Kite;
using System;
using System.Collections;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
  public bool hasRoutine;
  private IEnumerator routine;

  void Awake()
  {
    routine = GetCoroutine();
    StartCoroutine(routine);
  }

  private IEnumerator GetCoroutine()
  {
    int i = 0;
    while (true)
    {
      yield return new WaitForSeconds(1);
      i++;
      Debug.Log($"{i} seconds have passed.");
    }
  }

  private void Update()
  {
    hasRoutine = routine != null;
  }
}