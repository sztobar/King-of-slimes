using System.Collections;
using UnityEngine;

namespace Kite
{
  public class Dir4Component : DirComponent
  {
    [SerializeField]
    private Dir4 direction;

    public Dir4 Value
    {
      get => direction;
      set
      {
        if (direction != value)
        {
          direction = value;
          OnValidate();
        }
      }
    }

    public override Vector2 Forward => Vector2.right * direction.x;
    public override Vector2 Backward => Vector2.left * direction.x;
    public override Vector2 Above => Vector2.up * direction.y;
    public override Vector2 Below => Vector2.down * direction.y;

    private void OnValidate()
    {
      transform.localScale = new Vector3(direction.x, direction.y, 1f);
    }

#if UNITY_EDITOR
    private Dir4 cachedDirection;
    private void Update()
    {
      if (cachedDirection != direction)
      {
        OnValidate();
        cachedDirection = direction;
      }
    }
#endif
  }
}