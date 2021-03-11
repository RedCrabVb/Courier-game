using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class Menu : MonoBehaviour
    {
        private static string menu_scen_name = "GameMenu";
        public void restart_game()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
        public void reset_data()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("mask", false.ToString());
            PlayerPrefs.SetInt("karma", 0);
            PlayerPrefs.SetInt("select.Length", 0);
            PlayerPrefs.SetString("postProces", true.ToString());
        }
        public void go_menu()
        {
            SceneManager.LoadScene(menu_scen_name, LoadSceneMode.Single);
        }
        public void load_level()
        {
            if (PlayerPrefs.GetString("NotPassedLevel") != "")
                SceneManager.LoadScene(PlayerPrefs.GetString("NotPassedLevel"), LoadSceneMode.Single);
            else
                SceneManager.LoadScene("Training", LoadSceneMode.Single);
        }

        public void load_level(string name)
        {
            SceneManager.LoadScene(name, LoadSceneMode.Single);
        }

        public void exit_game()
        {
            Application.Quit();
        }
    }
}