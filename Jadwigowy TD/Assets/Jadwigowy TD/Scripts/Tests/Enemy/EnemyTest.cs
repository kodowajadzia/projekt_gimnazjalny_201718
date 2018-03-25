using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnemyTest {

    private GameObject startSignpost;
    private GameController gameController;

    [SetUp]
    public void SetUp() {
        startSignpost = new GameObject("Start Signpost") {
            tag = Signpost.StartSignpostTag
        };
        startSignpost.AddComponent<Signpost>();
        startSignpost.transform.position = new Vector2(1, 1);

        gameController = new GameObject("Game Controller").AddComponent<GameController>();
    }

    [Test]
    public void LosesHealth() {
        Enemy enemy = new GameObject("Enemy").AddComponent<Enemy>();
        float healthAtStart = enemy.GetHealth();

        enemy.Hit(healthAtStart / 2);

        Assert.AreEqual(enemy.GetHealth(), healthAtStart / 2);

        GameObject.Destroy(enemy.gameObject);
    }

    [UnityTest]
    public IEnumerator DiesWhenHealthIsZero() {
        Enemy enemy = new GameObject("Enemy").AddComponent<Enemy>();
        enemy.Hit(enemy.GetHealth());

        yield return null;// Destroy() needs 1 frame

        Assert.IsFalse(enemy);

        if (enemy) GameObject.Destroy(enemy.gameObject);
    }

    [UnityTest]
    public IEnumerator MovesAndRotatesToTarget() {
        Enemy enemy = new GameObject("Enemy").AddComponent<Enemy>();

        yield return null;

        Assert.Greater(enemy.transform.position.x, 0);
        Assert.Greater(enemy.transform.position.y, 0);
        Assert.AreEqual(enemy.transform.position.x, enemy.transform.position.y, 0.01f);

        Assert.AreEqual(enemy.transform.rotation.eulerAngles.z, 45, 0.01f);

        GameObject.Destroy(enemy.gameObject);
    }

    [UnityTest]
    public IEnumerator Attacks() {
        yield return new MonoBehaviourTest<Enemy>();

        Assert.AreEqual(1, gameController.Passes);
    }

    [UnityTest]
    public IEnumerator DiesAfterAttack() {
        yield return new MonoBehaviourTest<Enemy>();
        yield return null;// Destroy() needs 1 frame

        Assert.IsFalse(GameObject.FindObjectOfType<Enemy>());
    }

    [TearDown]
    public void TearDown() {
        GameObject.Destroy(startSignpost.gameObject);
        GameObject.Destroy(gameController.gameObject);
    }
}
