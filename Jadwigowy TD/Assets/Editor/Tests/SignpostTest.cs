using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using NUnit.Framework;
using System.Collections;

namespace Tests {
    public class SignpostTest {

        [Test]
        public void AreSignpostsInCurrentSceneCorrect() {

            Signpost[] signposts = GameObject.FindObjectsOfType<Signpost>();

            if (signposts.Length > 0) {
                Assert.IsTrue(GameObject.FindGameObjectsWithTag(Signpost.StartSignpostTag).Length == 1);

                int i = 0;
                foreach (Signpost signpost in signposts) {
                    if (signpost.SingpostType == Signpost.Type.End)
                        i++;
                }
                Assert.IsTrue(i == 1);
            }
        }
    }
}
