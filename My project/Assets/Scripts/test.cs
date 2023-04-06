using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    private Image image;
    private int MAXHP;

    public GameObject Target;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        image.fillAmount = 1;
        //MAXHP = Target.GetComponent<PlayerController>().HP;
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    int damage = Random.Range(50, 150);
        //    image.fillAmount -= (damage * 100 / HP) * 0.01f;
        //}
        //if(Input.GetKey(KeyCode.RightArrow))
        //{
        //    image.fillAmount = 1;
        //}

        image.fillAmount = Target.GetComponent<PlayerController>().HP * (1.0f / Target.GetComponent<PlayerController>().MAXHP);
    }
}
