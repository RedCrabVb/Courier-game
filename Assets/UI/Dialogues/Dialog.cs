using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Dialog : MonoBehaviour
{
    public string[] select = {"Надеть маску?", "Надеть перчатки?", "Использовать антисептик?", "Мыть руки?"};
    private string[] end = { "Вы полностью соблюдали противоковидные меры, и не подвергали жизни других людей опасности!",
        "Вы подвергли жизни других людей опасности не соблюдая противоковидные меры!",
        "Вы подвергли жизни других людей опасности, не нося маску, нужно думать не только о себе, но и об окружающих!", 
        "Вы относитесь к тем людям, которые либо отрицают ковид, либо не считают его реальной угрозой, вы являетесь ковид-диссидентом."};
    private int index_select = 0;
    public Text text_for_select;
    public UnityEvent start_event;

    private int karma = 0;
    private bool mask = true;
    private enum end_ptions : int
    {
        good = 0, average = 1, unsatisfactory = 2, bad = 3
    }
    public void Start()
    {
        text_for_select.text = select[index_select];
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void choice_made(bool choice)
    {
        karma += choice ? 1 : 0;
        mask = select[index_select] == "Надеть маску?" ? choice : mask;
        PlayerPrefs.SetString("mask", mask.ToString());
        if (index_select + 1 == select.Length)
        {
            PlayerPrefs.SetString("End", election_results());
            PlayerPrefs.SetInt("karma", karma);
            PlayerPrefs.SetInt("select.Length", select.Length);
            gameObject.SetActive(false);
            Debug.Log(PlayerPrefs.GetString("End"));
            start_event.Invoke();
        }
        else
        {
            index_select++;
            text_for_select.text = select[index_select];
        }
    }
    public string election_results()
    {
        int allKarm = karma + PlayerPrefs.GetInt("karma");
        int allSelect = select.Length + PlayerPrefs.GetInt("select.Length");
        return allKarm == allSelect ? end[(int)end_ptions.good] :
               allKarm > 0 && Convert.ToBoolean(PlayerPrefs.GetString("mask")) ? end[(int)end_ptions.average] :
               allKarm > 0 && !Convert.ToBoolean(PlayerPrefs.GetString("mask")) ? end[(int)end_ptions.unsatisfactory] : 
               end[(int)end_ptions.bad];
    }
}
