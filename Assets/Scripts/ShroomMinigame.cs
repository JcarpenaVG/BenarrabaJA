using System.Collections.Generic;
using UnityEngine;

public class ShroomMinigame : MonoBehaviour
{
    //Both of these serialize should be prefabs with as many children as we need
    [SerializeField] private GameObject shroomsParent;
    private List<GameObject> shrooms;
    [SerializeField] private GameObject positionsParent;
    private List<GameObject> positions;

    [SerializeField] private float secondsTime = 60f; //Time limit for the minigame

    private void Awake()
    {
        FillList(shroomsParent, shrooms);
        FillList(positionsParent, positions);
        Shuffle(shrooms);
        Shuffle(positions);
        InstantiateAll();
    }

    private void Update()
    {
        secondsTime -= Time.deltaTime;
        if (secondsTime < 0.2f)
        {
            TimeUp();
        }
    }

    private void FillList(GameObject parent, List<GameObject> list)
    {
        foreach (Transform thing in parent.transform)
        {
            list.Add(thing.gameObject);
        }
    }

    private void Shuffle(List<GameObject> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        for (int i = n - 1; i > 1; i--)
        {
            int rnd = random.Next(i + 1);

            GameObject value = list[rnd];
            list[rnd] = list[i];
            list[i] = value;
        }
    }

    private void InstantiateAll()
    {
        for (int i = 0; i < shrooms.Count; i++)
        {
            Instantiate(shrooms[i], positions[i].transform);
        }
    }

    private void TimeUp()
    {
        Debug.Log("Time up!");
        //TODO What should we do?
    }
}
