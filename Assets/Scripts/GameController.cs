using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject minimap;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;
    [SerializeField] private GameObject dialogueWindow;
    [SerializeField] private TMP_Text bunuelosCounter;
    private int bunuelosTaken;

    private bool paused = false;

    public bool Paused { get => paused; set => paused = value; }

    private void Awake()
    {
        Instance = this;
        UnpauseGame();
        bunuelosTaken = 0;
        bunuelosCounter.text = bunuelosTaken.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !map.activeSelf && !winMenu.activeSelf && !dialogueWindow.activeSelf && !loseMenu.activeSelf)
        {
            HandlePause();
        }
        if (Input.GetKeyUp(KeyCode.M) && !pauseMenu.activeSelf && !winMenu.activeSelf && !dialogueWindow.activeSelf && !loseMenu.activeSelf)
        {
            ShowMap();
            minimap?.SetActive(false);
        }
        if (!map.activeSelf)
        {
            minimap?.SetActive(true);

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

    //Hola, esto debería crear un conflicto
    //Haciendo pruebas
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
    }

    private void ShowMap()
    {
        map.SetActive(!map.activeSelf);
        if (map.activeSelf)
        {
            Paused = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        else
        {
            Paused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
    }

    public void ChangeScene(string name)
    {
        DontDestroyOnLoad(this);
        string prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(name);
        if (prevScene.Equals("Guayacan"))
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                Debug.Log("Ha encontrado player");
            }
        }
    }

    public void TookBunuelo()
    {
        bunuelosTaken++;
        bunuelosCounter.text = bunuelosTaken.ToString("00");
    }

}
