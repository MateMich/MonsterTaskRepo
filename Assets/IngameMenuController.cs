using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class IngameMenuController : MonoBehaviour
{
    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.UI.Enable();
    }

    private void Start()
    {
        _playerControls.UI.Escape.performed += context => GoToMainMenu();
    }

    private void GoToMainMenu()
    {
        Debug.Log("Escape pressed, loading Main Menu...");
        _playerControls.UI.Disable();

        SceneManager.LoadScene(0);
    }
}