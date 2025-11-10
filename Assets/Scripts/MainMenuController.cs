using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Definicja możliwych stanów menu
    public enum MenuState
    {
        MainMenu,
        Settings
    }

    [Header("Panel References")]
    [SerializeField]
    private GameObject _settingsMenu, _mainMenu;

    [Header("First Button References")]
    [SerializeField]
    private GameObject _firstMenuButton, _firstSettingsButton;

    private MenuState _currentState;

    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.UI.Enable();
    }

    private void OnDestroy()
    {
        _playerControls.UI.Disable();
    }

    private void Start()
    {
        _playerControls.UI.Escape.performed += context => TryToGoBackToMainMenu();
        ChangeState(MenuState.MainMenu);
    }

    public void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            switch (_currentState)
            {
                case MenuState.MainMenu:
                    EventSystem.current.SetSelectedGameObject(_firstMenuButton);
                    break;

                case MenuState.Settings:
                    EventSystem.current.SetSelectedGameObject(_firstSettingsButton);
                    break;
            }
        }
    }

    private void TryToGoBackToMainMenu()
    {
        if (_currentState == MenuState.Settings)
        {
            CloseSettings();
            return;
        }
    }

    private void ChangeState(MenuState newState)
    {
        _currentState = newState;

        switch (_currentState)
        {
            case MenuState.MainMenu:
                _mainMenu.SetActive(true);
                _settingsMenu.SetActive(false);
                EventSystem.current.SetSelectedGameObject(_firstMenuButton);
                break;

            case MenuState.Settings:
                _mainMenu.SetActive(false);
                _settingsMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(_firstSettingsButton);
                break;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        ChangeState(MenuState.Settings);
    }

    public void CloseSettings()
    {
        ChangeState(MenuState.MainMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}