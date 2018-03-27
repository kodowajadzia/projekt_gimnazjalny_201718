using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI {
    public class SettingsCanvasManager : MonoBehaviour {

        public Canvas canvas;
        public Text pauseText;
        public bool pause = true;

        public Toggle fullscreenToggle;
        public Slider masterVolumeSlider;

        void Awake() {
            if (pauseText) {
                if (pause)
                    pauseText.text = "PAUZA";
                else
                    pauseText.text = "";
            } else Debug.LogWarning("PauseText is not set.");

            if (canvas)
                Hide();
            else
                Debug.LogWarning("Canvas is not set.");
        }

        public void ChangeState() {
            if (canvas.enabled)
                Hide();
            else
                Show();
        }

        public void Show() {
            if (canvas) {
                canvas.enabled = true;
                if (pause)
                    Time.timeScale = 0;
            } else
                Debug.LogWarning("Canvas is not set.");
        }

        public void Hide() {
            if (canvas) {
                canvas.enabled = false;
                if (pause)
                    Time.timeScale = 1;
            } else
                Debug.LogWarning("Canvas is not set.");
        }

        private void Start() {
            SetSettingsMenu(PlayerSettinsApplyer.LoadSettingsData());
        }

        public void SetSettingsMenu(SettingsData settings) {
            fullscreenToggle.isOn = settings.isFullscreen == 1;
            masterVolumeSlider.value = settings.masterVolume;
        }

        public void SaveSettings() {
            var settingsApplyer = FindObjectOfType<PlayerSettinsApplyer>();
            SettingsData settingsFromMenu = GetSettingsFromMenu();
            if (settingsApplyer)
                settingsApplyer.ApplySettings(settingsFromMenu);
            else
                Debug.LogWarning("There is PlayerSettinsApplyer in the scene");
            PlayerSettinsApplyer.SaveSettingsData(settingsFromMenu);
        }

        public SettingsData GetSettingsFromMenu() {
            return new SettingsData() {
                isFullscreen = (fullscreenToggle.isOn) ? 1 : 0,
                masterVolume = masterVolumeSlider.value
            };
        }
    }
}
