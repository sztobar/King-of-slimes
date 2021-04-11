using Kite;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimatorStateInfo<T> where T : struct
{
  //int GetHash(T t);
  void PlayState(T t);
}