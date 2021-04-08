using Kite;
using System;
using System.Collections;
using UnityEngine;

public struct Emittable<T>
{
  private T value;

  public T Value
  {
    get => value;
    set
    {
      this.value = value;
      OnChange(this.value);
    }
  }

  public event Action<T> OnChange;
}