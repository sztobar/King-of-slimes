using UnityEngine;
using System.Collections.Generic;
using Kite;

namespace KiteEditTests {
  public static class GameObjectHelpers {

    private static readonly List<GameObject> gameObjects = new List<GameObject>();

    public static GameObject Create(string name = "test GameObject", int layer = CollisionHelpers.layer) {
      if (name == "test GameObject") {
        name = $"{name} #{gameObjects.Count + 1}";
      }
      GameObject gameObject = new GameObject(name) { layer = layer };
      gameObjects.Add(gameObject);
      return gameObject;
    }

    public static GameObject CreateCollider(string name, Vector2 position, Vector2 size) {
      GameObject go = Create(name);
      Transform transform = go.GetComponent<Transform>();
      transform.position = position;
      go.AddComponent<Rigidbody2D>();
      BoxCollider2D boxCollider = go.AddComponent<BoxCollider2D>();
      boxCollider.size = size;
      return go;
    }

    public static GameObject CreatePhysicsMovement(string name, Vector2 position, Vector2 size) {
      GameObject go = CreateCollider(name, position, size);
      var physicsMovement = go.AddComponent<PhysicsMovement>();
      physicsMovement.boxCollider = go.GetComponent<BoxCollider2D>();
      physicsMovement.rigidbody = go.GetComponent<Rigidbody2D>();
      physicsMovement.layerMask = Physics2D.GetLayerCollisionMask(go.layer);
      PrivateHelpers.CallPrivateMethod(physicsMovement, "Awake");
      return go;
    }

    public static void Clear() {
      foreach (GameObject go in gameObjects) {
        Object.DestroyImmediate(go);
      }
      gameObjects.Clear();
    }

  }
}