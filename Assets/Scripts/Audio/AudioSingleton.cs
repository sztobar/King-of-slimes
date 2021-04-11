using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using UnityEngine.Audio;
using Kite;

  /// <summary>
  /// Singleton class for handling basic audio-related functionality.
  /// 
  /// Audio clips under Resources/Sfx are all cached when this initializes, so they don't have to be loaded
  /// individually.
  /// 
  /// Usage:
  ///     // Play a sound under a "Resources" folder.
  ///     AudioManager.PlaySound("MySound");
  ///
  ///     // Play music under a "Resources" folder.
  ///     AudioManager.PlayMusic("MyMusic");
  /// 
  ///     // Stop music.
  ///     AudioManager.StopMusic();
  /// 
  ///     // Fade out music.
  ///     AudioManager.FadeMusic();
  /// </summary>
  public class AudioSingleton : MonoBehaviour {
    /// <summary>
    /// Returns the static AudioSource used for playing music.
    /// </summary>
    public static AudioSource MusicSource => Instance.musicSource;

    public static AudioSingleton Instance => SingletonBehavior<AudioSingleton>.Get("AudioSingleton");

    /// <summary>
    /// AudioSource used to play music.
    /// </summary>
    [SerializeField]
    AudioSource musicSource;

    [SerializeField]
    AudioMixerGroup sfxMixerGroup;

    public ScriptableSfx clips;

    static AudioMixerGroup staticSfxMixerGroup;

    /// <summary>
    /// Silent audio clip.  Played on init to prevent Unity's hiccup on initial audio play.
    /// </summary>
    [SerializeField]
    AudioClip silenceClip;

    HashSet<int> played = new HashSet<int>();

    Dictionary<string, AudioClip> _cachedClips;

    public static void Init() {
      SingletonBehavior<AudioSingleton>.Get("AudioSingleton");
    }

    /// <summary>
    /// Plays a one-shot sound.
    /// Only allows one instance of the same sound to be played per frame.
    /// TODO: Allow customizeable volume stacking behavior when this happens.
    /// </summary>
    /// <returns>AudioSource used to play the sound, or null if no sound was played.</returns>
    /// <param name="audioClipPath">Path to audio clip, under Resources/.</param>
    public static AudioSource PlaySound(string audioClipPath, float volume = 1.0f) {
      AudioClip clip = LoadClipFromPath(audioClipPath);
      return PlaySound(clip, volume);
    }

    /// <summary>
    /// Plays a one-shot sound.
    /// Only allows one instance of the same sound to be played per frame.
    /// TODO: Allow customizeable volume stacking behavior when this happens.
    /// </summary>
    /// <returns>AudioSource used to play the sound, or null if no sound was played.</returns>
    /// <param name="audioClipPath">Audio clip to play.</param>
    public static AudioSource PlaySound(AudioClip clip, float volume = 1.0f) {
      Assert.IsNotNull(clip);
      if (clip == null) {
        return null;
      }

      if (Instance.played.Contains(clip.GetHashCode())) {
        return null;
      }
      Instance.played.Add(clip.GetHashCode());

      Instance.PurgeSources();
      AudioSource source = Instance.gameObject.AddComponent<AudioSource>();
      source.outputAudioMixerGroup = staticSfxMixerGroup;
      source.volume = volume;
      source.PlayOneShot(clip);
      return source;
    }

    public static AudioSource PlayUniqueSound(AudioClip clip, float volume = 1.0f) {
      Assert.IsNotNull(clip);
      if (clip == null) {
        return null;
      }

      Instance.PurgeSources();
      AudioSource source = Instance.gameObject.AddComponent<AudioSource>();
      source.outputAudioMixerGroup = staticSfxMixerGroup;
      source.volume = volume;
      source.PlayOneShot(clip);
      return source;
    }

    public static AudioSource PlayScheduled(string audioClipPath, double dspTime, float volume = 1.0f) {
      AudioClip clip = LoadClipFromPath(audioClipPath);
      return PlayScheduled(clip, dspTime, volume);
    }

    public static AudioSource PlayScheduled(AudioClip clip, double dspTime, float volume = 1.0f) {
      Instance.PurgeSources();
      AudioSource source = Instance.gameObject.AddComponent<AudioSource>();
      source.outputAudioMixerGroup = staticSfxMixerGroup;
      source.clip = clip;
      source.volume = volume;
      source.PlayScheduled(dspTime);
      return source;
    }

    /// <summary>
    /// Plays and loops a music clip, immediately stopping any previous clip that was playing.
    /// Does nothing if the clip was already playing.
    /// </summary>
    /// <param name="audioClipPath">Path to audio clip, under Resources/.</param>
    /// <param name="loop">Whether to loop music.  Defaults to true.</param>
    public static void PlayMusic(string audioClipPath, bool loop = true) {
      AudioClip clip = LoadClipFromPath(audioClipPath);
      PlayMusic(clip, loop);
    }

    /// <summary>
    /// Plays and loops a music clip, immediately stopping any previous clip that was playing.
    /// Does nothing if the clip was already playing.
    /// </summary>
    /// <param name="audioClip">AudioClip to play.</param>
    /// <param name="loop">Whether to loop music.  Defaults to true.</param>
    public static void PlayMusic(AudioClip audioClip, bool loop = true) {
      if (Instance.musicSource.clip == audioClip && Instance.musicSource.isPlaying) {
        return;
      }

      Instance.musicSource.clip = audioClip;
      Instance.musicSource.Play();
      Instance.musicSource.volume = 1.0f;
      Instance.musicSource.loop = loop;
    }

    public static void PlayMusicScheduled(AudioClip audioClip, double dspTime, bool loop = true) {
      Instance.musicSource.clip = audioClip;
      Instance.musicSource.PlayScheduled(dspTime);
      Instance.musicSource.volume = 1.0f;
      Instance.musicSource.loop = loop;
    }

    /// <summary>
    /// Pauses the current music clip, if there is one.
    /// </summary>
    public static void PauseMusic() {
      // No-op if music is not playing.
      if (!IsMusicPlaying()) {
        Debug.LogWarningFormat("Ignoring PauseMusic: Music is not playing!");
        return;
      }

      Instance.musicSource.Pause();
    }

    /// <summary>
    /// Unpauses the current music clip, if there is one.
    /// </summary>
    public static void UnpauseMusic() {
      // TODO: Check that there is a music clip?

      Instance.musicSource.UnPause();
    }

    /// <summary>
    /// Whether a music clip is currently playing.
    /// </summary>
    /// <returns>True if music is playing, false otherwise.</returns>
    public static bool IsMusicPlaying() {
      return Instance.musicSource.isPlaying;
    }

    /// <summary>
    /// Fades the current music clip, if there is one.
    /// </summary>
    /// <param name="duration">
    /// Speed of fade, in amount per second.
    /// Defaults to AudioSourceFader.kDefaultSpeed.
    /// </param>
    public static void FadeMusic(float speed = AudioSourceFader.kDefaultSpeed) {
      // No-op if music is not playing.
      if (!IsMusicPlaying()) {
        Debug.LogWarningFormat("Ignoring FadeMusic with speed {0}: Music is not playing!", speed);
        return;
      }

      AudioSourceFader.Fade(MusicSource, 0.0f, speed, StopMusic);
    }

    /// <summary>
    /// Immediately stops the current music clip, if there is one.
    /// </summary>
    public static void StopMusic() {
      Instance.musicSource.Stop();
      Instance.musicSource.clip = null;
    }

    protected virtual void Awake() {
      // Preload everything in the PreloadSfx/ directory.
      AudioClip[] clips = Resources.LoadAll<AudioClip>("PreloadSfx");
      _cachedClips = new Dictionary<string, AudioClip>(clips.Length);
      foreach (AudioClip clip in clips) {
        _cachedClips.Add(string.Format("PreloadSfx/{0}", clip.name), clip);
        clip.LoadAudioData();
      }
      staticSfxMixerGroup = sfxMixerGroup;
    }

    protected virtual void Start() {
      //PlaySound(silenceClip, 0.0f);
    }

    protected virtual void Update() {
      played.Clear();
    }

    static AudioClip LoadClipFromPath(string path) {
      AudioClip result;
      if (!Instance._cachedClips.TryGetValue(path, out result)) {
        Debug.LogFormat("Loading non-cached audioclip {0} by name", path);
        result = Resources.Load<AudioClip>(path);
        Assert.IsNotNull(result, string.Format("Failed to load audio clip at {0}", path));
      }
      return result;
    }

    void PurgeSources() {
      AudioSource[] sources = GetComponents<AudioSource>();
      foreach (AudioSource source in sources) {
        if (source == musicSource) {
          continue;
        }

        if (!source.isPlaying) {
          Destroy(source);
        }
      }
    }
  }
