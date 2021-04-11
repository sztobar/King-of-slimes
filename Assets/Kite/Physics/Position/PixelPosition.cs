using UnityEngine;
using System.Collections;

namespace Kite {
  public class PixelPosition : IPosition {

    private readonly IPosition provider;

    public PixelPosition(IPosition provider) {
      this.provider = provider;
    }

    public Vector2 Value {
      get => PixelHelpers.Floor(provider.Value);
      set => provider.Value = PixelHelpers.Floor(value);
    }

    public Vector2 GetDelta(Vector2 destination) {
      return PixelHelpers.Floor(provider.GetDelta(destination));
    }

    public void Move(Vector2 deltaPosition) {
      provider.Value = PixelHelpers.Floor(provider.Value + deltaPosition);
    }
  }
}