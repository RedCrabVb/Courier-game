using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game.Player;

namespace Game.save
{
    public class EndOfLevel : MonoBehaviour
    {
        public UnityEvent endOfLevel;
        public Text end_text;
        public bool isEnd = false;
        private void Start()
        {
            PlayerPrefs.SetString("NotPassedLevel", SceneManager.GetActiveScene().name);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<RigidbodyFirstPersonController>())
            {
                MouseLook.cursor_enable();
                if (end_text != null && isEnd)
                    end_text.text = "Вы справились с заказом. \n" + PlayerPrefs.GetString("End");
                endOfLevel.Invoke();
            }
        }
    }
}