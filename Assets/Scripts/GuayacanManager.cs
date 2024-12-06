using UnityEngine;
using UnityEngine.SceneManagement;

public class GuayacanManager : MonoBehaviour
{
    private int points;
    private int dartsThrown;

    public static GuayacanManager Instance;

    public int Points { get => points; set => points = value; }
    public int DartsThrown { get => dartsThrown; set => dartsThrown = value; }

    private void Awake()
    {
        points = 0;
        dartsThrown = 0;
        Instance = this;
    }

    private void Update()
    {
        
    }
}
