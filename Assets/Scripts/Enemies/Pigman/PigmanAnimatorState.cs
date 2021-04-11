using System.Collections;
using UnityEngine;

public enum PigmanAnimatorState {
  Idle = 1,
  Walk = 2,
  Attack = 3,
  Roar = 4,
  Sit = 5,
  StandUp = 6,
  Run = 7,
  Parry = 8,
  Stagger = 9,

  // not implemented:
  Hit = 10,
  Dead = 11,
}