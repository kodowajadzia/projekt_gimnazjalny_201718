using UI;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace Tests.UI {
    public class CanvasManagerTest {

        private GameObject canvasGameObject;
        private Canvas canvas;
        private CanvasManager canvasManager;

        [OneTimeSetUp]
        public void SetUp() {
            canvasGameObject = new GameObject("Settings Canvas");
            canvas = canvasGameObject.AddComponent<Canvas>();
            canvasManager = canvasGameObject.AddComponent<CanvasManager>();
            canvasManager.canvas = canvas;
        }

        [Test]
        public void ShowsCanvas() {
            canvas.enabled = false;
            canvasManager.Show();

            Assert.IsTrue(canvas.enabled);

            canvas.enabled = true;
            canvasManager.Show();

            Assert.IsTrue(canvas.enabled);
        }

        [Test]
        public void HidesCanvas() {
            canvas.enabled = false;
            canvasManager.Hide();

            Assert.IsFalse(canvas.enabled);

            canvas.enabled = true;
            canvasManager.Hide();

            Assert.IsFalse(canvas.enabled);
        }

        [Test]
        public void ChangesState() {
            canvas.enabled = false;
            canvasManager.ChangeState();

            Assert.IsTrue(canvas.enabled);

            canvas.enabled = true;
            canvasManager.ChangeState();

            Assert.IsFalse(canvas.enabled);
        }

        [OneTimeTearDown]
        public void TearDown() {
            GameObject.Destroy(canvasGameObject);
        }
    }
}
