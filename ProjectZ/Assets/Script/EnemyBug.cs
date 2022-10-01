using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBug : Enemy
{
    public bool isLook;                 // 보는 중인지 확인
    public bool isDelay;                // 딜레이 중인지 확인
    public float targetRadius1 = 2f;        // 타겟팅 지름
    public float targetRange1 = 20f;        // 타겟팅 거리
    //public Vector3 Movevec = new Vector3()

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();//B46 피격로직 //GetComponent -> GetComponentInChildren 수정 B48 오브젝트 추가
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();// B48 애니매이션
       

        //nav.isStopped = true;
        //StartCoroutine(Think());
        Invoke("ChaseStart", 2);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            StopAllCoroutines();
            meleeArea.enabled = false;
            return;
        }

        if (nav.enabled)// B48 애니매이션 
        {
            nav.SetDestination(Target.position);
            nav.isStopped = !isChase;
        }
    }
    IEnumerator Attack1()
    {
        //rigid.velocity = Vector3.zero;
        isDelay = true;
        isChase = false;
        isAttack = true;

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isWalk", false);
        anim.SetBool("isAttack1", true);

        yield return new WaitForSeconds(0.198f);
        isDelay = false;
        meleeArea.enabled = true;
        rigid.AddForce(transform.forward * 20, ForceMode.Impulse);  // 전진 거리 20

        yield return new WaitForSeconds(0.5f);
        isDelay = true;
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.429f);
        anim.SetBool("isAttack1", false);

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isWalk", true);
        isDelay = false;
        isChase = true;
        isAttack = false;
        
    }

    void FixedUpdate()
    {
        FreezeVelocity();
        Targeting();
    
    void FreezeVelocity()
    {
        if (isDead || isDelay || isChase || enemyType == Type.D)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
    void Targeting()
    {
        if (!isDead && enemyType == Type.E)
        {

            if (RayHits1() && !isAttack)   // ray에 1 이상 감지되면 발생
            {
                StartCoroutine(Attack1());
            }
        }
    }

    bool RayHits1()
        {
            RaycastHit[] rayHits1 = // 근거리 공격 탐지 범위
                Physics.SphereCastAll(transform.position,
                                targetRadius1,
                                transform.forward,
                                targetRange1,
                                LayerMask.GetMask("Player"));

            if (rayHits1.Length > 0)
                return true;
            else
                return false;
        }
    }
}
