using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
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
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<RigidbodyFirstPersonController>().mouseLook.cursor_enable();
            if (end_text != null && isEnd)
                end_text.text = "Вы справились с заказом. \n" + PlayerPrefs.GetString("End");
            endOfLevel.Invoke();
        }
    }
}
