using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI {
    public class DraggableTeacher : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

        [SerializeField] private float price;
        public Text priceText;
        private Vector2 startPos;
        [SerializeField] private GameObject towerPref;
        private bool buyed;

        private void Start() {
            priceText.text = price + GameController.Currency;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            GameController gameController = FindObjectOfType<GameController>();
            startPos = transform.position;
            if (price <= gameController.money) {
                gameController.money -= price;
                buyed = true;
            }
        }

        public void OnDrag(PointerEventData eventData) {
            if (buyed)
                transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (IsPositionCorrect(eventData.position) && buyed) {
                Vector2 realPos = Camera.main.ScreenToWorldPoint(eventData.position);
                PutTower(realPos);
            }
            transform.position = startPos;
            buyed = false;
        }

        // TODO: IsPositionCerrect funcion
        public bool IsPositionCorrect(Vector2 pos) {
            return true;
        }

        public void PutTower(Vector2 pos) {
            GameObject ins = Instantiate(towerPref);
            ins.transform.position = pos;
        }
    }
}
