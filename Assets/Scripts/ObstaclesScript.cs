using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnObstacles();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnObstacles()
    {
        int checker = 0;
        if (FindObjectOfType<DiffLevel>().getDiffLevel() == 0)
        {
            checker = 0;
        }
        else if (FindObjectOfType<DiffLevel>().getDiffLevel() == 1)
        {
            checker = 3;
        }
        else if (FindObjectOfType<DiffLevel>().getDiffLevel() == 2)
        {
            checker = 6;
        }
        else if (FindObjectOfType<DiffLevel>().getDiffLevel() == 3)
        {
            checker = 9;
        }

        bool obs1 = false;
        bool obs2 = false;

        for (int i = 0; i < gameObject.transform.GetChild(3).childCount; i++)
        {
            if(i % 3 == 0)
            {
                obs1 = false;
                obs2 = false;
            }

            int chance = Random.Range(1, 12);
            if (chance < checker && (obs1 == false || obs2 == false))
            {
                gameObject.transform.GetChild(3).GetChild(i).gameObject.SetActive(true);
                if(i % 3 == 0)
                {
                    obs1 = true;
                }
                else if (i % 3 == 1)
                {
                    obs2 = true;
                }
            }
            else
            {
                gameObject.transform.GetChild(3).GetChild(i).gameObject.SetActive(false);
            }
        }
    }


}
