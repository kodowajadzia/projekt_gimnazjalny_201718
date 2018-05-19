using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

public class Enemy : MonoBehaviour, IMonoBehaviourTest {

    private Signpost targetSignpost;
    public float
        speed = 1,
        minDistance = 0.1f,
        health = 10,
        costPerKill = 10f;
    public int scorePerKill = 1;
    public Color hitColor = Color.red;
    public float hitColorDuration = 0.1f;
    private bool passed;
    public const string Tag = "Enemy";
    private HealthBar healthBar;
    private float maxHealth;
    public AudioClip deathSound;
    private AudioSource audioSource;
    private float stuned;

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

        healthBar = GetComponent<HealthBar>();
        maxHealth = health;
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
    }

    private void Update() {
        if (stuned <= 0) {
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
                Pass();
        } else {
            stuned -= Time.deltaTime;
        }
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

    private void Pass() {
        FindObjectOfType<GameController>().Pass();
        FindObjectOfType<PassEffect>().StartEffect();
        Die();
        IsTestFinished = true;
    }

    public void Hit(float dmg, float stun) {
        stuned = stun;
        health -= dmg;
        healthBar.SetHealth(health, maxHealth);
        if (health <= 0) {
            GameController gc = FindObjectOfType<GameController>();
            gc.IncreaseScore(scorePerKill);
            gc.IncreaseMoney(costPerKill);
            Die();
            audioSource.PlayOneShot(deathSound);
        } else {
            StartCoroutine(HitAnimation());
        }
    }

    private IEnumerator HitAnimation() {
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        Color normalColor = sprite.color;
        sprite.color = hitColor;
        yield return new WaitForSeconds(hitColorDuration);
        sprite.color = normalColor;
    }

    private void Die() {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public float GetHealth() {
        return health;
    }
}
