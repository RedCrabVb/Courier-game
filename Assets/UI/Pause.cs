using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private float timer;
    private bool ispuse, guipuse;
    public GameObject pause_panel;
    void Update()
    {
        Time.timeScale = timer;
        ispuse = (Input.GetKeyDown(KeyCode.Escape) ? !ispuse : ispuse);

        pause_panel.SetActive(ispuse);
        Cursor.visible = ispuse;
        Cursor.lockState = (ispuse ? CursorLockMode.None : CursorLockMode.Locked);
        timer = (ispuse == true ? 0 : 1f);
    }
}

