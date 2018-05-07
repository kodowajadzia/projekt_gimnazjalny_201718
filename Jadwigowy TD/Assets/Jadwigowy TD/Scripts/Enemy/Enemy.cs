using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class Enemy : MonoBehaviour, IMonoBehaviourTest {

    private Signpost targetSignpost;
    public float
        speed = 1,
        minDistance = 0.1f,
        health = 10,
        costPerKill = 10f;
    public int scorePerKill = 1;
    private bool passed;
    public const string Tag = "Enemy";

#if UNITY_EDITOR
    [SerializeField] private bool showPath = true;
#endif

    public bool IsTestFinished { get; private set; }

    private void Awake() {
        // There must be a starting signpost with a "Start Signpost" tag.
        GameObject startSignpost = GameObject.FindGameObjectWithTag(Signpost.StartSignpostTag);
        if (startSignpost)
            targetSignpost = startSignpost.GetComponent<Signpost>();
        else
            Debug.LogWarning("There is no starting signpost.");
    }

    private void Update() {
        if (targetSignpost != null) {
            LookAt(targetSignpost.transform);
            MoveForward();

            if (targetSignpost.SingpostType == Signpost.Type.End)
                passed = Vector2.Distance(transform.position, targetSignpost.transform.position) < minDistance;

            if (Vector2.Distance(transform.position, targetSignpost.transform.position) < minDistance)
                targetSignpost = targetSignpost.NextSignpost;

#if UNITY_EDITOR
            if (showPath)
                ShowPath();
#endif
        } else if (passed)
            Attack();
    }

    public void LookAt(Transform target) {
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

    private void MoveForward() {
        Vector3 move = new Vector3(speed, 0);
        move *= Time.deltaTime;
        transform.Translate(move);
    }

#if UNITY_EDITOR
    private void ShowPath() {
        if (targetSignpost != null)
            Debug.DrawLine(transform.position, targetSignpost.transform.position, Color.blue);
    }
#endif

    // TODO: rename this method with a better name
    private void Attack() {
        FindObjectOfType<GameController>().Pass();
        Die();
        IsTestFinished = true;
    }

    public void Hit(float dmg) {
        health -= dmg;
        if (health <= 0) {
            GameController gc = FindObjectOfType<GameController>();
            gc.IncreaseScore(scorePerKill);
            gc.IncreaseMoney(costPerKill);
            Die();
        }
    }

    private void Die() {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public float GetHealth() {
        return health;
    }
}
