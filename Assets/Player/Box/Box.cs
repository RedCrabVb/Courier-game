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
        private bool isActive = true;

        public GameObject boxObj;
        public Text text;
        public GameObject img;
        private AudioSource audioSource;
        private EndOfLevel endOfLevel;
        private Renderer renderCach;
        private Color colorCach;
        private void Start()
        {
            isSelectedOrder = PlayerPrefs.GetString("selectedOrder") == true.ToString() ? true : false;
            renderCach = isEndPoint ? gameObject.GetComponent<Renderer>() : boxObj.GetComponent<Renderer>();
            audioSource = gameObject.GetComponent<AudioSource>();
            colorCach = renderCach.material.color;
            endOfLevel = gameObject.GetComponent<EndOfLevel>();   
        }

        public void showHint()
        {
            text.text = !isSelectedOrder ? "Вы не взяли заказ ..." : "Вы можете закончить выполнение заказа, нажмите ПКМ";
            text.text = isEndPoint ? text.text : "Для того чтобы взять заказ, нажмите ПКМ";
            text.text = !isActive ? "" : text.text;
            img.SetActive(((isEndPoint && isSelectedOrder) || !isEndPoint) && isActive);
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
                boxObj.SetActive(isActive && isEndPoint);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<RigidbodyFirstPersonController>() && Input.GetMouseButtonDown(1) && (isSelectedOrder || !isEndPoint))
            {
                hideHint();
                audioSource.Play();
                PlayerPrefs.SetString("selectedOrder", (!isEndPoint).ToString());
                isActive = false;
                boxObj.SetActive(isActive);
                if (isEndPoint)
                    endOfLevel.end();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            hideHint();
            renderCach.material.color = colorCach;
            boxObj.SetActive(!isEndPoint && isActive);
        }
    }
}