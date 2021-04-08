using Kite;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace KiteEditor
{
  public class EventListenerComponentEditor<TEmitter, TListener, TValue, TEmitterList> : Editor
    where TListener : EventListenerComponent<TValue>
    where TEmitter : EventEmitterComponent<TListener, TValue>
    where TEmitterList : ScriptablEventEmittersList<TEmitter, TListener, TValue>
  {
    private static SceneGUICollider sceneGuiCollider;
    //private static GameObject guiClickedGameObject;
    //private static float guiClickedTime;
    private static TEmitterList emittersList;

    private TListener listener;
    private VisualElement root;
    private AddRemoveList addRemoveList;

    private SerializedObject serializedEmittersList;

    private void OnEnable()
    {
      sceneGuiCollider = new SceneGUICollider();
      listener = (TListener)target;
      root = UIToolkitHelpers.CreateDefault(serializedObject);
      emittersList = ScriptableObject.CreateInstance<TEmitterList>();
      emittersList.emitters = GetEmittersComponents();
      serializedEmittersList = new SerializedObject(emittersList);
      //SerializedProperty serlializedEmitters = serializedEmittersList.FindProperty(nameof(emittersList.emitters));

      //ObjectField addItemField = new ObjectField("Add emitter") { objectType = typeof(TEmitter) };
      //addRemoveList = new AddRemoveList("Emitters: ", serlializedEmitters, addItemField);
      //addRemoveList.RemoveItemAction += AddRemoveList_RemoveItemAction;
      //addRemoveList.AddItemAction += AddRemoveList_AddItemAction;
      //root.Add(addRemoveList);
    }

    public override void OnInspectorGUI()
    {
      EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));

      var emittersProperty = serializedEmittersList.FindProperty("emitters");
      var emittersCopy = new List<TEmitter>(emittersList.emitters);
      EditorGUI.BeginChangeCheck();
      EditorGUI.BeginDisabledGroup(true);
      EditorGUILayout.PropertyField(emittersProperty, new GUIContent("Emitters (readonly)"));
      EditorGUI.EndDisabledGroup();
      if (EditorGUI.EndChangeCheck())
      {
        int lenA = emittersCopy.Count;
        int lenB = emittersList.emitters.Count;
        if (lenA > lenB)
        {
          Debug.Log($"Emitter removed");
        }
        else if (lenA < lenB)
        {
          Debug.Log($"Emitter added");
        }
      }
    }

    private void AddRemoveList_AddItemAction(Object obj)
    {
      if (obj is TEmitter newEmitter)
      {
        if (!newEmitter.listeners.Contains(listener))
        {
          Undo.RecordObject(obj, "Add emitter");
          newEmitter.listeners.Add(listener);
          emittersList.emitters.Add(newEmitter);
          SceneView.RepaintAll();
        }
      }
    }

    private void AddRemoveList_RemoveItemAction(int index)
    {
      TEmitter emitter = emittersList.emitters[index];
      Undo.RecordObject(emitter, "Remove emitter");
      emitter.listeners.Remove(listener);
      emittersList.emitters.RemoveAt(index);
      SceneView.RepaintAll();
    }

    private void OnDisable()
    {
      Undo.undoRedoPerformed -= UndoRedoPerformed;
      emittersList = null;
      sceneGuiCollider = null;
      //guiClickedGameObject = null;
      //guiClickedTime = 0;
    }

    private void UndoRedoPerformed()
    {
      emittersList.emitters = GetEmittersComponents();
      addRemoveList.UpdateListViewSize();
    }

    //public override VisualElement CreateInspectorGUI()
    //{
    //  return root;
    //}

    public virtual void OnSceneGUI()
    {
      List<TEmitter> guiEmitters = emittersList.emitters;
      for (int i = 0; i < guiEmitters.Count; i++)
      {
        TEmitter emitter = guiEmitters[i];
        if (emitter)
        {
          HandlesHelpers.Arrow(emitter.transform.position, listener.transform.position - emitter.transform.position, 4);
          //float emitterHandleSize = 4;
          //Vector3 emitterHandleOffset = Vector3.zero;
          Collider2D collider = emitter.GetComponent<Collider2D>();
          if (collider)
          {
            HandlesHelpers.Collider(collider);
            //emitterHandleSize = Mathf.Max(collider.bounds.extents.x, collider.bounds.extents.y, emitterHandleSize);
            //emitterHandleOffset = collider.offset;
          }
          //if (Handles.Button(emitter.transform.position + emitterHandleOffset, Quaternion.identity, emitterHandleSize, emitterHandleSize, Handles.RectangleHandleCap))
          //{
          //  GameObject clickedGameObject = emitter.gameObject;
          //  if (clickedGameObject == guiClickedGameObject)
          //  {
          //    float currentTime = Time.realtimeSinceStartup;
          //    if (currentTime - guiClickedTime < 0.5f)
          //    {
          //      AssetDatabase.OpenAsset(clickedGameObject);
          //    }
          //  }
          //  else
          //  {
          //    guiClickedGameObject = clickedGameObject;
          //    EditorGUIUtility.PingObject(guiClickedGameObject);
          //  }
          //  guiClickedTime = Time.realtimeSinceStartup;
          //}
        }
      }
    }

    private List<TEmitter> GetEmittersComponents()
    {
      List<TEmitter> emitters = new List<TEmitter>();
      TEmitter[] sceneEmitters = FindObjectsOfType<TEmitter>();
      for (int i = 0; i < sceneEmitters.Length; i++)
      {
        TEmitter emitter = sceneEmitters[i];
        for (int j = 0; j < emitter.listeners.Count; j++)
        {
          TListener emitterListener = emitter.listeners[j];
          if (emitterListener == listener)
          {
            emitters.Add(emitter);
            break;
          }
        }
      }
      return emitters;
    }
  }

  public class ScriptablEventEmittersList<TEmitter, TListener, TValue> : ScriptableObject
    where TListener : EventListenerComponent<TValue>
    where TEmitter : EventEmitterComponent<TListener, TValue>
  {
    public List<TEmitter> emitters;
  }
}