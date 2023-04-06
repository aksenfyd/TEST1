using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPbar : MonoBehaviour
{
    private Slider EXPBar;
    public GameObject Target;
    private void Awake()
    {
        EXPBar = GetComponent<Slider>();
    }

    private void Start()
    {
        Target = GameObject.Find("Player");

        EXPBar.maxValue = Target.GetComponent<PlayerController>().REQUIREEXP;
        EXPBar.value = 0;
    }
    private void Update()
    {
        //if(Input.GetMouseButton(0))
        //{
        //    Target.GetComponent<PlayerController>().HP -= 1;
        //}
        //if(Input.GetMouseButton(1))
        //{
        //    Target.GetComponent<PlayerController>().HP += 1;
        //}

        EXPBar.value = Target.GetComponent<PlayerController>().EXP;

        if (Target.GetComponent<PlayerController>().EXP >= Target.GetComponent<PlayerController>().REQUIREEXP)
        {
            Target.GetComponent<PlayerController>().EXP -= Target.GetComponent<PlayerController>().REQUIREEXP;
            Target.GetComponent<PlayerController>().LEVEL += 1;
            Target.GetComponent<PlayerController>().LEVELPOINT += 1;
            Target.GetComponent<PlayerController>().REQUIREEXP *= 2;
        }

        GameObject.Find("LEVEL").GetComponent<Text>().text = "LEVEL"+Target.GetComponent<PlayerController>().LEVEL.ToString();

    }


}
