using UnityEngine;
using System.Collections;

namespace Kite {
  public interface ICollidableComponent : ICollidable {
    Vector2 AllowedMove { get; set; }
    bool IsPlatform { get; set; }
  }
}