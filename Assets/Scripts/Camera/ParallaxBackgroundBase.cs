using Kite;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParallaxBackgroundBase : MonoBehaviour
{
  public abstract void UpdateBackgroundPosition(Vector2 cameraPosition);
  public abstract void Activate();
  public abstract void Deactivate();
}