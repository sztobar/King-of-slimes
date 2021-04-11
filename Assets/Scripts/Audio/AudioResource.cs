using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;

public class AudioResource : MonoBehaviour
{
  public static AudioResource instance;

  public AudioMixerGroup sfxMixerGroup;

  private readonly Dictionary<int, AudioSource> sources;

  public AudioSource PlaySound(AudioClip clip, float volume = 1.0f)
  {
    Assert.IsNotNull(clip);
    int clipHash = clip.GetHashCode();
    if (sources.ContainsKey(clipHash))
      return null;

    PurgeSources();
    AudioSource source = gameObject.AddComponent<AudioSource>();
    sources.Add(clipHash, source);
    source.outputAudioMixerGroup = sfxMixerGroup;
    source.volume = volume;
    source.PlayOneShot(clip);
    return source;
  }

  private void PurgeSources()
  {
    foreach (KeyValuePair<int, AudioSource> sourceKV in sources)
    {
      sources.TryGetValue(sourceKV.Key, out AudioSource source);
      if (!source.isPlaying)
      {
        Destroy(source);
        sources.Remove(sourceKV.Key);
      }
    }
  }

  public static void Instantiate()
  {
    if (instance)
      return;
    GameObject gameObject = Resources.Load($"Prefabs/AudioResource") as GameObject;
    Assert.IsNotNull(gameObject, $"Failed to load prefab at path: Prefabs/AudioResource");
    Instantiate(gameObject);
    DontDestroyOnLoad(gameObject);
    instance = gameObject.GetComponent<AudioResource>();
  }


}