using System;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{

  public static MusicManager _instance;

  public AudioMixer mixer;

  public event Action<float> OnVolumeChange = delegate { };

  float volume = 1f;
  public float Volume
  {
    get => volume;
    set
    {
      volume = Mathf.Clamp(value, 0.0001f, 1);
      float dbValue = 20f * Mathf.Log10(volume);
      mixer.SetFloat("MusicVolume", dbValue);
      OnVolumeChange(volume);
    }
  }

  public void SaveVolume()
  {
    //SaveGameManager.Instance.SaveMusicVolume(Volume);
  }

  void Awake()
  {
    if (_instance == null)
    {
      _instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private void Start()
  {
    HydrateVolume();
  }

  private void HydrateVolume()
  {
    //Volume = SaveGameManager.Instance.GetSavedMusicVolume();
  }

}
