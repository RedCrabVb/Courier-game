using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private string level1_scen_name = "Map_1", menu_scen_name = "GameMenu";
    public void restart_game()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    public void go_menu()
    {
        SceneManager.LoadScene(menu_scen_name, LoadSceneMode.Single);
    }
    public void start_game()
    {
        SceneManager.LoadScene(level1_scen_name, LoadSceneMode.Single);
    }

    public void exit_game()
    {
        Application.Quit();
    }
}
