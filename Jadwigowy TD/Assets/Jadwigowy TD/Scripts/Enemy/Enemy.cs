using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Signpost target, endSingpost;
    public float speed = 1;
    public float minDistance = 0.1f;
    public float health = 10;
    private bool passed;

#if UNITY_EDITOR
    public bool showPath = true;

    private void Awake() {
        CheckStartSignpost();
    }

    private void CheckStartSignpost() {
        GameObject[] startSignposts = GameObject.FindGameObjectsWithTag("Start Signpost");
        Debug.Assert(startSignposts.Length == 1, "Hmm, the emenies don't konw where is the appropriate Start Signpost. Check how many objects have the \"Start Signpost\" tag.");
    }
#endif

    void Start () {
        target = GameObject.FindGameObjectWithTag("Start Signpost").GetComponent<Signpost>();
        endSingpost = GameObject.FindGameObjectWithTag("End Signpost").GetComponent<Signpost>();
    }
	
	void Update () {
        if (passed) {
            Attack();
        } else {
            RotateToTarget();
            MoveToTarget();
            if (Vector2.Distance(transform.position, target.transform.position) < minDistance)
                target = target.nextSignpost;
#if UNITY_EDITOR
            if (showPath)
                ShowPath();
#endif

            passed = Vector2.Distance(transform.position, endSingpost.transform.position) < minDistance;
        }
    }

    private void RotateToTarget() {
        Vector3 relativePos = target.transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(relativePos);
        rot = rot * Quaternion.Euler(0, 90, 0);//przykre, ale "rot *=" nie działa :(
        transform.rotation = rot;
    }

    private void MoveToTarget() {
        Vector3 move = new Vector3(-speed, 0);
        move *= Time.deltaTime;
        transform.Translate(move);
    }

#if UNITY_EDITOR
    private void ShowPath() {
        if (target != null)
            Debug.DrawLine(transform.position, target.transform.position, Color.blue);
    }
#endif

    private void Attack() {
        FindObjectOfType<GameController>().Pass();
        Kill();
    }

    public void Hit(float dmg) {
        health -= dmg;
        if (health <= 0)
            Kill();
    }

    private void Kill() {
        Debug.Log(gameObject.name + " inactivated");
        Destroy(gameObject);
    }
}
