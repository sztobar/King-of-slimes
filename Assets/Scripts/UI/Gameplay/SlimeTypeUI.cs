using System.Collections;
using UnityEngine;

public class SlimeTypeUI : MonoBehaviour
{
  public SpriteRenderer spriteRenderer;
  public Sprite normalSprite;
  public Sprite selectedSprite;

  private void Awake()
  {
    Deselect();
  }

  public void Select()
  {
    spriteRenderer.sprite = selectedSprite;
  }

  public void Deselect()
  {
    spriteRenderer.sprite = normalSprite;
  }

  public void Show()
  {
    spriteRenderer.enabled = true;
  }

  public void Hide()
  {
    spriteRenderer.enabled = false;
  }
}