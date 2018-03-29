using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace Tests {
    public class SpawnerTest {

        private Spawner spawner;
        private GameObject clone;

        [SetUp]
        public void SetUp() {
            ;
            spawner = new GameObject("Spawner").AddComponent<Spawner>();
        }

        [Test]
        public void Spawns() {
            clone = spawner.Spawn(new GameObject("Sample"));
            Assert.IsTrue(clone);
            Assert.AreEqual(spawner.transform.position, clone.transform.position);
        }

        [TearDown]
        public void TearDown() {
            GameObject.Destroy(spawner.gameObject);
            if (clone) GameObject.Destroy(clone);
        }
    }
}
