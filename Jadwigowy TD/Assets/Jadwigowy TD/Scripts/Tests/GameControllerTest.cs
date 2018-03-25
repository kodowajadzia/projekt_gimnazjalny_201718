using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class GameControllerTest {

    [Test]
    public void TellsAreEnemiesAlive() {
        GameController gameController = new GameObject("Game Controller").AddComponent<GameController>();

        Assert.IsFalse(gameController.AreEnemiesAlive);
        Enemy enemy = new GameObject("Enemy").AddComponent<Enemy>();
        Assert.IsTrue(gameController.AreEnemiesAlive);

        GameObject.Destroy(gameController.gameObject);
        GameObject.Destroy(enemy.gameObject);
    }

    [UnityTest]
    public IEnumerator SpawnsWaves() {
        GameController gameController = new GameObject("Game Controller").AddComponent<GameController>();
        gameController.DifficultyIncrease = 5f;
        gameController.DifficultyRange = 9f;
        gameController.WaveDelayInSeconds = 0.1f;
        gameController.SpawnCooldownInSeconds = 0.1f;
        gameController.Spawner = new GameObject("Spawner").AddComponent<EnemySpawner>();
        var enemy0 = new GameObject("Enemy0") { tag = Enemy.Tag };
        var enemy1 = new GameObject("Enmye1") { tag = Enemy.Tag };
        gameController.EnemiesPrefabs = new GameObject[] { enemy0, enemy1 };

        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(gameController.WaveDelayInSeconds + (gameController.SpawnCooldownInSeconds * 5f));

        GameObject[] spawnedEnemies;
        spawnedEnemies = GameObject.FindGameObjectsWithTag(Enemy.Tag);
        Assert.AreEqual(7 /* 2 already created and 5 spawned */, spawnedEnemies.Length);
        foreach (GameObject enemy in spawnedEnemies) {
            if (enemy.GetInstanceID() != enemy0.GetInstanceID() &&
                enemy.GetInstanceID() != enemy1.GetInstanceID()) {
                StringAssert.Contains(enemy0.name, enemy.name);
                GameObject.Destroy(enemy);
            }
        }

        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(gameController.WaveDelayInSeconds + (gameController.SpawnCooldownInSeconds * 10f));

        spawnedEnemies = GameObject.FindGameObjectsWithTag(Enemy.Tag);
        Assert.AreEqual(12 /* 2 already created and 10 spawned */, spawnedEnemies.Length);
        foreach (GameObject enemy in spawnedEnemies) {
            if (enemy.GetInstanceID() != enemy0.GetInstanceID() &&
                enemy.GetInstanceID() != enemy1.GetInstanceID()) {
                StringAssert.Contains(enemy1.name, enemy.name);
                GameObject.Destroy(enemy);
            }
        }

        GameObject.Destroy(gameController);
        GameObject.Destroy(enemy0);
        GameObject.Destroy(enemy1);
    }

    [UnityTest]
    public IEnumerator EndsGameAfterTooManyPasses() {
        GameController gameController = new GameObject("Game Controller").AddComponent<GameController>();
        gameController.MaxPasses = 1;
        Signpost endSignpost = new GameObject("End Signpost") { tag = Signpost.StartSignpostTag }.AddComponent<Signpost>();
        Enemy enemy = new GameObject("Enemy").AddComponent<Enemy>();

        yield return null;
        yield return null;

        Assert.IsFalse(gameController.IsPlaying);

        GameObject.Destroy(gameController);
        GameObject.Destroy(endSignpost);
        GameObject.Destroy(enemy);
    }
}
