using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public int LEVEL;
    public int EXP;
    public int REQUIREEXP;
    public int MAXHP;
    public int HP;
    public int ATK;

    public int LEVELPOINT;
    public float IDAMAGE;
    public float COOL;
    public int LDAMAGE;
    public int IEXP;
    public int THROW;
    public float ATKSPEED;


    // ** 움직이는 속도
    private float Speed;
    public float MoveSpeed;

    // ** 움직임을 저장하는 벡터
    private Vector3 Movement;

    // ** 플레이어의 Animator 구성요소를 받아오기위해...
    private Animator animator;

    // ** 플레이어의 SpriteRenderer 구성요소를 받아오기위해...
    private SpriteRenderer playerRenderer;

    // ** [상태체크]
    private bool onAttack; // 공격상태
    private bool onHit; // 피격상태
    private bool onbarrier;
    // ** 복제할 총알 원본
    private GameObject BulletPrefab;
    private GameObject Obj;
    private GameObject Barrier;    
    // ** 복제할 FX 원본
    private GameObject fxPrefab;

    // 추후 list로 변경
    //public GameObject[] stageBack = new GameObject[7];
    public List<GameObject> stageBack = new List<GameObject>();

    /*
    Dictionary<string, object>;
    Dictionary<string, GameObject>;
     */

    // ** 복제된 총알의 저장공간.
    private List<GameObject> Bullets = new List<GameObject>();

    // ** 플레이어가 마지막으로 바라본 방향.
    private float Direction;

    [Header("방향")]
    // ** 플레이어가 바라보는 방향

    [Tooltip("왼쪽")]
    public bool DirLeft;
    [Tooltip("오른쪽")]
    public bool DirRight;


    private float CoolDown;
    private float time;

    

    private void Awake()
    {
        // ** player 의 Animator를 받아온다.
        animator = this.GetComponent<Animator>();

        // ** player 의 SpriteRenderer를 받아온다.
        playerRenderer = this.GetComponent<SpriteRenderer>();

        // ** [Resources] 폴더에서 사용할 리소스를 들고온다.
        BulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
        //fxPrefab = Resources.Load("Prefabs/FX/Smoke") as GameObject;
        fxPrefab = Resources.Load("Prefabs/FX/Hit") as GameObject;
    }

    // ** 유니티 기본 제공 함수
    // ** 초기값을 설정할 때 사용
    void Start()
    {
        LEVEL = 1;
        EXP = 0;
        REQUIREEXP = 100;
        MAXHP = 100;
        HP = MAXHP;
        ATK = 30;

        LEVELPOINT = 10;
        IDAMAGE = 1.0f;
        COOL = 1.0f;
        LDAMAGE = 0;
        IEXP = 0;
        THROW = 0;
        ATKSPEED = 1.0f;

        // ** 속도를 초기화.
        Speed = 5.0f;
        MoveSpeed = 1.0f;
        
        // ** 초기값 셋팅
        onAttack = true;        
        onHit = false;
        onbarrier = false;

        Direction = 1.0f;

        DirLeft = false;
        DirRight = false;

        CoolDown = 1.0f;

        Barrier = transform.Find("barrier").gameObject;

        for (int i = 0; i < stageBack.Count; ++i)
        {
            stageBack[i] = GameObject.Find(i.ToString());
           
        }
    }

    IEnumerator HPRegen()
    {
        while (MAXHP > HP)
            HP += ControllerManager.GetInstance().HPREGENLV;

        if (HP > MAXHP)
            HP = MAXHP;
        print(HP);
        yield return new WaitForSeconds(1);
    }
    // ** 유니티 기본 제공 함수
    // ** 프레임마다 반복적으로 실행되는 함수.
    void Update()
    {
        // **  Input.GetAxis =     -1 ~ 1 사이의 값을 반환함. 
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 셋중에 하나를 반환.
        float Ver = Input.GetAxisRaw("Vertical"); // -1 or 0 or 1 셋중에 하나를 반환.

        // ** 입력받은 값으로 플레이어를 움직인다.
        Movement = new Vector3(
            Hor * Time.deltaTime * Speed * ControllerManager.GetInstance().MoveSpeed,
            Ver * Time.deltaTime * Speed * (ControllerManager.GetInstance().MoveSpeed * 0.5f),
            0.0f);

        transform.position += new Vector3(0.0f, Movement.y, 0.0f);

        // ** Hor이 0이라면 멈춰있는 상태이므로 예외처리를 해준다. 
        if (Hor != 0)
            Direction = Hor;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            // ** 플레이어의 좌표가 0.1f 보다 작을때 플레이어만 움직인다.
            if (transform.position.x < 0.1f)
                transform.position += Movement;
            else
            {
                ControllerManager.GetInstance().DirRight = true;
                ControllerManager.GetInstance().DirLeft = false;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ControllerManager.GetInstance().DirRight = false;
            ControllerManager.GetInstance().DirLeft = true;

            // ** 플레이어의 좌표가 -15.0 보다 클때 플레이어만 움직인다.
            if (transform.position.x > -15.0f)
                // ** 실제 플레이어를 움직인다.
                transform.position += Movement;
        }



        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || 
            Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            ControllerManager.GetInstance().DirRight = false;
            ControllerManager.GetInstance().DirLeft = false;
        }
        

        // ** 플레이어가 바라보고있는 방향에 따라 이미지 반전 설정.
        if (Direction < 0)
        {
            playerRenderer.flipX = DirLeft = true;
        }
        else if (Direction > 0)
        {
            playerRenderer.flipX = false;
            DirRight = true;
        }

        if (onAttack)
        {
            onAttack = false;
            StartCoroutine(OnAttack());
        }



        // ** 좌측 쉬프트키를 입력한다면.....
        if (Input.GetKey(KeyCode.LeftShift))
            // ** 피격
            OnHit();

        // ** 플레이의 움직임에 따라 이동 모션을 실행 한다.
        animator.SetFloat("Speed", Hor+Ver);

        //StartCoroutine(HPRegen());

        time += Time.deltaTime;

        if(time>=1)
        {
            HP += ControllerManager.GetInstance().HPREGENLV;
            time = 0.0f;
            if (HP > MAXHP)
                HP = MAXHP;
        }

    }

    IEnumerator OnAttack()
    {
        // ** 공격모션을 실행 시킨다.
        animator.SetTrigger("Attack");

        // ** 총알원본을 본제한다.
        Obj = Instantiate(BulletPrefab);

        // ** 복제된 총알의 위치를 현재 플레이어의 위치로 초기화한다.
        Obj.transform.position = transform.position;

        // ** 총알의 BullerController 스크립트를 받아온다.
        BulletController Controller = Obj.AddComponent<BulletController>();

        // ** 총알 스크립트내부의 방향 변수를 현재 플레이어의 방향 변수로 설정 한다.
        Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

        // ** 총알 스크립트내부의 FX Prefab을 설정한다.
        Controller.fxPrefab = fxPrefab;

        // ** 총알의 SpriteRenderer를 받아온다.
        SpriteRenderer buleltRenderer = Obj.GetComponent<SpriteRenderer>();

        // ** 총알의 이미지 반전 상태를 플레이어의 이미지 반전 상태로 설정한다.
        buleltRenderer.flipY = playerRenderer.flipX;

        Obj.transform.localScale *= ControllerManager.GetInstance().BulletScale;

        // ** 모든 설정이 종료되었다면 저장소에 보관한다.
        Bullets.Add(Obj);

        while (CoolDown > 0.0f)
        {
            CoolDown -= Time.deltaTime;
            yield return null;
        }

        CoolDown = ControllerManager.GetInstance().AttackSpeed/ATKSPEED;
        onAttack = true;
    }
    IEnumerator BreakBarrier()
    {
        yield return new WaitForSeconds(0.5f);
        BarrierOff();

    }

    private void SetAttack()
    {
        // ** 함수가 실행되면 공격모션이 비활성화 된다.
        // ** 함수는 애니매이션 클립의 이벤트 프레임으로 삽입됨.
        onAttack = false;
    }

    private void OnHit()
    {
        // ** 이미 피격모션이 진행중이라면
        if (onHit)
            // ** 함수를 종료시킨다.
            return;

        // ** 함수가 종료되지 않았다면...
        // ** 피격상태를 활성화 하고.
        onHit = true;

        // ** 피격모션을 실행 시킨다.
        animator.SetTrigger("Hit");
    }

    private void SetHit()
    {
        // ** 함수가 실행되면 피격모션이 비활성화 된다.
        // ** 함수는 애니매이션 클립의 이벤트 프레임으로 삽입됨.
        onHit = false;
    }


    
    private void OnTriggerStay2D(Collider2D collision)
    {
        //enemy
        if (collision.GetComponent<EnemyController>() != null)
        {
            if (collision.GetComponentInParent<EnemyController>().Hit && collision.CompareTag("EnemyAttack"))
            {
                if (onbarrier)
                    BreakBarrier();

                //collision.GetComponentInParent
                else
                {
                    HP -= collision.GetComponent<EnemyController>().ATK-LDAMAGE;

                    collision.GetComponentInParent<EnemyController>().Hit = false;
                }
            }
        }

        //boss
        if (collision.GetComponent<BossController>() != null)
        {
            if (collision.GetComponentInParent<BossController>().Hit && collision.CompareTag("EnemyAttack"))
            {
                if (onbarrier)
                    BreakBarrier();

                //collision.GetComponentInParent
                else
                {
                    HP -= collision.GetComponent<BossController>().ATK-LDAMAGE;

                    collision.GetComponentInParent<BossController>().Hit = false;
                }
            }
        }
    }

    public void BarrierOn()
    {
        onbarrier = true;
        Barrier.SetActive(onbarrier);
    }
    public void BarrierOff()
    {
        onbarrier = false;
        Barrier.SetActive(onbarrier);
    }
}
