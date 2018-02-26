using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType {
    ToTests,
};

public class EnemySpawner : MonoBehaviour {

    public GameObject[] enemysPrefabs;

#if UNITY_EDITOR
    private void Awake() {
        CheckEnumAndTablic();
        CheckNullsInTablic();
    }

    private void CheckEnumAndTablic() {
        int enemysEnumLenght = Enum.GetNames(typeof(EnemyType)).Length;
        Debug.Assert(enemysEnumLenght == enemysPrefabs.Length, "Enum EnemyType should respond with the tablic of prefabs.");
    }

    private void CheckNullsInTablic() {
        foreach (GameObject gameObject in enemysPrefabs)
            Debug.Assert(gameObject != null, "A null enemy?");
    }
#endif

    public void Spawn(EnemyType type) {
        GameObject ins = Instantiate(enemysPrefabs[(int)type]);
        ins.transform.position = transform.position;
    }
}
