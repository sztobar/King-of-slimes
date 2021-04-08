using System;
using System.Collections;
using UnityEngine;

namespace Kite {
  public class LateUpdateManager : MonoBehaviour {

    public static LateUpdateManager instance;
    public static LateUpdateManager Instance {
      get {
        if (instance) {
          return instance;
        } else {
          GameObject newObject = new GameObject("LateUpdateManager");
          instance = newObject.AddComponent<LateUpdateManager>();
          return instance;
        }
      }
    }

    public event Action OnFixedUpdate = delegate { };

    private void FixedUpdate() {
      OnFixedUpdate();
    }

    private void Awake() {
      if (instance) {
        Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
      } else {
        instance = this;
      }
    }
  }
}