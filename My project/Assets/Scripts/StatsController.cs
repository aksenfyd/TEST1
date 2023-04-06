using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    private GameObject StatsCanvas;
    private GameObject statsbuttons;
    private GameObject Stats;
    private GameObject Player;

    void Start()
    {
        StatsCanvas = GameObject.Find("StatsCanvas");
        statsbuttons = GameObject.Find("statsbuttons");
        Stats = GameObject.Find("Stats");
        Player = GameObject.Find("Player");
        statsbuttons.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        Stats.transform.localPosition = new Vector3(-361.0f, -398.0f, 0.0f);

       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ATKUP()
    {
        if (ControllerManager.GetInstance().Gold >= (ControllerManager.GetInstance().ATKLV + 1) * 100)
        {
            ControllerManager.GetInstance().Gold-= (ControllerManager.GetInstance().ATKLV + 1) *100;
            ControllerManager.GetInstance().ATKLV += 1;
        }
    }
    public void CRIUP()
    {
        if (ControllerManager.GetInstance().Gold >= (ControllerManager.GetInstance().CRILV + 1) * 100)
        {
            ControllerManager.GetInstance().Gold -= (ControllerManager.GetInstance().CRILV + 1) * 100;
            ControllerManager.GetInstance().CRILV += 1;
        }
    }
    public void HPUP()
    {
        if (ControllerManager.GetInstance().Gold >= (ControllerManager.GetInstance().HPLV + 1) * 100)
        {
            ControllerManager.GetInstance().Gold -= (ControllerManager.GetInstance().HPLV + 1) * 100;
            ControllerManager.GetInstance().HPLV += 1;
            Player.GetComponent<PlayerController>().HP += 30;
            Player.GetComponent<PlayerController>().MAXHP += 30;
        }
    }
    public void CRIDMGUP()
    {
        if (ControllerManager.GetInstance().Gold >= (ControllerManager.GetInstance().CRIDMGLV + 1) * 100)
        {
            ControllerManager.GetInstance().Gold -= (ControllerManager.GetInstance().CRIDMGLV + 1) * 100;
            ControllerManager.GetInstance().CRIDMGLV += 1;
        }
    }
    public void HPREGENUP()
    {
        if (ControllerManager.GetInstance().Gold >= (ControllerManager.GetInstance().HPREGENLV+1) * 100)
        {
            ControllerManager.GetInstance().Gold -= (ControllerManager.GetInstance().HPREGENLV + 1) * 100;
            ControllerManager.GetInstance().HPREGENLV += 1;
        }
    }
    public void GOLDUP()
    {
        if (ControllerManager.GetInstance().Gold >= (ControllerManager.GetInstance().GOLDLV+1) * 100)
        {
            ControllerManager.GetInstance().Gold -= (ControllerManager.GetInstance().GOLDLV + 1) * 100;
            ControllerManager.GetInstance().GOLDLV += 1;
        }
    }

    public void StatsCanvasOff()
    {
        StatsCanvas.SetActive(false);
    }
}
