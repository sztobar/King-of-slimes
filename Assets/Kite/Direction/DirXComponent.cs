using System.Collections;
using UnityEngine;

namespace Kite
{
  public class DirXComponent : DirComponent
  {
    [SerializeField]
    private DirX direction;

    public DirX Value
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

    public override Vector2 Forward => Vector2.right * direction.value;
    public override Vector2 Backward => Vector2.left * direction.value;
    public override Vector2 Above => Vector2.up;
    public override Vector2 Below => Vector2.down;

    private void OnValidate()
    {
      transform.localScale = new Vector3(direction, 1f, 1f);
    }

#if UNITY_EDITOR
    private DirX cachedDirection;
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