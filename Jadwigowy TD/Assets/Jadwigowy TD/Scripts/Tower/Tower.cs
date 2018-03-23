using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] private float range = 3, cooldown = 1;
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
    }

    private void LookAtTarget() {
        float x = target.transform.position.x - transform.position.x;
        x = (x < 0) ? -x : x;
        float y = target.transform.position.y - transform.position.y;
        y = (y < 0) ? -y : y;

        float angleZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        if (target.transform.position.x > transform.position.x && target.transform.position.y > transform.position.y)
            angleZ = angleZ;
        else if (target.transform.position.x < transform.position.x && target.transform.position.y > transform.position.y)
            angleZ = 180 - angleZ;
        else if (target.transform.position.x < transform.position.x && target.transform.position.y < transform.position.y)
            angleZ = 180 + angleZ;
        else if (target.transform.position.x > transform.position.x && target.transform.position.y < transform.position.y)
            angleZ = 360 - angleZ;

        float angleY;

        if (target.transform.position.x > transform.position.x)
            angleY = 0;
        else
            angleY = 180;

        Quaternion rot = Quaternion.Euler(0, 0, angleZ) * Quaternion.Euler(angleY, 0, 0);
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
