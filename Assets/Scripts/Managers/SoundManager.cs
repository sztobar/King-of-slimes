using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

  public static SoundManager _instance;

  public AudioMixer mixer;
  public AudioSource audioSource;

  public event Action<float> OnVolumeChange = delegate { };

  float volume = 1f;
  public float Volume
  {
    get => volume;
    set
    {
      volume = Mathf.Clamp(value, 0.0001f, 1);
      float dbValue = 20f * Mathf.Log10(volume);
      mixer.SetFloat("SFXVolume", dbValue);
      OnVolumeChange(volume);
    }
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
      return;
    }
  }

  private void Start()
  {
    HydrateVolume();
  }

  public void SaveVolume()
  {
    //SaveGameManager.Instance.SaveSfxVolume(Volume);
  }

  public void PlayMenuConfirm()
  {
    //audioSource.clip = menuConfirm;
    //audioSource.Play();
  }

  public void PlaySucess()
  {
    //audioSource.clip = success;
    //audioSource.Play();
  }

  public void PlayPotion()
  {
    //audioSource.clip = gotHP;
    //audioSource.Play();
  }

  private void HydrateVolume()
  {
    //Volume = SaveGameManager.Instance.GetSavedSfxVolume();
  }
}
