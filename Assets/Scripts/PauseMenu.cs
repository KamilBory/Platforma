using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public Dropdown diffLevel;

    private bool options = false;

    // Start is called before the first frame update
    void Start()
    {
        diffLevel.SetValueWithoutNotify(FindObjectOfType<DiffLevel>().getDiffLevel());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused && !options)
            {
                Resume();
            }
            else if (gameIsPaused && options)
            {
                Return();
            }
            else
            {
                Pause();
            }
        }

        FindObjectOfType<DiffLevel>().setDiffLevel(diffLevel.value);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Options()
    {
        optionsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        options = true;
    }

    public void Return()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        options = false;
    }
}
