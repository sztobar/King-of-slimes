using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kite {
  public class PauseManager : BaseManager<PauseManager> {

    private bool isPaused;
    private readonly List<PausableComponent> pausables = new List<PausableComponent>();

    public static bool IsPaused => instance.isPaused;

    public static void TogglePause() {
      instance.isPaused = !instance.isPaused;
      if (instance.isPaused) {
        BroadcastPauseOff();
      } else {
        BroadcastPauseOn();
      }
    }

    public static void PauseOn() {
      instance.isPaused = true;
      Time.timeScale = 0;
      BroadcastPauseOn();
    }

    public static void PauseOff() {
      instance.isPaused = false;
      Time.timeScale = 1;
      BroadcastPauseOff();
    }

    public static void RemovePausable(PausableComponent pausable) {
      instance.pausables.Remove(pausable);
    }

    public static void AddPausable(PausableComponent pausable) {
      instance.pausables.Add(pausable);
    }

    public static void BroadcastPauseOn() {
      for (int i = 0; i < instance.pausables.Count; i++) {
        PausableComponent pausable = instance.pausables[i];
        pausable.HandlePauseOn();
      }
    }

    public static void BroadcastPauseOff() {
      for (int i = 0; i < instance.pausables.Count; i++) {
        PausableComponent pausable = instance.pausables[i];
        pausable.HandlePauseOff();
      }
    }

    protected override void ManagerInit() {
      base.ManagerInit();
      SceneManager.activeSceneChanged += HandleActiveSceneChange;
    }

    private void HandleActiveSceneChange(Scene currentScene, Scene nextScene) {
      if (currentScene.name != null) {
        pausables.Clear();
      }
    }
  }
}