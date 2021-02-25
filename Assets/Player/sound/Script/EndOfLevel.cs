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
        public bool isEnd;
        
        private void Start()
        {
            PlayerPrefs.SetString("NotPassedLevel", SceneManager.GetActiveScene().name);
        }

        public void end()
        {
            MouseLook.cursor_enable();
            if (end_text != null)
                end_text.text = "Вы справились с заказом. \n" +  (isEnd ? PlayerPrefs.GetString("End") : "");
            PlayerPrefs.SetString("selectedOrder", "false");
            endOfLevel.Invoke();
        }
    }
}