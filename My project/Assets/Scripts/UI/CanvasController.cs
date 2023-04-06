using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject StatsCanvas;
    public GameObject LevelCanvas;
    public bool StatsCanvasActive;
    public bool LevelCanvasActive;

    private void Awake()
    {
        StatsCanvas = GameObject.Find("StatsCanvas");
        LevelCanvas = GameObject.Find("LevelCanvas");
    }
    void Start()
    {
        StatsCanvasActive = false;
        LevelCanvasActive = false;
        StatsCanvas.SetActive(StatsCanvasActive);
        LevelCanvas.SetActive(LevelCanvasActive);
    }

    public void onSkillCanvasActive()
    {
        StatsCanvasActive = !StatsCanvasActive;
        StatsCanvas.SetActive(StatsCanvasActive);
    }
    public void onLevelCanvasActive()
    {
        LevelCanvasActive = !LevelCanvasActive;
        LevelCanvas.SetActive(LevelCanvasActive);
    }

    void Update()
    {
    
    }
}
