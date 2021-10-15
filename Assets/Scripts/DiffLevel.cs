using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffLevel : MonoBehaviour
{
    public static int diffLevel;
    public static int fogLevel;
    public static int lightLevel;
    public static int steerLevel;
    public static int cameraLevel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setDiffLevel(int level)
    {
        diffLevel = level;
    }

    public int getDiffLevel()
    {
        return diffLevel;
    }

    public void setFogLevel(int level)
    {
        fogLevel = level;
    }

    public int getFogLevel()
    {
        return fogLevel;
    }

    public void setLightLevel(int level)
    {
        lightLevel = level;
    }

    public int getLightLevel()
    {
        return lightLevel;
    }

    public void setSteerLevel(int st)
    {
        steerLevel = st;
    }

    public int getSteerLevel()
    {
        return steerLevel;
    }

    public void setCameraLevel(int st)
    {
        cameraLevel = st;
    }

    public int getCameraLevel()
    {
        return cameraLevel;
    }
}
