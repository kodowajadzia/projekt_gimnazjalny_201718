using UI;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections;

namespace Tests.UI {
    public class SettingsCanvasManagerTest {

        protected Toggle fullscreenToggle;
        protected Slider masterVolumeSlider;

        protected SettingsCanvasManager settingsCanvasManager;

        [OneTimeSetUp]
        public void SetUp() {
            fullscreenToggle = new GameObject("Toggle").AddComponent<Toggle>();
            masterVolumeSlider = new GameObject("Slider").AddComponent<Slider>();
            masterVolumeSlider.minValue = -80f;
            masterVolumeSlider.maxValue = 20f;

            settingsCanvasManager = new GameObject("Settings Canvas Manager").AddComponent<SettingsCanvasManager>();
            settingsCanvasManager.fullscreenToggle = fullscreenToggle;
            settingsCanvasManager.masterVolumeSlider = masterVolumeSlider;
        }

        [Test]
        public void GetsSettingsFromMenu() {
            SettingsData settings;

            fullscreenToggle.isOn = true;
            masterVolumeSlider.value = 10f;

            settings = settingsCanvasManager.GetSettingsFromMenu();

            Assert.AreEqual(1, settings.isFullscreen);
            Assert.AreEqual(10f, settings.masterVolume);

            fullscreenToggle.isOn = false;
            masterVolumeSlider.value = -10f;

            settings = settingsCanvasManager.GetSettingsFromMenu();

            Assert.AreEqual(0, settings.isFullscreen);
            Assert.AreEqual(-10f, settings.masterVolume);
        }

        [Test]
        public void SetsSettingsMenu() {
            SettingsData settings;

            settings = new SettingsData() {
                isFullscreen = 1,
                masterVolume = 10f
            };
            settingsCanvasManager.SetSettingsMenu(settings);

            Assert.IsTrue(fullscreenToggle.isOn);
            Assert.AreEqual(10f, masterVolumeSlider.value);

            settings = new SettingsData() {
                isFullscreen = 0,
                masterVolume = -10f
            };
            settingsCanvasManager.SetSettingsMenu(settings);

            Assert.IsFalse(fullscreenToggle.isOn);
            Assert.AreEqual(-10f, masterVolumeSlider.value);
        }

        [Test]
        public void SavesSettings() {
            byte isFullscreen = (byte)PlayerPrefs.GetInt(PlayerSettingsApplier.FullscreenTag);
            float masterVolume = PlayerPrefs.GetFloat(PlayerSettingsApplier.MasterVolumeTag);

            SettingsData settings;

            settings = new SettingsData() {
                isFullscreen = 1,
                masterVolume = 10f
            };
            settingsCanvasManager.SetSettingsMenu(settings);
            settingsCanvasManager.SaveSettings();

            Assert.AreEqual(settings, PlayerSettingsApplier.LoadSettingsData());

            settings = new SettingsData() {
                isFullscreen = 0,
                masterVolume = -10f
            };
            settingsCanvasManager.SetSettingsMenu(settings);
            settingsCanvasManager.SaveSettings();

            Assert.AreEqual(settings, PlayerSettingsApplier.LoadSettingsData());

            PlayerPrefs.SetInt(PlayerSettingsApplier.FullscreenTag, isFullscreen);
            PlayerPrefs.SetFloat(PlayerSettingsApplier.MasterVolumeTag, masterVolume);
        }

        [OneTimeTearDown]
        public void TearDown() {
            GameObject.Destroy(fullscreenToggle);
            GameObject.Destroy(masterVolumeSlider);
            GameObject.Destroy(settingsCanvasManager);
        }
    }
}
