using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource backgroundSource;
    public Slider backgroundSlider;
    public Toggle muteToggle;

    private void Start()
    {
        backgroundSource.volume = backgroundSlider.value = 1;
    }

    public void ChangeVolume(float _value)
    {
        backgroundSource.volume =  _value;
    }

    public void ToggleMute(bool _mute)
    {
        backgroundSource.mute = !_mute;
    }

}
