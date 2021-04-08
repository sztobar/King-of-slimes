using System.Collections;
using UnityEngine;

namespace Kite
{
  public class DirYComponent : DirComponent
  {
    [SerializeField]
    private DirY direction;

    public DirY Value
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

    public override Vector2 Forward => Vector2.right;
    public override Vector2 Backward => Vector2.left;
    public override Vector2 Above => Vector2.up * direction.value;
    public override Vector2 Below => Vector2.down * direction.value;

    private void OnValidate()
    {
      transform.localScale = new Vector3(1f, direction, 1f);
    }

#if UNITY_EDITOR
    private DirY cachedDirection;
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