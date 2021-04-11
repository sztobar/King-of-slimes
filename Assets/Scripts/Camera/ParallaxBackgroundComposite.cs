using Kite;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundComposite : ParallaxBackgroundBase
{
  public List<ParallaxBackground> backgrounds;

  private void OnValidate()
  {
    GetComponentsInChildren(backgrounds);
  }

  private void Awake()
  {
    Deactivate();
  }

  public override void UpdateBackgroundPosition(Vector2 cameraPosition)
  {
    foreach (ParallaxBackground bg in backgrounds)
      bg.UpdateBackgroundPosition(cameraPosition);
  }

  public override void Activate()
  {
    foreach (ParallaxBackground bg in backgrounds)
      bg.Activate();
  }

  public override void Deactivate()
  {
    foreach (ParallaxBackground bg in backgrounds)
      bg.Deactivate();
  }
}