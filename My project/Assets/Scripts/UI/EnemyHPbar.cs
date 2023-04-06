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
    
    // ** ����ٴ� ��ü
    public GameObject Target;

    // ** ������ġ ����
    private Vector3 offset;

    private Image image;
    private Slider HPBar;
    private int MAXHP;

    private EnemyController Geti;
    void Start()
    {
        image = GetComponent<Image>();
        image.fillAmount = 1;

    // ** ��ġ ����
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
        // ** WorlToScreenPoint = ���� ��ǥ�� ī�޶� ��ǥ�� ��ȯ
        // ** ���� �� �ִ� Ÿ���� ��ǥ�� ī�޶� ��ǥ�� ��ȯ�Ͽ� UI�� �����Ѵ�



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
