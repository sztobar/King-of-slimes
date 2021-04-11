using System;
using System.Collections.Generic;
using UnityEngine;

public class SlimePoof : MonoBehaviour
{
  public EasyAnimator easyAnimator;
  public SpriteRenderer spriteRenderer;

  void Awake()
  {
    easyAnimator.Stop();
    spriteRenderer.enabled = false;
    easyAnimator.OnAnimationEnd += HandleAnimationEnd;
  }

  private void HandleAnimationEnd(int animatorStateHash)
  {
    spriteRenderer.enabled = false;
    easyAnimator.Stop();
  }

  internal void SpawnAt(Vector2 spawnPosition)
  {
    transform.position = spawnPosition;
    spriteRenderer.enabled = true;
    easyAnimator.Play();
  }
}
