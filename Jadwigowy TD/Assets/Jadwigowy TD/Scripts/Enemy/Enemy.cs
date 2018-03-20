using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Signpost targetSignpost;
    [SerializeField] private float
        speed = 1,
        minDistance = 0.1f,
        health = 10;
    private bool passed;
    public const string Tag = "Enemy";

#if UNITY_EDITOR
    [SerializeField] private bool showPath = true;
#endif

    private void Awake() {
        // There must be a starting signpost with a "Start Signpost" tag.
        targetSignpost = GameObject.FindGameObjectWithTag(Signpost.StartSignpostTag).GetComponent<Signpost>();
    }

    private void Update() {
        if (passed) {
            Attack();
        } else {
            RotateToTarget();
            MoveToTarget();

            if (targetSignpost.SingpostType == Signpost.Type.End)
                passed = Vector2.Distance(transform.position, targetSignpost.transform.position) < minDistance;

            if (Vector2.Distance(transform.position, targetSignpost.transform.position) < minDistance)
                targetSignpost = targetSignpost.NextSignpost;

#if UNITY_EDITOR
            if (showPath)
                ShowPath();
#endif  
        }
    }

    private void RotateToTarget() {
        Vector3 relativePos = targetSignpost.transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(relativePos);
        rot *= Quaternion.Euler(0, 90, 0);
        transform.rotation = rot;
    }

    private void MoveToTarget() {
        Vector3 move = new Vector3(-speed, 0);
        move *= Time.deltaTime;
        transform.Translate(move);
    }

#if UNITY_EDITOR
    private void ShowPath() {
        if (targetSignpost != null)
            Debug.DrawLine(transform.position, targetSignpost.transform.position, Color.blue);
    }
#endif

    private void Attack() {
        FindObjectOfType<GameController>().Pass();
        Die();
    }

    public void Hit(float dmg) {
        health -= dmg;
        if (health <= 0)
            Die();
    }

    private void Die() {
        Debug.Log(gameObject.name + " inactivated");
        Destroy(gameObject);
    }
}
