using UnityEngine;
using UnityEngine.SceneManagement;

public class GuayacanManager : MonoBehaviour
{
    private int points;
    private int dartsThrown;
    [SerializeField] private GameObject canvasPutVR;

    public static GuayacanManager Instance;

    public int Points { get => points; set => points = value; }
    public int DartsThrown { get => dartsThrown; set => dartsThrown = value; }

    private void Awake()
    {
        points = 0;
        dartsThrown = 0;
        Instance = this;
        canvasPutVR.SetActive(true);
    }

    private void Update()
    {
        if (canvasPutVR.activeSelf)
        {
            if (Input.anyKey)
            {
                canvasPutVR.SetActive(false);
                if (!Input.GetKeyUp(KeyCode.Space)) //TODO change to the appropriate key in VR controller
                {
                    SceneManager.LoadScene("OutdoorsScene"); //TODO put the player just outside the place
                }
            }
        }
    }
}
