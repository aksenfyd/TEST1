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


    // ** �����̴� �ӵ�
    private float Speed;
    public float MoveSpeed;

    // ** �������� �����ϴ� ����
    private Vector3 Movement;

    // ** �÷��̾��� Animator ������Ҹ� �޾ƿ�������...
    private Animator animator;

    // ** �÷��̾��� SpriteRenderer ������Ҹ� �޾ƿ�������...
    private SpriteRenderer playerRenderer;

    // ** [����üũ]
    private bool onAttack; // ���ݻ���
    private bool onHit; // �ǰݻ���
    private bool onbarrier;
    // ** ������ �Ѿ� ����
    private GameObject BulletPrefab;
    private GameObject Obj;
    private GameObject Barrier;    
    // ** ������ FX ����
    private GameObject fxPrefab;

    // ���� list�� ����
    //public GameObject[] stageBack = new GameObject[7];
    public List<GameObject> stageBack = new List<GameObject>();

    /*
    Dictionary<string, object>;
    Dictionary<string, GameObject>;
     */

    // ** ������ �Ѿ��� �������.
    private List<GameObject> Bullets = new List<GameObject>();

    // ** �÷��̾ ���������� �ٶ� ����.
    private float Direction;

    [Header("����")]
    // ** �÷��̾ �ٶ󺸴� ����

    [Tooltip("����")]
    public bool DirLeft;
    [Tooltip("������")]
    public bool DirRight;


    private float CoolDown;
    private float time;

    

    private void Awake()
    {
        // ** player �� Animator�� �޾ƿ´�.
        animator = this.GetComponent<Animator>();

        // ** player �� SpriteRenderer�� �޾ƿ´�.
        playerRenderer = this.GetComponent<SpriteRenderer>();

        // ** [Resources] �������� ����� ���ҽ��� ���´�.
        BulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
        //fxPrefab = Resources.Load("Prefabs/FX/Smoke") as GameObject;
        fxPrefab = Resources.Load("Prefabs/FX/Hit") as GameObject;
    }

    // ** ����Ƽ �⺻ ���� �Լ�
    // ** �ʱⰪ�� ������ �� ���
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

        // ** �ӵ��� �ʱ�ȭ.
        Speed = 5.0f;
        MoveSpeed = 1.0f;
        
        // ** �ʱⰪ ����
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
    // ** ����Ƽ �⺻ ���� �Լ�
    // ** �����Ӹ��� �ݺ������� ����Ǵ� �Լ�.
    void Update()
    {
        // **  Input.GetAxis =     -1 ~ 1 ������ ���� ��ȯ��. 
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.
        float Ver = Input.GetAxisRaw("Vertical"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.

        // ** �Է¹��� ������ �÷��̾ �����δ�.
        Movement = new Vector3(
            Hor * Time.deltaTime * Speed * ControllerManager.GetInstance().MoveSpeed,
            Ver * Time.deltaTime * Speed * (ControllerManager.GetInstance().MoveSpeed * 0.5f),
            0.0f);

        transform.position += new Vector3(0.0f, Movement.y, 0.0f);

        // ** Hor�� 0�̶�� �����ִ� �����̹Ƿ� ����ó���� ���ش�. 
        if (Hor != 0)
            Direction = Hor;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            // ** �÷��̾��� ��ǥ�� 0.1f ���� ������ �÷��̾ �����δ�.
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

            // ** �÷��̾��� ��ǥ�� -15.0 ���� Ŭ�� �÷��̾ �����δ�.
            if (transform.position.x > -15.0f)
                // ** ���� �÷��̾ �����δ�.
                transform.position += Movement;
        }



        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || 
            Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            ControllerManager.GetInstance().DirRight = false;
            ControllerManager.GetInstance().DirLeft = false;
        }
        

        // ** �÷��̾ �ٶ󺸰��ִ� ���⿡ ���� �̹��� ���� ����.
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



        // ** ���� ����ƮŰ�� �Է��Ѵٸ�.....
        if (Input.GetKey(KeyCode.LeftShift))
            // ** �ǰ�
            OnHit();

        // ** �÷����� �����ӿ� ���� �̵� ����� ���� �Ѵ�.
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
        // ** ���ݸ���� ���� ��Ų��.
        animator.SetTrigger("Attack");

        // ** �Ѿ˿����� �����Ѵ�.
        Obj = Instantiate(BulletPrefab);

        // ** ������ �Ѿ��� ��ġ�� ���� �÷��̾��� ��ġ�� �ʱ�ȭ�Ѵ�.
        Obj.transform.position = transform.position;

        // ** �Ѿ��� BullerController ��ũ��Ʈ�� �޾ƿ´�.
        BulletController Controller = Obj.AddComponent<BulletController>();

        // ** �Ѿ� ��ũ��Ʈ������ ���� ������ ���� �÷��̾��� ���� ������ ���� �Ѵ�.
        Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

        // ** �Ѿ� ��ũ��Ʈ������ FX Prefab�� �����Ѵ�.
        Controller.fxPrefab = fxPrefab;

        // ** �Ѿ��� SpriteRenderer�� �޾ƿ´�.
        SpriteRenderer buleltRenderer = Obj.GetComponent<SpriteRenderer>();

        // ** �Ѿ��� �̹��� ���� ���¸� �÷��̾��� �̹��� ���� ���·� �����Ѵ�.
        buleltRenderer.flipY = playerRenderer.flipX;

        Obj.transform.localScale *= ControllerManager.GetInstance().BulletScale;

        // ** ��� ������ ����Ǿ��ٸ� ����ҿ� �����Ѵ�.
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
        // ** �Լ��� ����Ǹ� ���ݸ���� ��Ȱ��ȭ �ȴ�.
        // ** �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
        onAttack = false;
    }

    private void OnHit()
    {
        // ** �̹� �ǰݸ���� �������̶��
        if (onHit)
            // ** �Լ��� �����Ų��.
            return;

        // ** �Լ��� ������� �ʾҴٸ�...
        // ** �ǰݻ��¸� Ȱ��ȭ �ϰ�.
        onHit = true;

        // ** �ǰݸ���� ���� ��Ų��.
        animator.SetTrigger("Hit");
    }

    private void SetHit()
    {
        // ** �Լ��� ����Ǹ� �ǰݸ���� ��Ȱ��ȭ �ȴ�.
        // ** �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
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
