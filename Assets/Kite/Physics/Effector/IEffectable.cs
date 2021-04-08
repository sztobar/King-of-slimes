using System.Collections;
using UnityEngine;

namespace Kite {
  public interface IEffectable {
    void Push(float distance, Direction4 direction);
  }
}