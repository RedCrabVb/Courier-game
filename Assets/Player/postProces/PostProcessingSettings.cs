using System;
using System.Collections;
using Game.Player;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;
using Game.Player;
public class PostProcessingSettings : MonoBehaviour
{
    private static bool postProcs = false;

    private void changeReal()
    {
        try
        {
            if (gameObject.GetComponent<PostProcessingBehaviour>() != null)
            {
                gameObject.GetComponent<PostProcessingBehaviour>().enabled = postProcs;
            }
        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }
    }
    
    public void postProcesChange()
    {
        postProcs = gameObject.GetComponent<Toggle>().isOn;
        PlayerPrefs.SetString("postProces", postProcs.ToString());
    }
    
    private void Awake()
    {
        try
        {
            postProcs = Convert.ToBoolean(PlayerPrefs.GetString("postProces"));
        }
        catch {
            PlayerPrefs.SetString("postProces", postProcs.ToString());
        }

        /*erro in method*/
        if (gameObject.GetComponent<PostProcessingBehaviour>() != null)
        {
            gameObject.GetComponent<PostProcessingBehaviour>().enabled = postProcs;
        }
        else
        {
            gameObject.GetComponent<Toggle>().isOn = postProcs;
        }

        MouseLook.cursor_enable();
        StartCoroutine("changThis");
    }

    IEnumerator changThis() {
        while (true)
        {
            changeReal();
            yield return new WaitForSeconds(2f);
        }
    }
}
