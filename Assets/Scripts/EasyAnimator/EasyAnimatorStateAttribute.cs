using Kite;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EasyAnimatorStateAttribute : PropertyAttribute
{
  public Type easyAnimatorStateType;

  public EasyAnimatorStateAttribute(Type easyAnimatorStateType)
  {
    this.easyAnimatorStateType = easyAnimatorStateType;
  }
}