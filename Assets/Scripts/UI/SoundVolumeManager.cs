using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeManager : MonoBehaviour
{
    public float generalVolume;
    public float musicVolume;
    public float soundVolume;
    public AudioSource[] musicSources;
    public AudioSource[] soundSources;

    private readonly string generalName = "GeneralVolume";
    private readonly string musicName = "MusicVolume";
    private readonly string soundName = "SoundVolume";
    // Start is called before the first frame update
    void Start()
    {
        generalVolume = PlayerPrefs.GetFloat(generalName);
        musicVolume = PlayerPrefs.GetFloat(musicName);
        soundVolume = PlayerPrefs.GetFloat(soundName);

        if (GetComponent<Slider>() != null)
        {
            switch (this.name)
            {
                case "GeneralSlider":
                    GetComponent<Slider>().value = generalVolume;
                    break;
                case "MusicSlider":
                    GetComponent<Slider>().value = musicVolume;
                    break;
                case "SoundSlider":
                    GetComponent<Slider>().value = soundVolume;
                    break;
            }
        }

        SetSourcesToVolume(musicSources, generalVolume * musicVolume);
        SetSourcesToVolume(soundSources, generalVolume * soundVolume);
    }

    void Update()
    {
        if (GetComponent<Slider>() == null)
        {
            if (!generalVolume.Equals(PlayerPrefs.GetFloat(generalName)))
                UpdateGeneralVolume(PlayerPrefs.GetFloat(generalName));
            if (!musicVolume.Equals(PlayerPrefs.GetFloat(musicName)))
                UpdateMusicVolume(PlayerPrefs.GetFloat(musicName));
            if (!soundVolume.Equals(PlayerPrefs.GetFloat(soundName)))
                UpdateSoundVolume(PlayerPrefs.GetFloat(soundName));
        }
    }

    void SetSourcesToVolume(AudioSource[] sources, float volume)
    {
        foreach (var source in sources)
            source.volume = volume;
    }

    public void UpdateGeneralVolume(float value)
    {
        var tmp = GetComponent<Slider>()?.value;
        if (tmp != null)
            value = (float)tmp;
        generalVolume = value;
        PlayerPrefs.SetFloat(generalName, value);
        SetSourcesToVolume(musicSources, generalVolume * musicVolume);
        SetSourcesToVolume(soundSources, generalVolume * soundVolume);
    }

    public void UpdateSoundVolume(float value)
    {
        var tmp = GetComponent<Slider>()?.value;
        if (tmp != null)
            value = (float)tmp;
        generalVolume = value;
        soundVolume = value;
        PlayerPrefs.SetFloat(soundName, value);
        SetSourcesToVolume(soundSources, generalVolume * soundVolume);
    }

    public void UpdateMusicVolume(float value)
    {
        var tmp = GetComponent<Slider>()?.value;
        if (tmp != null)
            value = (float)tmp;
        generalVolume = value;
        musicVolume = value;
        PlayerPrefs.SetFloat(musicName, value);
        SetSourcesToVolume(musicSources, generalVolume * musicVolume);
    }
}
