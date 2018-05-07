using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSettingsApplier : MonoBehaviour {

    public AudioMixer masterMixer;
    public const string MasterVolumeMixerTag = "MasterVolume";

    public const string FullscreenTag = "Fullscreen";
    public const string MasterVolumeTag = "Master Volume";

    public void Start() {
        ApplySettings(LoadSettingsData());
    }

    public void ApplySettings(SettingsData settings) {
        Screen.fullScreen = settings.isFullscreen == 1;
        if (masterMixer)
            masterMixer.SetFloat(MasterVolumeMixerTag, settings.masterVolume);
        else
            Debug.LogWarning("MasterMixer is not set.");
    }

    public static void SaveSettingsData(SettingsData settings) {
        PlayerPrefs.SetInt(FullscreenTag, settings.isFullscreen);
        PlayerPrefs.SetFloat(MasterVolumeTag, settings.masterVolume);
    }

    public static SettingsData LoadSettingsData() {
        return new SettingsData() {
            isFullscreen = (byte)PlayerPrefs.GetInt(FullscreenTag, 0),
            masterVolume = PlayerPrefs.GetFloat(MasterVolumeTag, 0f)
        };
    }
}
