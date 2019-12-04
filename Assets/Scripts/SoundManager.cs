using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;

    void Start() {
      float storedSliderValue;

      if (PlayerPrefs.HasKey("music_slider_value"))
        storedSliderValue = PlayerPrefs.GetFloat("music_slider_value");
      else
        storedSliderValue = 1;

      mixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(storedSliderValue, 0.0001f))*20f);
    }
}
