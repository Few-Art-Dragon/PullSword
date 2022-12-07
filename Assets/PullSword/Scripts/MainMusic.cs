using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMusic : MonoBehaviour
{
    [SerializeField]
    private MainToggle _mainMusicTonggle;
    [SerializeField]
    private MainSlider _mainMusicSlider;
    private AudioSource _mainThemeAudioSource;

    private void SetEnableMainMusic()
    {
        _mainThemeAudioSource.enabled = _mainThemeAudioSource.enabled ? false : true;
    }

    private void SetVolumeMainMusic(float value)
    {
        _mainThemeAudioSource.volume = value;
    }

    private void OnEnable()
    {
        _mainMusicTonggle.OnClickEvent += SetEnableMainMusic;
        _mainMusicSlider.OnMoveEvent += SetVolumeMainMusic;
    }

    private void Start()
    {
        _mainThemeAudioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        _mainMusicTonggle.OnClickEvent -= SetEnableMainMusic;
        _mainMusicSlider.OnMoveEvent -= SetVolumeMainMusic;
    }

}
