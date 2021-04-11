using Kite;
using System;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
  public int tilesUpWhenOpened;
  public WorldTileFloat openedDeltaPosition;
  [Range(0, 1)] public float openedPercent;

  public AnimationCurve openCurve;
  public float openTime;
  public AnimationCurve closeCurve;
  public float closeTime;
  public Transform moveTransform;
  public DoorCrusher crusher;

  private float elapsedTime;
  private bool isOpened;
  private Vector2 openedPosition;

  private void OnValidate()
  {
    if (openedDeltaPosition == 0) // TODO: temporary variable translation
    {
      openedDeltaPosition = tilesUpWhenOpened;
      openedDeltaPosition.type = WorldTileFloat.Type.Tile;
    }
    openedPosition = Vector2.up * openedDeltaPosition;
    SetOpenedPosition(openedPercent);
  }

  public void Awake()
  {
    enabled = false;
  }

  public void ForceOpen()
  {
    isOpened = true;
    SetOpenedPosition(1);
  }

  public void SetIsOpen(bool isOpen)
  {
    isOpened = true;
    openedPercent = isOpen ? 1 : 0;
    SetOpenedPosition(openedPercent);
    enabled = false;
  }

  public void Reset()
  {
    elapsedTime = 0;
    openedPercent = 0;
    enabled = false;
    SetOpenedPosition(0);
  }

  public void StartIsOpen(bool isOpen)
  {
    isOpened = isOpen;
    if (!enabled)
    {
      enabled = true;
    }
    elapsedTime = 0;
  }

  public void SetOpenedPosition(float lerp)
  {
    openedPercent = lerp;
    moveTransform.localPosition = Vector2.Lerp(Vector2.zero, openedPosition, lerp);
  }

  public void SetNormalizedTime(bool isOpened, float normalizedTime)
  {
    this.isOpened = isOpened;
    if (isOpened)
    {
      elapsedTime = normalizedTime * openTime;
      openedPercent = openCurve.Evaluate(normalizedTime);
    }
    else
    {
      elapsedTime = normalizedTime * closeTime;
      openedPercent = closeCurve.Evaluate(normalizedTime);
    }
    SetOpenedPosition(openedPercent);
  }

  public void SetElapsedTime(bool isOpened, float elapsedTime)
  {
    this.isOpened = isOpened;
    this.elapsedTime = elapsedTime;
    if (isOpened)
    {
      float timePercentage = elapsedTime / openTime;
      SetOpenedPosition(timePercentage);
    }
    else
    {
      float timePercentage = elapsedTime / closeTime;
      SetOpenedPosition(timePercentage);
    }
  }

  private void Update()
  {
    float timePercentage;
    if (isOpened)
    {
      timePercentage = elapsedTime / openTime;
      float newOpenedPercent = openCurve.Evaluate(timePercentage);
      if (newOpenedPercent > openedPercent)
      {
        openedPercent = newOpenedPercent;
        SetOpenedPosition(newOpenedPercent);
      }
    }
    else
    {
      timePercentage = elapsedTime / closeTime;
      float newOpenedPercent = closeCurve.Evaluate(timePercentage);
      if (newOpenedPercent < openedPercent)
      {
        openedPercent = newOpenedPercent;
        SetOpenedPosition(newOpenedPercent);
      }
    }

    if (timePercentage == 1)
    {
      enabled = false;
    }
    else
    {
      elapsedTime += Time.deltaTime;
    }
  }
}