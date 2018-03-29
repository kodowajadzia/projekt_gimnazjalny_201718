using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace Tests {
    public class BorderTest {

        [UnityTest]
        public IEnumerator BorderDestroysGameObjectWithRigidbodyWhenCollided() {
            var border = new GameObject("Border");
            border.AddComponent<BoxCollider2D>();
            border.GetComponent<BoxCollider2D>().isTrigger = true;
            border.AddComponent<Border>();

            var objectToCollision = new GameObject("Destroy it");
            objectToCollision.AddComponent<BoxCollider2D>().size = new Vector2(1, 1);
            objectToCollision.AddComponent<Rigidbody2D>();

            yield return null;

            Assert.IsFalse(objectToCollision);

            GameObject.Destroy(border);
            if (objectToCollision) GameObject.Destroy(objectToCollision);
        }

    }
}
