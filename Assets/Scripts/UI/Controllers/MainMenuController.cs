using UnityEngine;
using UI.Screens;
using UnityEngine.SceneManagement;
using Kite;

namespace UI.Controllers {
  public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private AudioClip menuMusic;

    [SerializeField]
    private MenuScreen mainMenu;

    [SerializeField]
    private MenuScreen optionsMenu;

    private void Awake() {
      AudioSingleton.PlayMusic(menuMusic);
    }

    public void StartGame() {
      SceneManager.LoadScene(GameplayScenes.Tutorial);
    }

    public void OpenOptionsScreen() {
      mainMenu.Close();
      optionsMenu.SetSelectedOptionIndex(0);
      optionsMenu.Open();
    }

    public void OpenMainScreen() {
      optionsMenu.Close();
      mainMenu.Open();
    }

    public void SelectSlider(TriangleSlider slider) { }


    public void StartCredits() {
      SceneManager.LoadScene(GameplayScenes.Credits);
    }
  }
}