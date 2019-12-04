using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
  public AudioMixer mixer;
  private Slider slider;

  void Awake() {
    float storedSliderValue;

    if (PlayerPrefs.HasKey("music_slider_value"))
      storedSliderValue = PlayerPrefs.GetFloat("music_slider_value");
    else
      storedSliderValue = 1;

    slider = GetComponent<Slider>();
    slider.value = storedSliderValue;
  }

  public void SetLevel(float sliderValue)
  {
    mixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(sliderValue, 0.0001f))*20f);
    PlayerPrefs.SetFloat("music_slider_value", sliderValue);
  }
}
