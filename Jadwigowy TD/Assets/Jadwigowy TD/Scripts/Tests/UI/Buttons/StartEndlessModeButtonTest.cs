using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace UI {
    public class StartEndlessModeButtonTest {

        [UnityTest]
        [Ignore("SceneManager")]
        public IEnumerator StartsEndlessModeWhenClicked() {
            var button = new GameObject("Button");
            button.AddComponent<StartEndlessModeButton>().StartMode();

            yield return null;

            Assert.AreEqual(SceneManager.GetActiveScene().name, "Endless Mode");
        }
    }
}