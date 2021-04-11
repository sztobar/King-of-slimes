using System.Collections;
using UnityEngine;
using Bt = BtStatus;

public interface IBtState {
  void StateStart();
  Bt StateUpdate();
  void StateExit();
}