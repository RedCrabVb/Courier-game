using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool ispuse;
    public GameObject pause_panel;
    void Update()
    {
        Time.timeScale = (ispuse == true ? 0 : 1f);
        ispuse = (Input.GetKeyDown(KeyCode.Escape) ? !ispuse : ispuse);

        pause_panel.SetActive(ispuse);
        Cursor.visible = ispuse;
        Cursor.lockState = (ispuse ? CursorLockMode.None : CursorLockMode.Locked);
    }
}

