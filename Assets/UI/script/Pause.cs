using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class Pause : MonoBehaviour
{
    private bool ispuse = false;
    public GameObject pause_panel;
    public RigidbodyFirstPersonController rigidbodyFirstPersonController;
    private enum SpeedTime
    {
        Stop_time = 0,
        Start_time = 1
    }
    private void Start()
    {
        ispuse = false;
        Time.timeScale = (float)SpeedTime.Start_time;
        //rigidbodyFirstPersonController.mouseLook.cursor_disabled();
    }
    private void pause()
    {
        ispuse = !ispuse;
        Time.timeScale = (ispuse == true ? (float)SpeedTime.Stop_time : (float)SpeedTime.Start_time);

        pause_panel.SetActive(ispuse);
        if (ispuse)
            rigidbodyFirstPersonController.mouseLook.cursor_enable();
        else
            rigidbodyFirstPersonController.mouseLook.cursor_disabled();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }
    }
}

