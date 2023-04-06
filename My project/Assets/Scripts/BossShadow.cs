using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShadow : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            GetComponentInParent<BossController>().HP--;

            if (GetComponentInParent<BossController>().HP <= 0)
            {
                //GetComponentInParent<BossController>().Anim.SetTrigger("Die");
                GetComponent<CapsuleCollider2D>().enabled = false;
                GetComponentInParent<BossController>().GetComponent<CapsuleCollider2D>().enabled = false;

            }
        }

    }
}
