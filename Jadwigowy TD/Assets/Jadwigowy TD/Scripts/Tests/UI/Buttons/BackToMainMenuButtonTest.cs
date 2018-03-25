using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace UI {
    public class BackToMainMenuButtonTest {

        [UnityTest]
        public IEnumerator BacksToMenuWhenClicked() {
            var button = new GameObject("Button");
            button.AddComponent<BackToMainMenuButton>().BackToMainMenu();
            yield return null;
            Assert.AreEqual(SceneManager.GetActiveScene().name, "Main Menu");
        }
    }
}
