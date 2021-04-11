using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneCollider : MonoBehaviour
{

  public string sceneName;
  private void OnTriggerEnter2D(Collider2D collision)
  {
    PlayerUnitController unit = InteractiveHelpers.GetPlayer(collision);
    if (unit)
    {
      SceneManager.LoadScene(sceneName);
    }
  }
}