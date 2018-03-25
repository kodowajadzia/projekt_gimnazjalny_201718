using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TowerTest {

	[UnityTest]
	public IEnumerator LooksAtTarget() {
        Tower tower = new GameObject("Tower").AddComponent<Tower>();
        tower.Range = 1;
        Enemy enemy = new GameObject("Enemy").AddComponent<Enemy>();
        enemy.transform.position = new Vector2(0, 1.1f);

        yield return null;

        Assert.AreEqual(0, tower.transform.rotation.eulerAngles.z, 3);

        enemy.transform.position = new Vector2(0, 0.9f);

        yield return null;

        Assert.AreEqual(90, tower.transform.rotation.eulerAngles.z, 3);

        enemy.transform.position = new Vector2(0, -1.1f);

        yield return null;

        Assert.AreEqual(90, tower.transform.rotation.eulerAngles.z, 3);

        GameObject.Destroy(tower);
        GameObject.Destroy(enemy);
    }

    [UnityTest]
    public IEnumerator ShootsToTarget() {
        Tower tower = new GameObject("Tower").AddComponent<Tower>();
        tower.Range = 1;
        tower.Cooldown = 0.1f;
        GameObject bullet = new GameObject("Bullet");
        bullet.AddComponent<Bullet>();
        tower.Bullet = bullet;
        Enemy enemy = new GameObject("Enemy").AddComponent<Enemy>();
        enemy.transform.position = new Vector2(0, 1.1f);

        yield return new WaitForSeconds(0.11f);

        Assert.AreEqual(1 /* 1 is already created */, GameObject.FindObjectsOfType<Bullet>().Length);

        enemy.transform.position = new Vector2(0, 0.9f);

        yield return new WaitForSeconds(0.11f);

        Assert.AreEqual(2 /* 1 is already created */, GameObject.FindObjectsOfType<Bullet>().Length);

        yield return new WaitForSeconds(0.11f);

        Assert.AreEqual(3 /* 1 is already created */, GameObject.FindObjectsOfType<Bullet>().Length);

        GameObject.Destroy(tower);
        GameObject.Destroy(enemy);
        foreach (Bullet b in GameObject.FindObjectsOfType<Bullet>()) {
            GameObject.Destroy(b);
        }
    }
}
