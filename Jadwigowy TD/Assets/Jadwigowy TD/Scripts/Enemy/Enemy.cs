using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class Enemy : MonoBehaviour, IMonoBehaviourTest {

    private Signpost targetSignpost;
    [SerializeField]
    private float
        speed = 1,
        minDistance = 0.1f,
        health = 10;
    private bool passed;
    public const string Tag = "Enemy";

#if UNITY_EDITOR
    [SerializeField] private bool showPath = true;
    public bool IsTestFinished { get; private set; }
#endif

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
            RotateTo(targetSignpost.transform);
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

    private void RotateTo(Transform target) {
        Vector3 relativePos = target.transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(relativePos);
        rot *= Quaternion.Euler(0, 90, 0) * Quaternion.Euler(0, 180, 0);
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

    // TODO rename this method with a better name
    private void Attack() {
        FindObjectOfType<GameController>().Pass();
        Die();
        IsTestFinished = true;
    }

    public void Hit(float dmg) {
        health -= dmg;
        if (health <= 0)
            Die();
    }

    private void Die() {
        Destroy(gameObject);
    }

    public float GetHealth() {
        return health;
    }
}
