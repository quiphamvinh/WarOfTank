using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    [Header("------Audio Source------")]
    public AudioSource musicSource;
    public AudioSource SFXSource;
    [Header("------Audio Clip------")]
    public AudioClip background;
    public AudioClip Shoot;
    public AudioClip Engine;
    public AudioClip hurt;
    public AudioClip death;
    public AudioClip addHealth;
    public AudioClip addCoin;
    public AudioClip addSpeed;
    public AudioClip box;
    public AudioClip pressButton;
    public AudioClip pressButtonBuy;
    public AudioClip firegas;

    public AudioClip winlevel;
    public AudioClip losegame;

    public AudioClip warning;




    public AudioClip[] sfxClips;
    public List<AudioSource> sfxSources;
    [Header("------Audio Slider------")]
    public Slider volumeSlider;
    public Slider sfxSlider;
    [Header("------Audio Button------")]
    public Button volumeMuteBtn;
    public Button sfxMuteBtn;
    [Header("------Text------")]
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI sfxText;

    private const string volumeKey = "backgroundVolume";
    private const string sfxKey = "sfxVolume";
    private const string volumeMuteKey = "volumeMute";
    private const string sfxMuteKey = "sfxMute";

    private bool isVolumeMuted = false;
    private bool isSFXMuted = false;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        float volume = PlayerPrefs.GetFloat(volumeKey, AudioListener.volume);
        float sfxVolume = PlayerPrefs.GetFloat(sfxKey, AudioListener.volume);
        SetVolume(volume);
        SetSFXVolume(sfxVolume);

        volumeSlider.value = PlayerPrefs.GetFloat(volumeKey, AudioListener.volume);
        sfxSlider.value = PlayerPrefs.GetFloat(sfxKey, AudioListener.volume);


        UpdateVolumeText(volumeSlider.value);
        UpdateSFXText(sfxSlider.value);


        volumeSlider.onValueChanged.AddListener(delegate { UpdateVolumeText(volumeSlider.value); });
        sfxSlider.onValueChanged.AddListener(delegate { UpdateSFXText(sfxSlider.value); });


        isVolumeMuted = PlayerPrefs.GetInt(volumeMuteKey, 0) == 1;
        isSFXMuted = PlayerPrefs.GetInt(sfxMuteKey, 0) == 1;

        if (isVolumeMuted) ToggleVolumeMute();
        if (isSFXMuted) ToggleSFXMute();

        sfxClips = new AudioClip[] { Shoot,Engine, hurt, death, addHealth, addCoin, addSpeed, box, firegas };
        sfxSources = new List<AudioSource> { SFXSource };
        for (int i = 1; i < sfxClips.Length; i++)
        {
            sfxSources.Add(gameObject.AddComponent<AudioSource>());
        }
        for (int i = 0; i < sfxSources.Count; i++)
        {
            sfxSources[i].clip = sfxClips[i];
        }
    }
    public void UpdateVolumeText(float volume)
    {
        volumeText.text = Mathf.RoundToInt(volume * 100) + "%";
    }

    public void UpdateSFXText(float volume)
    {
        sfxText.text = Mathf.RoundToInt(volume * 100) + "%";
    }
    private void Update()
    {
        SetVolume(volumeSlider.value);
        SetSFXVolume(sfxSlider.value);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat(volumeKey, volume);
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(sfxKey, volume);

        for (int i = 0; i < sfxClips.Length; i++)
        {
            if (i < sfxSources.Count && sfxSources[i] != null)
            {
                sfxSources[i].volume = volume;
            }
        }
    }

    public void ToggleVolumeMute()
    {
        isVolumeMuted = !isVolumeMuted;

        if (isVolumeMuted)
        {
            float volume = musicSource.volume;
            PlayerPrefs.SetFloat(volumeKey + "old", volume);
            volumeSlider.value = 0f;
            PlayerPrefs.SetFloat(volumeKey, volume);
            musicSource.volume = 0f;
            PlayerPrefs.SetInt(volumeMuteKey, 1);
        }
        else
        {
            float volume = PlayerPrefs.GetFloat(volumeKey + "old", AudioListener.volume);
            volumeSlider.value = volume;
            musicSource.volume = volume;
            PlayerPrefs.SetInt(volumeMuteKey, 0);
        }
    }

    public void ToggleSFXMute()
    {
        isSFXMuted = !isSFXMuted;

        if (isSFXMuted)
        {
            float volume = sfxSources[0].volume;
            PlayerPrefs.SetFloat(sfxKey + "old", volume);
            sfxSlider.value = 0f;
            PlayerPrefs.SetFloat(sfxKey, volume);
            foreach (AudioSource soundEffect in sfxSources)
            {
                soundEffect.volume = 0f;
            }
            PlayerPrefs.SetInt(sfxMuteKey, 1);
        }
        else
        {
            float volume = PlayerPrefs.GetFloat(sfxKey + "old", AudioListener.volume);
            sfxSlider.value = volume;
            foreach (AudioSource soundEffect in sfxSources)
            {
                soundEffect.volume = volume;
            }
            PlayerPrefs.SetInt(sfxMuteKey, 0);
        }
    }
    public void PlayButtonClickSound()
    {
        SFXSource.PlayOneShot(pressButton);
    }
    public void PlayButtonClickBuySound()
    {
        SFXSource.PlayOneShot(pressButtonBuy);
    }
}