using UnityEngine;
using UnityEngine.Assertions;

namespace Kite {
  /// <summary>
  /// Templated wrapper for instantiating and retrieving a Singleton MonoBehavior.
  /// By default, the singleton instance is created the first time that it is used.
  /// By default, a new empty GameObject is created for the MonoBehavior, but a prefab in a "Resources" folder can
  /// also be used.
  ///
  /// Usage:
  /// 
  /// Declare a static _instance property in your class:
  ///     static MySingletonClass _instance { get { return SingletonBehavior<MySingletonClass>.Get(); } }
  /// 
  /// To use a prefab in a "Resources" folder, pass in the path to the prefab:
  ///     // Requires a prefab at ".../Resources/MyPrefab".
  ///     // If the prefab does not already contain a "MySingletonClass" component, one will be added automatically.
  ///     static MySingletonClass _instance { get { return SingletonBehavior<MySingletonClass>.Get("MyPrefab"); } }
  /// 
  /// To retrieve the singleton instance, just access the "_instance" property:
  ///     _instance.SomeMethod();
  /// 
  /// You can also retrieve the singleton instance manually instead of declaring a property:
  ///     MySingletonClass instance = SingletonBehavior<MySingletonClass>.Get();
  ///     instance.SomeMethod();
  /// 
  /// If you need the singleton instance to be created earlier, you can also create it explicitly:
  ///     SingletonBehavior<MySingletonClass>.CreateInstance();
  /// </summary>
  public static class SingletonBehavior<T> where T : MonoBehaviour {
    /// <summary>
    /// String to prefix to singleton instances.
    /// </summary>
    private static string namePrefix = "[Singleton] ";

    static T instance = null;

    /// <summary>
    /// Returns the singleton instance, creating it if necessary.
    /// </summary>
    /// <param name="prefabPath">
    /// Path to the prefab to instantiate, or "" to create an empty GameObject.  Defaults to "".
    /// </param>
    public static T Get(string prefabPath = "") {
      if (instance == null) {
        CreateInstance(prefabPath);
      }
      return instance;
    }

    /// <summary>
    /// Creates the singleton instance if it does not already exist.
    /// </summary>
    /// <param name="prefabPath">Path to the prefab to instantiate, or "" to create an empty GameObject.</param>
    public static void CreateInstance(string prefabPath = "") {
      // Don't double-create.
      if (instance != null) {
        return;
      }

      GameObject gameObject;
      if (prefabPath == "") {
        gameObject = new GameObject(namePrefix + typeof(T));
      } else {
        gameObject = InstantiatePrefab(prefabPath);
        gameObject.name = namePrefix + gameObject.name;
      }
      GameObject.DontDestroyOnLoad(gameObject);

      // Try to get existing component.
      instance = gameObject.GetComponent<T>();

      // Create one if it doesn't exist.
      if (instance == null) {
        instance = gameObject.AddComponent<T>();
      }
    }

    /// <summary>
    /// Checks whether the singleton instance exists.
    /// </summary>
    public static bool Exists() {
      return instance != null;
    }

    /// <summary>
    /// Instantiates an instance of a prefab.
    /// </summary>
    /// <param name="prefabPath">Path to prefab, under Resources/.</param>
    /// <returns>The instantiated GameObject.</returns>
    public static GameObject InstantiatePrefab(string prefabPath) {
      GameObject prefab = Resources.Load($"Prefabs/{prefabPath}") as GameObject;
      Assert.IsNotNull(prefab, $"Failed to load prefab at path: Prefabs/{prefabPath}");
      return Object.Instantiate(prefab);
    }

    /// <summary>
    /// Instantiates an instance of a prefab, returning an attached script of the specified type.
    /// </summary>
    /// <typeparam name="TPrefab">Type of script to retrieve.</typeparam>
    /// <param name="prefabPath">Path to prefab, under Resources/.</param>
    /// <returns>The attached script.</returns>
    public static TPrefab InstantiatePrefab<TPrefab>(string prefabPath) where TPrefab : Component {
      GameObject instance = InstantiatePrefab(prefabPath);
      return instance.GetComponent<TPrefab>();
    }
  }
}