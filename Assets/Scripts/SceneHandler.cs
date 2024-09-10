    using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private GameObject loadedMenu;

    public void LoadMenu(GameObject menu)
    {
        if(loadedMenu != null)
        {
            loadedMenu.SetActive(false);
            loadedMenu = menu;
            loadedMenu.SetActive(true);
        }
        else
        {
            Debug.LogWarning("loadedMenu is null");
        }
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Exit()
    {
        Application.Quit(); 
    }
}
