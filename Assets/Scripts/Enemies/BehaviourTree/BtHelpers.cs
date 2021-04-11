using System;
using System.Collections;
using UnityEngine;
using Bt = BtStatus;

public static class BtHelpers {

  public static Bt Sequence(params IBtNode[] nodes) {
    for (int i = 0; i < nodes.Length; i++) {
      Bt status = nodes[i].BtUpdate();
      if (status == Bt.Running) {
        return Bt.Running;
      } else if (status == Bt.Failure) {
        return Bt.Failure;
      }
    }
    return Bt.Success;
  }

  public static Bt Not(IBtNode node) => Not(node.BtUpdate());
  internal static Bt Not(Bt status) {
    switch (status) {
      case Bt.Success:
        return Bt.Failure;
      case Bt.Failure:
        return Bt.Success;
      default:
        return Bt.Running;
    }
  }

  public static Bt Sequence(params Func<Bt>[] nodes) {
    for (int i = 0; i < nodes.Length; i++) {
      Bt status = nodes[i]();
      if (status == Bt.Running) {
        return Bt.Running;
      } else if (status == Bt.Failure) {
        return Bt.Failure;
      }
    }
    return Bt.Success;
  }

  public static Bt Fallback(params IBtNode[] nodes) {
    for (int i = 0; i < nodes.Length; i++) {
      Bt status = nodes[i].BtUpdate();
      if (status == Bt.Running) {
        return Bt.Running;
      } else if (status == Bt.Success) {
        return Bt.Success;
      }
    }
    return Bt.Failure;
  }

  public static Bt Fallback(params Func<Bt>[] nodes) {
    for (int i = 0; i < nodes.Length; i++) {
      Bt status = nodes[i]();
      if (status == Bt.Running) {
        return Bt.Running;
      } else if (status == Bt.Success) {
        return Bt.Success;
      }
    }
    return Bt.Failure;
  }
}
