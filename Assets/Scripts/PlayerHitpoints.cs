using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHitpoints : MonoBehaviour
{
    public Text lifeDisplay;
    public GameObject player;
    public Text LoseText;
    public Text CountDown;

    private int lifes = 5;
    private float countMax = 5f;

    // Start is called before the first frame update
    void Start()
    {
        countMax = 5;
        lifes = 5;
        LoseText.gameObject.SetActive(false);
        CountDown.gameObject.SetActive(false);
        if(FindObjectOfType<DiffLevel>().getDiffLevel() == 0)
        {
            lifeDisplay.gameObject.SetActive(false);
        }
        else
        {
            lifeDisplay.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }

    private void LoseHP()
    {
        lifes--;
        DisplayLifes();
    }

    private void DisplayLifes()
    {
        lifeDisplay.text = lifes.ToString();
    }

    private void GameOver()
    {
        if(lifes <= 0)
        {
            LoseText.gameObject.SetActive(true);
            CountDown.gameObject.SetActive(true);

            int tmp = (int)countMax;
            CountDown.text = tmp.ToString();
            if (countMax <= 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                countMax -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        {
            LoseHP();
        }
    }
}
