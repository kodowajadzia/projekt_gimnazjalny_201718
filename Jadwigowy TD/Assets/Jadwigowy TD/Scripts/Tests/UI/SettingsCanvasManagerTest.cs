using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace UI {
    public class SettingsCanvasManagerTest {

        private GameObject settingsCanvas;
        private Canvas canvas;
        private SettingsCanvasManager settingsCanvasManager;

        [OneTimeSetUp]
        public void SetUp() {
            settingsCanvas = new GameObject("Settings Canvas");
            canvas = settingsCanvas.AddComponent<Canvas>();
            settingsCanvasManager = settingsCanvas.AddComponent<SettingsCanvasManager>();
            settingsCanvasManager.canvas = canvas;
        }

        [Test]
        public void ShowsCanvas() {
            canvas.enabled = false;
            settingsCanvasManager.Show();

            Assert.IsTrue(canvas.enabled);

            canvas.enabled = true;
            settingsCanvasManager.Show();

            Assert.IsTrue(canvas.enabled);
        }

        [Test]
        public void HidesCanvas() {
            canvas.enabled = false;
            settingsCanvasManager.Hide();

            Assert.IsFalse(canvas.enabled);

            canvas.enabled = true;
            settingsCanvasManager.Hide();

            Assert.IsFalse(canvas.enabled);
        }

        [Test]
        public void ChangesState() {
            canvas.enabled = false;
            settingsCanvasManager.ChangeState();

            Assert.IsTrue(canvas.enabled);

            canvas.enabled = true;
            settingsCanvasManager.ChangeState();

            Assert.IsFalse(canvas.enabled);
        }

        [OneTimeTearDown]
        public void TearDown() {
            GameObject.Destroy(settingsCanvas);
        }
    }
}
