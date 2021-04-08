using UnityEngine;
using System;

namespace Kite {
  /// <summary>
  /// A lightweight component that automatically applies a linear fade to a given AudioSource, optionally executing a
  /// callback when the fade finishes.
  /// The fade will ABORT if the volume of the AudioSource is changed by something else while the fade is happening!
  /// 
  /// Usage:
  ///     // Fade out a sound to 0 volume with a speed of 1.
  ///     FadeAudioSource(audioSource, 0.0f, 1.0f);
  ///
  ///     // Fade out a sound to AudioManager.kGlobalVolumeMultiplier volume with a speed of 1.
  ///     FadeAudioSource(audioSource, 1.0f, 1.0f);
  ///
  ///     // Instantly mute a sound.
  ///     FadeAudioSource(audioSource, 0.0f, 0.0f);
  /// </summary>
  public class AudioSourceFader : MonoBehaviour {
    public const float kDefaultSpeed = 1.0f;

    float _previousFrameVolume;

    float _startVolume;

    float _endVolume;

    float _startTime;

    float _duration;

    AudioSource _audioSource;

    Action _callback;

    /// <summary>
    /// Fades the given audio source using a new AudioSourceFader.
    /// </summary>
    /// <param name="target">Target AudioSource.</param>
    /// <param name="volume">Volume to fade to.</param>
    /// <param name="speed">Speed of fade, in amount per second, or 0.0f to set volume instantly.</param>
    /// <param name="callback">Callback to execute when fade is finished.  Defaults to null.</param>
    public static void Fade(
        AudioSource target,
        float volume,
        float speed = kDefaultSpeed,
        Action callback = null) {
      if (speed <= 0.0f) {
        target.volume = volume;
        return;
      }

      AudioSourceFader fader = target.gameObject.AddComponent<AudioSourceFader>();
      fader.ConstructAudioSourceFader(target, volume, speed, callback);
    }

    protected void ConstructAudioSourceFader(AudioSource target, float volume, float speed, Action callback) {
      _audioSource = target;
      _startVolume = target.volume;
      _endVolume = volume;
      float delta = _endVolume - _startVolume;
      _startTime = Time.unscaledTime;
      _duration = Mathf.Abs(delta) / speed;
      _callback = callback;
      _previousFrameVolume = target.volume;
    }

    protected virtual void Update() {
      // Abort if Audio Source volume has been changed by something else.
      if (_audioSource.volume != _previousFrameVolume) {
        Debug.LogErrorFormat("Aborting fade on {0} - unexpected volume change", _audioSource);
        Destroy(this);
        return;
      }

      float progress = (Time.unscaledTime - _startTime) / _duration;
      _audioSource.volume = Mathf.Lerp(_startVolume, _endVolume, progress);

      if (progress >= 1.0f) {
        Destroy(this);
        if (_callback != null) {
          _callback();
        }
        return;
      }

      _previousFrameVolume = _audioSource.volume;
    }
  }
}