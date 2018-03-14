using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] private GameObject[] enemysPrefabs;

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

    private void Update() {
        //TODO replace this with some smawn-system
        if (Input.GetKeyDown(KeyCode.Space)) {
            Spawn(EnemyType.FirstClassHS);
        }
    }

    public void Spawn(EnemyType type) {
        GameObject ins = Instantiate(enemysPrefabs[(int)type]);
        ins.transform.position = transform.position;
    }
}
