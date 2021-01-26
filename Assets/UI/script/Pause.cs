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
            ispuse = !ispuse;
            Time.timeScale = (ispuse == true ? (float)SpeedTime.Stop_time : (float)SpeedTime.Start_time);

            pause_panel.SetActive(ispuse);
            if (ispuse)
                MouseLook.cursor_enable();
            else
                MouseLook.cursor_disabled();
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pause();
            }
        }
    }

}