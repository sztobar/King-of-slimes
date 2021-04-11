using System.Collections;
using UnityEngine;

public enum BtStatus {
  Running,
  Success,
  Failure
}

public static class BtStatusHelpers {
  public static BtStatus From(bool success) =>
    success ? BtStatus.Success : BtStatus.Failure;
}