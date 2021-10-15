using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GroundSpawner : MonoBehaviour
{
    public GameObject [] groundTiles;
    public GameObject [] skyVolumes;
    public GameObject lightSource;


    private Vector3 nextSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        setFog();
        for (int i = 0; i < 10; i++)
        {
            SpawnTile();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnTile()
    {
        GameObject temp = Instantiate(groundTiles[Mathf.FloorToInt(Random.Range(0, groundTiles.Length))], nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(0).transform.position;
    }

    public void ControlPopulation()
    {
        int tileAmount = FindObjectsOfType<GroundTile>().Length;
        List<GroundTile> tileList = FindObjectsOfType<GroundTile>().ToList();

        if (tileAmount >= 13)
        {
            foreach (Transform child in tileList[tileList.Count - 1].gameObject.transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(tileList[tileList.Count - 1].gameObject);
        }
    }

    private void setFog()
    {
        if(FindObjectOfType<DiffLevel>().getFogLevel() == 0)
        {
            foreach(GameObject z in groundTiles)
            {
                z.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                z.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                z.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            }
        }
        else if(FindObjectOfType<DiffLevel>().getFogLevel() == 1)
        {
            foreach (GameObject z in groundTiles)
            {
                z.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                z.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                z.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
            }
        }
        else if (FindObjectOfType<DiffLevel>().getFogLevel() == 2)
        {
            foreach (GameObject z in groundTiles)
            {
                z.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                z.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                z.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            }
        }
        else if (FindObjectOfType<DiffLevel>().getFogLevel() == 3)
        {
            foreach (GameObject z in groundTiles)
            {
                z.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                z.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                z.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            }
        }
    }
}
