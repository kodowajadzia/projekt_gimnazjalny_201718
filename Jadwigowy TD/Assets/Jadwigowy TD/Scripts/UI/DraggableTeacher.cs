using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI {
    public class DraggableTeacher : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

        [SerializeField] private float price;
        public Text priceText;
        public Image image;
        private Vector2 startPos;
        [SerializeField] private GameObject towerPref;
        private GameController gc;
        public Color cannotAfforColor = Color.black;
        public RectTransform panel;
        public float towersPadding = 1f;
        public AudioClip buySound, cancelSound;
        private AudioSource audioSource;

        private void Awake() {
            gc = FindObjectOfType<GameController>();
            audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        }

        private void Start() {
            priceText.text = price + " " + GameController.Currency;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            startPos = transform.position;
        }

        public void OnDrag(PointerEventData eventData) {
            if (IsBuyalbe())
                transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (IsPositionCorrect(eventData.position) && IsBuyalbe()) {
                gc.money -= price;
                audioSource.PlayOneShot(buySound);
                Vector2 realPos = Camera.main.ScreenToWorldPoint(eventData.position);
                PutTower(realPos);
            }
            transform.position = startPos;
        }

        public bool IsPositionCorrect(Vector2 pos) {
            Vector2 realPos = Camera.main.ScreenToWorldPoint(pos);

            GameObject[] teachers = GameObject.FindGameObjectsWithTag("Teacher");
            bool isOnAnotherTeacher = false;
            foreach(GameObject teacher in teachers) {
                if (Vector2.Distance(realPos, teacher.transform.position) < towersPadding) {
                    isOnAnotherTeacher = true;
                    audioSource.PlayOneShot(cancelSound);
                }
            }

            bool isInPanel = panel.rect.Contains(pos);

            return !isInPanel && !isOnAnotherTeacher;
        }

        public void PutTower(Vector2 pos) {
            GameObject ins = Instantiate(towerPref);
            ins.transform.position = pos;
        }

        private void OnGUI() {
            if(gc.money >= price)
                image.color = Color.white;
            else
                image.color = cannotAfforColor;
        }

        public bool IsBuyalbe() {
            return price <= gc.money;
        }
    }
}
