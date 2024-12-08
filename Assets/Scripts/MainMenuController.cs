using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;

    public void StartGame()
    {
        Debug.Log("Deberia empezar el juego");
        SceneManager.LoadScene("OutdoorsScene");
    }

    public void GoToCreditsPanel()
    {
        creditsPanel.SetActive(true);
    }

    public void GoBack()
    {
        creditsPanel.SetActive(false);
    }

    //public void GoToOptions()
    //{
    //    Debug.Log("Boton de opciones pulsado correctamente");
    //    //TODO
    //}

    public void ExitGame()
    {
        Debug.Log("Deberia salir del juego");
        Application.Quit();
    }
}
