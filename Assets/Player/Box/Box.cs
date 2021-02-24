using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game.Player;

namespace Game.save
{
    public class Box : MonoBehaviour
    {
        public bool isEndPoint = false;
        private bool isSelectedOrder = false;

        public GameObject boxObj;
        public Text text;
        public GameObject img;
        private EndOfLevel endOfLevel;
        private Renderer renderCach;
        private Color colorCach;

        private void Start()
        {
            isSelectedOrder = PlayerPrefs.GetString("selectedOrder") == true.ToString() ? true : false;
            renderCach = isEndPoint ? gameObject.GetComponent<Renderer>() : boxObj.GetComponent<Renderer>();
            colorCach = renderCach.material.color;
            endOfLevel = gameObject.GetComponent<EndOfLevel>();   
        }

        public void showHint()
        {
            text.text = !isSelectedOrder ? "Вы не взяли заказ ..." : "Вы можете закончить выполнение заказа, нажмите ЛКМ";
            text.text = isEndPoint ? text.text : "Для того чтобы взять заказ, нажмите ЛКМ";
            img.SetActive((isEndPoint && isSelectedOrder) || !isEndPoint);
        }

        public void hideHint()
        {
            text.text = "";
            img.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            isSelectedOrder = PlayerPrefs.GetString("selectedOrder") == true.ToString() ? true : false;
            showHint();
            if (!isEndPoint)
            {
                renderCach.material.color = Color.yellow;
            }
            else
            {
                boxObj.SetActive(true);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<RigidbodyFirstPersonController>() && Input.GetMouseButtonDown(1) && (isSelectedOrder || !isEndPoint))
            {
                hideHint();
                gameObject.SetActive(isEndPoint);
                PlayerPrefs.SetString("selectedOrder", (!isEndPoint).ToString());
                if(isEndPoint)
                    endOfLevel.end();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            hideHint();
            renderCach.material.color = colorCach;
            boxObj.SetActive(!isEndPoint);
        }
    }
}