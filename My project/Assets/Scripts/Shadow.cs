using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Bullet")
    //    {
    //        GetComponentInParent<EnemyController>().HP--;

    //        if (GetComponentInParent<EnemyController>().HP <= 0)
    //        {
    //            GetComponentInParent<EnemyController>().Anim.SetTrigger("Die");
    //            GetComponent<CapsuleCollider2D>().enabled = false;
    //            GetComponentInParent<EnemyController>().GetComponent<CapsuleCollider2D>().enabled = false;

    //        }
    //    }

    //}

    private GameObject Player;
    private float Bosstransparency = 1.0f;
    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponentInParent<EnemyController>() != null&&collision.tag == "Bullet")
        {

            if (critical(ControllerManager.GetInstance().CRILV))
            {
                GetComponentInParent<EnemyController>().HP
                  -= (int)((GameObject.Find("Player").GetComponent<PlayerController>().ATK + ControllerManager.GetInstance().ATKLV)
                  * 2
                  * (1 + ControllerManager.GetInstance().CRIDMGLV * 0.2f)
                  * Player.GetComponent<PlayerController>().IDAMAGE);

                print("critical" + (int)((GameObject.Find("Player").GetComponent<PlayerController>().ATK + ControllerManager.GetInstance().ATKLV)
                  * 2
                  * (1 + ControllerManager.GetInstance().CRIDMGLV * 0.2f)
                  * (Player.GetComponent<PlayerController>().IDAMAGE)));
            }
            else if(!critical(ControllerManager.GetInstance().CRILV))
            {
                GetComponentInParent<EnemyController>().HP
                      -= (int)((GameObject.Find("Player").GetComponent<PlayerController>().ATK + ControllerManager.GetInstance().ATKLV)
                      * Player.GetComponent<PlayerController>().IDAMAGE);

                print("nomal" + (int)((GameObject.Find("Player").GetComponent<PlayerController>().ATK + ControllerManager.GetInstance().ATKLV)
                    * (Player.GetComponent<PlayerController>().IDAMAGE)));
            }
      

            if (GetComponentInParent<EnemyController>().HP <= 0)
            {
                ControllerManager.GetInstance().Gold += GetComponentInParent<EnemyController>().Gold+ControllerManager.GetInstance().GOLDLV;
                Player.GetComponent<PlayerController>().EXP += GetComponentInParent<EnemyController>().EXP+Player.GetComponent<PlayerController>().IEXP;

                GetComponent<CapsuleCollider2D>().enabled = false;
                GetComponentInParent<EnemyController>().Anim.SetTrigger("Die");
                GetComponentInParent<EnemyController>().GetComponent<CapsuleCollider2D>().enabled = false;
            }
        }
        if (GetComponentInParent<BossController>() != null&&collision.tag == "Bullet")
        {

            if (critical(ControllerManager.GetInstance().CRILV))
            {
                GetComponentInParent<BossController>().HP
                  -= (int)((GameObject.Find("Player").GetComponent<PlayerController>().ATK + ControllerManager.GetInstance().ATKLV)
                  * 2
                  * (1 + ControllerManager.GetInstance().CRIDMGLV * 0.2f)
                  * Player.GetComponent<PlayerController>().IDAMAGE);

                print("critical" + (int)((GameObject.Find("Player").GetComponent<PlayerController>().ATK + ControllerManager.GetInstance().ATKLV)
                  * 2
                  * (1 + ControllerManager.GetInstance().CRIDMGLV * 0.2f)
                  * (Player.GetComponent<PlayerController>().IDAMAGE)));
            }
            else if(!critical(ControllerManager.GetInstance().CRILV))
            {
                GetComponentInParent<BossController>().HP
                      -= (int)((GameObject.Find("Player").GetComponent<PlayerController>().ATK + ControllerManager.GetInstance().ATKLV)
                      * Player.GetComponent<PlayerController>().IDAMAGE);

                print("nomal" + (int)((GameObject.Find("Player").GetComponent<PlayerController>().ATK + ControllerManager.GetInstance().ATKLV)
                    * (Player.GetComponent<PlayerController>().IDAMAGE)));
            }
      

            if (GetComponentInParent<BossController>().HP <= 0)
            {

                GetComponent<CapsuleCollider2D>().enabled = false;
                StartCoroutine(transparencycontroll(this.transform.gameObject));
                StartCoroutine(transparencycontroll(this.transform.parent.gameObject));
                GetComponentInParent<BossController>().GetComponent<CapsuleCollider2D>().enabled = false;
            }
        }

    }

    private bool critical(int i)
    {
        int criticalval = Random.Range(0, 100);
        if (criticalval < i)
        {
        
            return true;
        }
        else
        {
    
            return false;
        }
    }

    IEnumerator transparencycontroll(GameObject _Boss)
    {
        while (Bosstransparency > 0.04f)
        {
            yield return new WaitForSeconds(0.1f);
            Bosstransparency -= 0.05f;

            _Boss.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Bosstransparency);
          
        }
        if (Bosstransparency < 0.04f)
        {
            Destroy(_Boss);
        }
    }
}
