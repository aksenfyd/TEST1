using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public PlayerController Target;

    private List<GameObject> Images = new List<GameObject>();
    private List<GameObject> Buttons = new List<GameObject>();
    private List<Image> ButtonImages = new List<Image>();
    private float cooldown;

    private int push2;
    private void Start()
    {
        Target = GameObject.Find("Player").GetComponent<PlayerController>();

        int push = push2;
        GameObject SkillsObj = GameObject.Find("Skills");

        for (int i = 0; i < SkillsObj.transform.childCount; ++i)
            Images.Add(SkillsObj.transform.GetChild(i).gameObject);

        for (int i = 0; i < Images.Count; ++i)
            Buttons.Add(Images[i].transform.GetChild(push).gameObject);

        for (int i = 0; i < Buttons.Count; ++i)
            ButtonImages.Add(Buttons[i].GetComponent<Image>());
        
        cooldown = 0.0f;
    }


    public void PushButton()
    {
        int push = push2;

        ButtonImages[push].fillAmount = 0;
        Buttons[push].GetComponent<Button>().enabled = false;

        StartCoroutine(PushButton_Coroutine());
    }

    IEnumerator PushButton_Coroutine()
    {
        float cool = cooldown;
        int push = push2;

        while (ButtonImages[push].fillAmount != 1)
        {
            ButtonImages[push].fillAmount += Time.deltaTime * cool;
            yield return null;
        }

        Buttons[push].GetComponent<Button>().enabled = true;
    }

    public void Testcase1()
    {
        push2 = 0;
        cooldown = 0.1f * Target.COOL;


        ControllerManager.GetInstance().AttackSpeed = 0.3f;
        StartCoroutine(RapidShot(5));

    }

    public void Testcase2()
    {
        push2 = 1;
        cooldown = 0.1f * Target.COOL;


        ControllerManager.GetInstance().BulletScale = 4.0f;
        StartCoroutine(BigShot(5));

    }

    public void Testcase3()
    {
        push2 = 2;
        cooldown = 0.05f * Target.COOL;


        ControllerManager.GetInstance().MoveSpeed = 2.0f;
        StartCoroutine(SpeedUp(5));
    }

    public void Testcase4()
    {
        push2 = 3;
        cooldown = 0.02f * Target.COOL;

        //GameObject.Find("barrier").gameObject.SetActive(false);
        Target.BarrierOn();
        StartCoroutine(Barrier(15));
    }

    public void Testcase5()
    {
        push2 = 4;
        cooldown = 0.05f * Target.COOL;

        
        Target.HP += (int)(Target.HP * 0.2f);


        if (Target.HP > Target.MAXHP)
            Target.HP = Target.MAXHP;
      
    }

    IEnumerator RapidShot(int _time)
    {
        yield return new WaitForSeconds(_time);
        ControllerManager.GetInstance().AttackSpeed = 1.0f;
    }
    IEnumerator BigShot(int _time)
    {
        yield return new WaitForSeconds(_time);
        ControllerManager.GetInstance().BulletScale = 1.0f;
    }
    IEnumerator SpeedUp(int _time)
    {
        yield return new WaitForSeconds(_time);
        ControllerManager.GetInstance().MoveSpeed = 1.0f;
    }
    IEnumerator Barrier(int _time)
    {
        yield return new WaitForSeconds(_time);
        Target.BarrierOff();

    }

}