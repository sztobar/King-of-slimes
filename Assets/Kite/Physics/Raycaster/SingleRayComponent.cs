using System.Collections;
using UnityEngine;

namespace Kite
{
  public class SingleRayComponent : MonoBehaviour
  {
    private void OnDrawGizmos()
    {
      GizmosHelpers.FatArrow(transform.position, transform.right * 16);
    }
  }
}