using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    [Header("UI")]
    public AudioSource backgroundSource;
    public Slider backgroundSlider;
    public Toggle muteToggle;

    [Header("Enemy Sounds")]
    public AudioClip[] hitSounds;
    public AudioClip dieSound;
    public AudioClip[] attackSounds;
    public AudioClip[] footStepSounds;

    [Header("Player Sounds")]
    public AudioClip playerFootStep;

    #region Menu Sounds
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

    #endregion

    #region Enemy Sounds

    public AudioClip GetDieSound()
    {
        return dieSound;
    }

    public AudioClip GetHitSound()
    {
        int rnd = Random.Range(0, hitSounds.Length);
        return hitSounds[rnd];
    }

    public AudioClip GetAttackSound()
    {
        return attackSounds[Random.Range(0, attackSounds.Length)];
    }

    public AudioClip GetFootStepSound()
    {
        return footStepSounds[Random.Range(0, footStepSounds.Length)];
    }

    #endregion

}
