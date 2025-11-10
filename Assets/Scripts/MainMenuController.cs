using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject _settingsMenu, _mainMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        _mainMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit(); //ignored in editor
    }
}