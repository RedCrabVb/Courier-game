using UnityEngine;
using Game.Player;

namespace Game.UI
{
    public class Pause : MonoBehaviour
    {
        private bool ispuse = false;
        public GameObject pause_panel;
        private enum SpeedTime
        {
            Stop_time = 0,
            Start_time = 1
        }
        private void Start()
        {
            ispuse = false;
            Time.timeScale = (float)SpeedTime.Start_time;
        }
        private void pause()
        {
            Time.timeScale = (float)SpeedTime.Stop_time;

            pause_panel.SetActive(true);
            MouseLook.cursor_enable();
        }

        private void start()
        {
            Time.timeScale = (float)SpeedTime.Start_time;

            pause_panel.SetActive(false);
            MouseLook.cursor_disabled();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ispuse = !ispuse;
                if (ispuse)
                    pause();
                else
                    start();
            }
        }
    }

}