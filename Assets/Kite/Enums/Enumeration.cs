using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Kite
{
  public abstract class Enumeration<T>
  {
    public string name;
    public T value;

    protected Enumeration(string name, T value) => (this.name, this.value) = (name, value);
  }
}