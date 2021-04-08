using UnityEngine;

namespace Kite {

  public abstract class BaseManager<T> : MonoBehaviour where T : MonoBehaviour {

    protected static T instance;

    protected virtual void Awake() {
      if (instance == null) {
        instance = GetComponent<T>();
        ManagerInit();
        DontDestroyOnLoad(gameObject);
      } else {
        Destroy(gameObject);
      }
    }

    protected virtual void ManagerInit() { }
  }
}
