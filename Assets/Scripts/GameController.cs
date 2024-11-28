using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject map;
    private bool paused = false;

    public bool Paused { get => paused; set => paused = value; }

    private void Awake()
    {
        Instance = this;
        UnpauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !map.activeSelf)
        {
            HandlePause();
        }
        if (Input.GetKeyUp(KeyCode.M) && !pauseMenu.activeSelf)
        {
            ShowMap();
        }
    }

    private void HandlePause()
    {
        if (pauseMenu.activeSelf)
        {
            UnpauseGame();
        }
        else PauseGame();
    }

    private void PauseGame()
    {
        Paused = true;
        pauseMenu?.SetActive(Paused);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        Paused = false;
        pauseMenu?.SetActive(Paused);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(("MainMenu"));
        //TODO Actually create the main menu
    }

    private void ShowMap()
    {
        map.SetActive(!map.activeSelf);
    }

}
