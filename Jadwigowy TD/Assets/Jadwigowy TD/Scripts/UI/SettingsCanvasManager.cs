using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI {
    public class SettingsCanvasManager : CanvasManager {

        public Text pauseText;
        public bool pause = true;

        public Toggle fullscreenToggle;
        public Slider masterVolumeSlider;

        new protected void Awake() {
            base.Awake();

            if (pauseText) {
                if (pause)
                    pauseText.text = "PAUZA";
                else
                    pauseText.text = "";
            } else Debug.LogWarning("PauseText is not set.");
        }

        protected void Start() {
            SetSettingsMenu(PlayerSettingsApplier.LoadSettingsData());
        }

        public void SetSettingsMenu(SettingsData settings) {
            if (fullscreenToggle)
                fullscreenToggle.isOn = settings.isFullscreen == 1;
            else
                Debug.LogWarning("FullscreenToggle is not set.");
            if (masterVolumeSlider)
                masterVolumeSlider.value = settings.masterVolume;
            else
                Debug.LogWarning("MasterVolumeSlider is not set.");
        }

        public void SaveSettings() {
            var settingsApplyer = FindObjectOfType<PlayerSettingsApplier>();
            SettingsData settingsFromMenu = GetSettingsFromMenu();
            if (settingsApplyer)
                settingsApplyer.ApplySettings(settingsFromMenu);
            else
                Debug.LogWarning("There is PlayerSettinsApplyer in the scene");
            PlayerSettingsApplier.SaveSettingsData(settingsFromMenu);
        }

        public SettingsData GetSettingsFromMenu() {
            return new SettingsData() {
                isFullscreen = (fullscreenToggle.isOn) ? (byte)1 : (byte)0,
                masterVolume = masterVolumeSlider.value
            };
        }
    }
}
