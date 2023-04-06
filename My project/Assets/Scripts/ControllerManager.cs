using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager
{
    private ControllerManager() { }
    private static ControllerManager Instance = null;

    public static ControllerManager GetInstance()
    {
        if (Instance == null)
            Instance = new ControllerManager();
        return Instance;
    }

   
    public bool DirLeft;
    public bool DirRight;

    public float BulletScale = 1.0f;
    public float AttackSpeed = 1.0f;
    public float MoveSpeed = 1.0f;
    public float distance = 0.0f;

    
    public int Gold = 10000;

    public int ATKLV = 0;
    public int HPLV = 0;
    public int HPREGENLV = 0;
    public int CRILV = 0;
    public int CRIDMGLV = 0;
    public int GOLDLV = 0;

}
