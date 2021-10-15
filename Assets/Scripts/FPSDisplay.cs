using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{

    public Text FPS;
    public Toggle Display;

    private float updateTime = 0.7f;
    private float timecount = 0.7f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float fps = Mathf.Floor(1 / Time.unscaledDeltaTime);

        if(timecount >= updateTime)
        {
            FPS.text = "" + fps;
            timecount = 0f;
        }

        if (Display.isOn)
        {
            timecount += Time.unscaledDeltaTime;
            FPS.gameObject.SetActive(true);
        }
        else
        {
            FPS.gameObject.SetActive(false);
        }
    }
}