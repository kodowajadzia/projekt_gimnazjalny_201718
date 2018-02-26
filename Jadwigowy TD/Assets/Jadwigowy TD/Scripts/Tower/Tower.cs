using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] private float range = 3, dmg = 1, cooldown = 1;
    private float timer;
    private Enemy target;
    [SerializeField] private GameObject bullet;

#if UNITY_EDITOR
    [SerializeField] private bool showTarget = true, showRange = true;
#endif

    void Update() {
        if (target == null || IsOutOfRange(target.transform.position))
            target = GetEnemyOrNull();
        timer += Time.deltaTime;

        if (target != null) {
            LookAtTarget();
            if (timer > cooldown) {
                timer = 0;
                Attack();
            }
        }

#if UNITY_EDITOR
        if (showTarget && target != null)
            Debug.DrawLine(transform.position, target.transform.position, Color.yellow);
#endif
    }

    private bool IsOutOfRange(Vector3 targetPosition) {
        float distace = Vector2.Distance(transform.position, targetPosition);
        return distace > range;
    }

    private Enemy GetEnemyOrNull() {
        Enemy enemy = null;
        Enemy[] potentialEnemies = FindObjectsOfType<Enemy>();

        int i = 0;
        while (enemy == null && i < potentialEnemies.Length) {
            if (!IsOutOfRange(potentialEnemies[i].transform.position))
                enemy = potentialEnemies[i];
            i++;
        }

        return enemy;
    }

    private void Attack() {
        GameObject ins = Instantiate(bullet);
        ins.transform.position = transform.position;
        ins.transform.rotation = transform.rotation;
        ins.GetComponent<Bullet>().SetDmg(dmg);
    }

    private void LookAtTarget() {
        Vector3 relativePos = target.transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(relativePos);
        rot *= Quaternion.Euler(0, 90, 0);
        transform.rotation = rot;
    }

#if UNITY_EDITOR
    private readonly Color TransparentWhite = new Color(1, 1, 1, 0.5f);

    private void OnDrawGizmos() {
        if (showRange) {
            Gizmos.color = TransparentWhite;
            Gizmos.DrawSphere(transform.position, range);
        }
    }
#endif
}
