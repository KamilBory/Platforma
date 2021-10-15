using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Dropdown levelList;
    public Dropdown diffLevel;
    public Dropdown fogLevel;
    public Dropdown steering;
    public Dropdown cameraLevel;

    // Start is called before the first frame update
    void Start()
    {
        setDifficultyLevel();
        setFogLevel();
        setSteerLevel();
        setCameraLevel();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        FindObjectOfType<DiffLevel>().setDiffLevel(diffLevel.value);
        FindObjectOfType<DiffLevel>().setFogLevel(fogLevel.value);
        FindObjectOfType<DiffLevel>().setSteerLevel(steering.value);
        FindObjectOfType<DiffLevel>().setCameraLevel(cameraLevel.value);
        SceneManager.LoadScene(levelList.value + 1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void setDifficultyLevel()
    {
        diffLevel.SetValueWithoutNotify(FindObjectOfType<DiffLevel>().getDiffLevel());
    }

    private void setFogLevel()
    {
        fogLevel.SetValueWithoutNotify(FindObjectOfType<DiffLevel>().getFogLevel());
    }

    private void setSteerLevel()
    {
        steering.SetValueWithoutNotify(FindObjectOfType<DiffLevel>().getSteerLevel());
    }

    private void setCameraLevel()
    {
        cameraLevel.SetValueWithoutNotify(FindObjectOfType<DiffLevel>().getCameraLevel());
    }
}
