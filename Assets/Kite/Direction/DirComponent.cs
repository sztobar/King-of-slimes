using System.Collections;
using UnityEngine;

namespace Kite
{
  public abstract class DirComponent : MonoBehaviour
  {
    public abstract Vector2 Forward { get; }
    public abstract Vector2 Backward { get; }
    public abstract Vector2 Above { get; }
    public abstract Vector2 Below { get; }
  }
}