using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI {
    public class DraggableTeacher : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

        [SerializeField] private float price;
        private Vector2 startPos;
        [SerializeField] private GameObject towerPref;

        public void OnBeginDrag(PointerEventData eventData) {
            GameController gameController = FindObjectOfType<GameController>();
            if(price <= gameController.Money) {
                gameController.Money -= price;
                startPos = transform.position;
            }
        }

        public void OnDrag(PointerEventData eventData) {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (IsPositionCorrect(eventData.position)) {
                Vector2 realPos = Camera.main.ScreenToWorldPoint(eventData.position);
                PutTower(realPos);
            }
            transform.position = startPos;
        }

        public bool IsPositionCorrect(Vector2 pos) {
            return true;//TODO IsPositionCerrect funcion
        }

        public void PutTower(Vector2 pos) {
            GameObject ins = Instantiate(towerPref);
            ins.transform.position = pos;
        }
    }
}
