using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour
{
    public UnityEvent endOfLevel;
    private void Start()
    {
        PlayerPrefs.SetString("NotPassedLevel", SceneManager.GetActiveScene().name);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            endOfLevel.Invoke();
        }
    }
}
