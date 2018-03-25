using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class BulletTest {

    private GameObject bullet;

    [UnityTest]
	public IEnumerator Moves() {
        bullet = new GameObject("Bullet");
        bullet.AddComponent<Bullet>();
		yield return null;
        Assert.Greater(bullet.transform.position.x, 0);
	}

    [TearDown]
    public void TearDown() {
        GameObject.Destroy(bullet);
    }
}
