using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private GameObject LevelCanvas;
    private GameObject levelbuttons;
    private GameObject Stats;
    private GameObject Player;

    void Start()
    {
        LevelCanvas = GameObject.Find("LevelCanvas");
        levelbuttons = GameObject.Find("levelbuttons");
        Stats = GameObject.Find("LevelStats");
        Player = GameObject.Find("Player");
        levelbuttons.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        Stats.transform.localPosition = new Vector3(-361.0f, -398.0f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IDAMAGEUP()
    {
        if (Player.GetComponent<PlayerController>().LEVELPOINT>= 1)
        {
            Player.GetComponent<PlayerController>().LEVELPOINT -= 1;
            Player.GetComponent<PlayerController>().IDAMAGE += 0.1f;
            
        }
    }
    public void COOLUP()
    {
        if (Player.GetComponent<PlayerController>().LEVELPOINT >= 1)
        {
            Player.GetComponent<PlayerController>().LEVELPOINT -= 1;
            Player.GetComponent<PlayerController>().COOL += 0.15f;
        }
    }
    public void LDAMAGEUP()
    {
        if (Player.GetComponent<PlayerController>().LEVELPOINT >= 1)
        {
            Player.GetComponent<PlayerController>().LEVELPOINT -= 1;
            Player.GetComponent<PlayerController>().LDAMAGE += 1;
        }
    }
    public void IEXPUP()
    {
        if (Player.GetComponent<PlayerController>().LEVELPOINT >= 1)
        {
            Player.GetComponent<PlayerController>().LEVELPOINT -= 1;
            Player.GetComponent<PlayerController>().IEXP += 1;
        }
    }
    public void THROWUP()
    {
        if (Player.GetComponent<PlayerController>().LEVELPOINT >= 1)
        {
            Player.GetComponent<PlayerController>().LEVELPOINT -= 1;
            Player.GetComponent<PlayerController>().THROW += 1;
        }
    }
    public void ATKSPEEDUP()
    {
        if (Player.GetComponent<PlayerController>().LEVELPOINT >= 1)
        {
            Player.GetComponent<PlayerController>().LEVELPOINT -= 1;
            Player.GetComponent<PlayerController>().ATKSPEED += 0.1f;
        }
    }

    public void StatsCanvasOff()
    {
        LevelCanvas.SetActive(false);
    }
}
