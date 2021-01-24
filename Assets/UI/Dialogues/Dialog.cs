using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Dialog : MonoBehaviour
{
    private string[] select = {"Надеть маску?", "Надеть перчатки?", "Использовать антисептик?", "Мыть руки?"};
    private string[] end = { "Молодец!", "Будь ответственным!", "Нужно думать не только о себе, но и об окружающих.", "Это поведение представляет угрозу."};
    private int index_select = 0;
    public Text text_for_select;
    public UnityEvent start_event;

    private int karma = 0;
    private bool mask = false;
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
        if (index_select + 1 == select.Length)
        {
            PlayerPrefs.SetString("End", election_results());
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
        return karma == select.Length ? end[(int)end_ptions.good] :
               karma > 0 && mask ? end[(int)end_ptions.average] :
               karma > 0 && !mask ? end[(int)end_ptions.unsatisfactory] : 
               end[(int)end_ptions.bad];
    }
}
