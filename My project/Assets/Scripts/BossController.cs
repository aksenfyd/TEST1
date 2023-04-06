using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;
    const int STATE_SLIDE = 3;

    private GameObject Target;

    private Animator Anim;

    // ** 플레이어의 SpriteRenderer 구성요소를 받아오기위해...
    private SpriteRenderer ren;

    private Vector3 Movement;
    private Vector3 EndPoint;
    private Vector3 Offset;

    //private float CoolDown;
    private float Speed;
    public int HP;
    public int ATK;

    private bool SkillAttack;
    private bool Attack;
    private bool Walk;
    private bool active;
    private bool Slide_Jump;
    private bool Slide;
    
    public bool Hit;

    private int choice;

    private void Awake()
    {
        Target = GameObject.Find("Player");

        Anim = GetComponent<Animator>();

        ren = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //CoolDown = 1.5f;
        Speed = 0.3f;
        HP = 2000;
        ATK = 30;
        Offset = new Vector3(1.0f, 1.0f, 0.0f);

        SkillAttack = false;
        Attack = false;
        active = true;
        Slide_Jump = false;
        Slide = false;
        Walk = false;
        Hit = false;
    }

    void Update()
    {
        float result = Target.transform.position.x - transform.position.x;

        if (result < 0.0f)
            ren.flipX = true;
        else if (result > 0.0f)
            ren.flipX = false;

        if (ControllerManager.GetInstance().DirRight)
            transform.position -= new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime;


        if (active)
        {
            active = false;
            //choice = onController();
      
            StartCoroutine(onCooldown(1));
        }
        else
        {
            switch (choice)
            {
                case STATE_WALK:
                    onWalk();
                    break;

                case STATE_ATTACK:
                    onAttack();
                    break;

                case STATE_SLIDE:
                    onSlide();
                    break;
            }
        }
        EndPoint = Target.transform.position + Offset;

        if (Vector3.Distance(EndPoint, transform.position)>10.0f)
        {
            Slide = true;
            Slide_Jump = true;
            //choice = STATE_SLIDE;
            onSlide();

        }
        else if(Vector3.Distance(EndPoint, transform.position) <= 10.0f)
        {         
            onWalk();
        }
        if (Slide)
        {
            Vector3 Direction = (EndPoint - transform.position).normalized;

            Movement = new Vector3(
                Speed * Direction.x * 30.0f,
                Speed * Direction.y * 30.0f,
                0.0f);

            transform.position += Movement * Time.deltaTime;
        }

        if(Vector3.Distance(EndPoint, transform.position) < 1.0f)
        {
            Slide = false;
            Anim.SetBool("Slide", false);
            Anim.SetTrigger("Attack");
            //Anim.SetTrigger("Attak");
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Bullet")
    //    {
    //        --HP;
    //        if (HP <= 0)
    //        {

    //            GetComponent<CapsuleCollider2D>().enabled = false;
    //        }
    //    }
    //}

    private int onController()
    {
        // ** 행동 패턴에 대한 내용을 추가 합니다.

        {
            // ** 초기화
            if (Walk)
            {
                Movement = new Vector3(0.0f, 0.0f, 0.0f);
                Anim.SetFloat("Speed", Movement.x);
                Walk = false;
            }

            if (SkillAttack)
            {
                SkillAttack = false;
            }

            if (Attack)
            {
                Attack = false;
            }

            if(Slide_Jump)
            {
                Slide_Jump = false;
            }
        }

        // ** 로직

        // ** 어디로 움직일지 정하는 시점에 플레이어의 위치를 도착지점으로 셋팅.
        EndPoint = Target.transform.position+Offset;
        

        // * [return]
        // * 1 : 이동         STATE_WALK
        // * 2 : 공격         STATE_ATTACK
        // * 3 : 슬라이딩     STATE_SLIDE
        return Random.Range(STATE_WALK, STATE_SLIDE + 1);
    }


    private IEnumerator onCooldown(float _cool)
    {
        float fTime = _cool;

        while (fTime > 0.0f)
        {
            fTime -= Time.deltaTime;
            yield return null;
        }
    }

    private void onAttack()
    {
        Attack = true;
        
            //print("onAttack");
            Anim.SetTrigger("Attack");
            //StartCoroutine(onCooldown(0.35f));


        active = true;
    }

    private void onWalk()
    {
        //print("onWalk");
        Walk = true;
        
        // ** 목적지에 도착할 때까지......
        float Distance = Vector3.Distance(EndPoint, transform.position);


        //print(EndPoint);
        //print(Distance);

        if(Distance > 0.5f)
        {
            Vector3 Direction = (EndPoint - transform.position).normalized;

            Movement = new Vector3(
                Speed * Direction.x,
                Speed * Direction.y,
                0.0f);

            transform.position += Movement * Time.deltaTime;
            Anim.SetFloat("Speed", Mathf.Abs(Movement.x));
        }
        else
            active = true;
    }

    private void onSlide()
    {
        Anim.SetTrigger("Slide_Jump");
        Anim.SetBool("Slide", true);
        onCooldown(0.15f);
    

        active = true;
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject, 0.016f);
        //Destroy(EnemyHP, 0.016f);
    }

    private void THit()
    {
        Hit = true;
    }
    private void FHit()
    {
        Hit = false;

    }
}
