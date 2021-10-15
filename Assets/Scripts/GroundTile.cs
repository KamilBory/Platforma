using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;

    // Background objects data arrays

    public int levelDifficulty;


    // Start is called before the first frame update
    void Start()
    {
        levelDifficulty = FindObjectOfType<DiffLevel>().getDiffLevel();
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile();
        groundSpawner.ControlPopulation();
    }
}
