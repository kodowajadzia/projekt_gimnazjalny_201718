using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace Tests {
    public class PlayerSettingsApplierTest {

        // TODO: audioMixer testing
        // TODO: fix it

        protected AudioMixer audioMixer;
        protected PlayerSettingsApplier settinsApplier;

        private byte isFullscreen;
        private float masterVolume;

        [OneTimeSetUp]
        public void SetUp() {
            isFullscreen = (byte)PlayerPrefs.GetInt(PlayerSettingsApplier.FullscreenTag, 1);
            masterVolume = PlayerPrefs.GetFloat(PlayerSettingsApplier.MasterVolumeTag);

            settinsApplier = new GameObject("PlayerSettingsApplier").AddComponent<PlayerSettingsApplier>();
        }

        [Test]
        [Ignore("doesn't work")]
        public void AppliesSettings() {
            SettingsData settings;

            settings = new SettingsData() {
                isFullscreen = 1,
                masterVolume = 10f
            };
            settinsApplier.ApplySettings(settings);

            Assert.IsTrue(Screen.fullScreen);

            settings = new SettingsData() {
                isFullscreen = 0,
                masterVolume = -10f
            };
            settinsApplier.ApplySettings(settings);

            Assert.IsFalse(Screen.fullScreen);
        }

        [UnityTest]
        [Ignore("doesn't work")]
        public IEnumerator AppliesSettingsAtStart() {
            PlayerPrefs.SetInt(PlayerSettingsApplier.FullscreenTag, 1);
            settinsApplier.Start();

            yield return null;

            Assert.IsTrue(Screen.fullScreen);

            PlayerPrefs.SetInt(PlayerSettingsApplier.FullscreenTag, 0);
            settinsApplier.Start();

            yield return null;

            Assert.IsFalse(Screen.fullScreen);
        }

        [Test]
        [Ignore("doesn't work")]
        public void SavesSettingsData() {
            SettingsData settings;

            settings = new SettingsData() {
                isFullscreen = 1,
                masterVolume = 10f
            };
            PlayerSettingsApplier.SaveSettingsData(settings);

            Assert.AreEqual(1, PlayerPrefs.GetInt(PlayerSettingsApplier.FullscreenTag));
            Assert.AreEqual(10f, PlayerPrefs.GetInt(PlayerSettingsApplier.MasterVolumeTag));

            settings = new SettingsData() {
                isFullscreen = 0,
                masterVolume = -10f
            };
            PlayerSettingsApplier.SaveSettingsData(settings);

            Assert.AreEqual(0, PlayerPrefs.GetInt(PlayerSettingsApplier.FullscreenTag));
            Assert.AreEqual(-10f, PlayerPrefs.GetInt(PlayerSettingsApplier.MasterVolumeTag));
        }

        [Test]
        public void LoadsSettingsData() {
            SettingsData settings;

            PlayerPrefs.SetInt(PlayerSettingsApplier.FullscreenTag, 0);
            PlayerPrefs.SetFloat(PlayerSettingsApplier.MasterVolumeTag, 10f);

            settings = PlayerSettingsApplier.LoadSettingsData();

            Assert.AreEqual(settings.isFullscreen, 0);
            Assert.AreEqual(settings.masterVolume, 10f);

            PlayerPrefs.SetInt(PlayerSettingsApplier.FullscreenTag, 1);
            PlayerPrefs.SetFloat(PlayerSettingsApplier.MasterVolumeTag, -10f);

            settings = PlayerSettingsApplier.LoadSettingsData();

            Assert.AreEqual(1, settings.isFullscreen);
            Assert.AreEqual(-10f, settings.masterVolume);
        }

        [OneTimeTearDown]
        public void TearDown() {
            GameObject.Destroy(settinsApplier);

            PlayerPrefs.SetInt(PlayerSettingsApplier.FullscreenTag, isFullscreen);
            PlayerPrefs.SetFloat(PlayerSettingsApplier.MasterVolumeTag, masterVolume);
        }
    }
}
