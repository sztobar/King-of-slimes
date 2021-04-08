using UnityEditor.SceneManagement;

namespace KiteEditTests {
  public static class TestHelpers {

    public static void OpenEmptyScene() {
#if UNITY_EDITOR
      EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
#endif
    }
  }
}