using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnemySpawnerTest {

    private EnemySpawner spawner;
    private GameObject clone;

    [SetUp]
    public void SetUp() {;
        spawner = new GameObject("Spawner").AddComponent<EnemySpawner>();
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
