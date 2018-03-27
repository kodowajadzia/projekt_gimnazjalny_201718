using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSettinsApplyer : MonoBehaviour {

    public AudioMixer masterMixer;
    public const string MasterVolumeMixerTag = "MasterVolume";

    private const string FullscreenTag = "Fullscreen";
    private const string MasterVolumeTag = "Master Volume";

    void Start () {
        ApplySettings(LoadSettingsData());
	}

    public static void SaveSettingsData(SettingsData settings) {
        PlayerPrefs.SetInt(FullscreenTag, settings.isFullscreen);
        PlayerPrefs.SetFloat(MasterVolumeTag, settings.masterVolume);
    }

    public static SettingsData LoadSettingsData() {
        return new SettingsData() {
            isFullscreen = PlayerPrefs.GetInt(FullscreenTag, 1),
            masterVolume = PlayerPrefs.GetFloat(MasterVolumeTag, 0f)
        };
    }

    public void ApplySettings(SettingsData settings) {
        Screen.fullScreen = settings.isFullscreen == 1;
        masterMixer.SetFloat(MasterVolumeMixerTag, settings.masterVolume);
    }
}
