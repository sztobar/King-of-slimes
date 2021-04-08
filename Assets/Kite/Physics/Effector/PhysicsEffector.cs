using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kite
{
  public class PhysicsEffector<T> : MonoBehaviour where T : PhysicsEffectable
  {
    private readonly HashSet<T> effectables = new HashSet<T>();

    public List<T> GetEffectables() => new List<T>(effectables);

    public void ClearEffectables()
    {
      effectables.Clear();
    }

    public virtual void AddEffectable(T effectable)
    {
      effectables.Add(effectable);
    }

    [Obsolete]
    public virtual void RegisterEffectable(EffectableBehaviour effectable)
    {
      //effectables.Add(effectable);
    }

    [Obsolete]
    internal void UnregisterEffectable(EffectableBehaviour effectable)
    {
      //effectables.Remove(effectable);
    }


    [Obsolete]
    public virtual bool Match(CollisionMoveHit hit) => true;
  }
}