using System.Collections;
using UnityEngine;

public abstract class BtNode : MonoBehaviour, IBtNode {
  public abstract BtStatus BtUpdate();

}
