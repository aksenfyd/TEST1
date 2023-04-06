using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EnemyManager() { }
    private static EnemyManager instance = null;
    public static EnemyManager GetInstance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    
    // ** 생성되는 Enemy를 담아둘 상위 객체
    private GameObject Parent;

    // ** Enemy로 사용할 원형 객체
    private GameObject Prefab;
    private GameObject BossPrefab;
    private GameObject HPPrefab;

    private GameObject BackGround1;
    private GameObject BackGround2;
    private GameObject BackGround3;
    private GameObject BackGround4;

    private List<GameObject> BackGround1C = new List<GameObject>();
    private List<GameObject> BackGround2C = new List<GameObject>();
    private List<GameObject> BackGround3C = new List<GameObject>();
    private List<GameObject> BackGround4C = new List<GameObject>();
    private float transparency;

    private bool BossRegen;

    public EnemyHPbar EnemyHPBar;
    // ** 플레이어의 누적 이동 거리
    public float Distance;

    public int LEVEL;
    public int EXP;
    public int HP;
    public int ATK;
    public int GOLD;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            Distance = 0.0f;

            LEVEL = 1;
            EXP = 10;
            HP = 100;
            ATK = 5;
            GOLD = 10;

            // ** 씬이 변경되어도 계속 유지될 수 있게 해준다.
            DontDestroyOnLoad(gameObject);

            // ** 생성되는 Enemy를 담아둘 상위 객체
            Parent = new GameObject("EnemyList");

            // ** Enemy로 사용할 원형 객체
            Prefab = Resources.Load("Prefabs/Enemy/Enemy") as GameObject;
            BossPrefab = Resources.Load("Prefabs/Enemy/Boss") as GameObject;
            HPPrefab = Resources.Load("Prefabs/HP") as GameObject;

            BackGround1 = GameObject.Find("Background1");
            BackGround2 = GameObject.Find("Background2");
            BackGround3 = GameObject.Find("Background3");
            BackGround4 = GameObject.Find("Background4");

            transparency = 1.0f;

            BossRegen = false;

            BackGround2.SetActive(false);
            BackGround3.SetActive(false);
            BackGround4.SetActive(false);

            for (int i = 0; i < BackGround1.transform.childCount; ++i)
            {
                BackGround1C.Add(BackGround1.transform.GetChild(i).gameObject);
                BackGround2C.Add(BackGround2.transform.GetChild(i).gameObject);
                BackGround3C.Add(BackGround3.transform.GetChild(i).gameObject);
                BackGround4C.Add(BackGround4.transform.GetChild(i).gameObject);
            }
        }
    }

    // ** 시작하자마자 Start함수를 코루틴 함수로 실행
    private IEnumerator Start()
    {
        while(true)
        {
            // ** Enemy 원형객체를 복제한다.
            GameObject Obj = Instantiate(Prefab);

            // ** Enemy HP UI 복제
            GameObject Bar = Instantiate(HPPrefab);

            // ** 복제된 UI를 캔버스에 
            Bar.transform.SetParent(GameObject.Find("EnemyHPCanvas").transform);


            // ** Enemy 작동 스크립트 포함.
            //Obj.AddComponent<EnemyController>();

            // ** 클론의 위치를 초기화.
            Obj.transform.position = new Vector3(
                18.0f, Random.Range(-5.2f, -3.2f), 0.0f);
                //18.0f, 3.5f, 0.0f);

           //Obj.transform.position = new Vector3(
           //     18.0f, -7.5f, 0.0f);

            // ** 클론의 이름 초기화.
            Obj.transform.name = "Enemy";

            // ** 클론의 계층구조 설정.
            Obj.transform.parent = Parent.transform;

            
            // ** UI 객체가 들고있는 스크립트 설정
            EnemyHPBar = Bar.GetComponent<EnemyHPbar>();


            EnemyHPBar.Target = Obj;
            // ** 1.5초 휴식.
            yield return new WaitForSeconds(1.5f);
        }
    }


    private void Update()
    {
        if (ControllerManager.GetInstance().DirRight)
        {
            Distance += Input.GetAxisRaw("Horizontal") * Time.deltaTime;
         
        }

        if (Distance >= 10 * LEVEL) 
        {
            LEVEL += 1;
            EXP += 5;
            GOLD += 5;
            HP += 50;
            ATK += 5;

            if(LEVEL%5==1)
            {                
                if (LEVEL/5==1)
                {
                    transparency = 1.0f;
                    StartCoroutine(transparencycontroll1(BackGround1C, BackGround2C,BackGround1));
                    
                    for (int i = 0; i < BackGround2C.Count; ++i)
                    {
                        BackGround2C[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                        BackGround2C[i].GetComponent<SpriteRenderer>().enabled = true;
                    }
                    BackGround2.SetActive(true);
                }
                if(LEVEL/5==2)
                {
                    transparency = 1.0f;
                    StartCoroutine(transparencycontroll1(BackGround2C, BackGround3C, BackGround2));

                    for (int i = 0; i < BackGround3C.Count; ++i)
                    {
                        BackGround3C[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                        BackGround3C[i].GetComponent<SpriteRenderer>().enabled = true;
                    }
                    BackGround3.SetActive(true);
                }
                if(LEVEL/5==3)
                {
                    transparency = 1.0f;
                    StartCoroutine(transparencycontroll1(BackGround3C, BackGround4C, BackGround3));

                    for (int i = 0; i < BackGround4C.Count; ++i)
                    {
                        BackGround4C[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                        BackGround4C[i].GetComponent<SpriteRenderer>().enabled = true;
                    }
                    BackGround4.SetActive(true);
                }
            }
        }

        //if(Distance>=50&&!BossRegen)
        if (Distance>=1&&!BossRegen)
        {
            BossRegen = true;
            // ** Enemy 원형객체를 복제한다.
            GameObject Obj = Instantiate(BossPrefab);

            // ** Enemy HP UI 복제
            GameObject Bar = Instantiate(HPPrefab);

            // ** 복제된 UI를 캔버스에 
            Bar.transform.SetParent(GameObject.Find("EnemyHPCanvas").transform);


            // ** Enemy 작동 스크립트 포함.
            //Obj.AddComponent<EnemyController>();

            // ** 클론의 위치를 초기화.
            Obj.transform.position = new Vector3(
                2.0f, -4.2f, 0.0f);
            
            // ** 클론의 이름 초기화.
            Obj.transform.name = "Boss";

            // ** 클론의 계층구조 설정.
            Obj.transform.parent = Parent.transform;


            // ** UI 객체가 들고있는 스크립트 설정
            EnemyHPBar = Bar.GetComponent<EnemyHPbar>();


            EnemyHPBar.Target = Obj;
        }

     
    }

    IEnumerator transparencycontroll1(List<GameObject> A, List<GameObject> B,GameObject C)
    {
        while (transparency > 0.04f)
        {
            yield return new WaitForSeconds(0.1f);
            transparency -= 0.05f;

            for (int i = 0; i < A.Count; ++i)
            {
                A[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transparency);
            }
            
            for (int i = 0; i < B.Count; ++i)
            {
                B[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1-transparency);
            }
        }
        if(transparency<0.04f)
        {
            C.SetActive(false);
        }
    }
}
