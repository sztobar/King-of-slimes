using System;
using System.Collections;
using UnityEngine;

[Serializable]
public readonly struct BtFunc : IBtNode {

  private readonly Func<BtStatus> callback;

  public BtFunc(Func<BtStatus> callback) {
    this.callback = callback;
  }

  public BtStatus BtUpdate() =>
    callback();
}
