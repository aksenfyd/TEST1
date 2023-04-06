using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPbar : MonoBehaviour
{  
    
    private void Awake()
    {
        HPBar = GetComponent<Slider>();
    }
    
    // ** 따라다닐 객체
    public GameObject Target;

    // ** 세부위치 조정
    private Vector3 offset;

    private Image image;
    private Slider HPBar;
    private int MAXHP;

    private EnemyController Geti;
    void Start()
    {
        image = GetComponent<Image>();
        image.fillAmount = 1;

    // ** 위치 셋팅
    offset = new Vector3(0.5f, 0.6f, 0.0f);

        /*
        Geti = Target.GetComponent<EnemyController>();
        HP = Geti.HP;
        HPBar.maxValue = HP;
        HPBar.value = HPBar.maxValue;

         */
        if (Target.GetComponent<BossController>() != null)
            MAXHP = Target.GetComponent<BossController>().HP;
        else if (Target.GetComponent<EnemyController>() != null)
            MAXHP = EnemyManager.GetInstance.HP;
        
    }


    void Update()
    {
        // ** WorlToScreenPoint = 월드 좌표를 카메라 좌표로 변환
        // ** 월드 상에 있는 타겟의 좌표를 카메라 좌표로 변환하여 UI에 셋팅한다



        if (Target != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(Target.transform.position + offset);

            if (Target.GetComponent<EnemyController>() != null)
                image.fillAmount = Target.GetComponent<EnemyController>().HP * (1.0f / MAXHP);
            else if (Target.GetComponent<BossController>() != null)
            {
                image.fillAmount = Target.GetComponent<BossController>().HP * (1.0f / MAXHP);
                
            }
        }
        if (Target == null)
            Destroy(transform.gameObject);

    }


    public void Hit(int _damage)
    {
        
    }
}
