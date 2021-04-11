using Kite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeathPoof : MonoBehaviour
{
  public EasyAnimator animator;

  private void Awake()
  {
    animator.Stop();
    animator.OnAnimationEnd += OnAnimationEnd;
  }

  private void OnAnimationEnd(int animatorStateHash)
  {
    Destroy(gameObject);
  }

  public void Spawn()
  {
    gameObject.SetActive(true);
    animator.Play();
  }

  public void DestroyWithPoof(GameObject gameObjectToDestroy)
  {
    Spawn();
    transform.SetParent(gameObjectToDestroy.transform.parent);
    Destroy(gameObjectToDestroy);
  }
}