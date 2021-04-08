using Kite;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace KiteEditor
{
  //public class EventEmitterComponentEditor<TEmitter, TListener, TValue> : Editor
  //  where TEmitter : EventEmitterComponent<TListener, TValue>
  //  where TListener : EventListenerComponent<TValue>
  //{
  //  private static SceneGUICollider sceneGuiCollider;

  //  private VisualElement root;

  //  private void OnEnable()
  //  {
  //    sceneGuiCollider = new SceneGUICollider();
  //    root = UIToolkitHelpers.CreateDefault(serializedObject);
  //  }

  //  public override VisualElement CreateInspectorGUI()
  //  {
  //    return root;
  //  }

  //  public override void OnInspectorGUI()
  //  {
  //    base.OnInspectorGUI();
  //  }

  //  private void OnDisable()
  //  {
  //    sceneGuiCollider = null;
  //  }

  //  public void OnSceneGUI()
  //  {
  //    TEmitter emitter = (TEmitter)target;
  //    if (emitter.listeners == null)
  //      return;

  //    for (int i = 0; i < emitter.listeners.Count; i++)
  //    {
  //      TListener listener = emitter.listeners[i];
  //      if (listener)
  //      {
  //        Collider2D collider = listener.GetComponent<Collider2D>();
  //        if (collider)
  //        {
  //          if (HandlesHelpers.ColliderButton(collider))
  //            sceneGuiCollider.OnClick(collider);
  //        }
  //        HandlesHelpers.Arrow(emitter.transform.position, listener.transform.position - emitter.transform.position, 4);
  //      }
  //    }
  //  }
  //}
}