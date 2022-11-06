using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGolem : Enemy
{
    public bool isLook;                 // ���� ������ Ȯ��
    public bool isDelay;                // ������ ������ Ȯ��
    Vector3 lookVec;
    Vector3 tauntVec;
    public float targetRadius1 = 2f;        // Ÿ���� ����
    public float targetRange1 = 5f;        // Ÿ���� �Ÿ�
    public float targetRadius2 = 0.5f;      // Ÿ���� ����
    public float targetRange2 = 25f;        // Ÿ���� �Ÿ�

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<Renderer>();//B46 �ǰݷ��� //GetComponent -> GetComponentInChildren ���� B48 ������Ʈ �߰�
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();// B48 �ִϸ��̼�

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
        //if (isLook)
        //{
        //    float h = Input.GetAxisRaw("Horizontal");
        //    float v = Input.GetAxisRaw("Vertical");
        //    lookVec = new Vector3(h + v, 0, v - h) * 5f;
        //    transform.LookAt(Target.position + lookVec);
        //}
        //else
        //{
        //    nav.SetDestination(Target.position);
        //}
        if (nav.enabled)// B48 �ִϸ��̼� 
        {
            nav.SetDestination(Target.position);
            nav.isStopped = !isChase;
        }
    }
    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);



        //if (rayHits1.Length > 0 && !isAttack)
        //{
        //    StartCoroutine(Attack1());
        //}
    }
    IEnumerator Attack1()
    {
        //rigid.velocity = Vector3.zero;
        isDelay = true;
        isChase = false;
        isAttack = true;

        anim.SetBool("isWalk", false);
        anim.SetBool("isAttack1", true);
        
        yield return new WaitForSeconds(0.8f);
        isDelay = false;
        meleeArea.enabled = true;
        rigid.AddForce(transform.forward * 40, ForceMode.Impulse);  // ���� �Ÿ� 40


        yield return new WaitForSeconds(0.2f);
        //rigid.velocity = Vector3.zero;
        isDelay = true;
        meleeArea.enabled = false;


        yield return new WaitForSeconds(1f);
        anim.SetBool("isWalk", true);
        anim.SetBool("isAttack1", false);

        isDelay = false;
        isChase = true;
        isAttack = false;
    }
    IEnumerator Attack2()
    {
        yield return null;
    }
    IEnumerator DelayTime(int delayTime)
    {
        for (int i = 0; i > delayTime; i++)
        {
            yield return new WaitForSeconds(0.1f);
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
    void FixedUpdate()
    {
        FreezeVelocity();
        Targeting();
    }
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
            if (RayHits1() && RayHits2() && !isAttack)   // ray�� 1 �̻� �����Ǹ� �߻�
            {
                Debug.Log("Attack1");
                StartCoroutine(Attack1());
            }
            else if (RayHits2() && !isAttack)
            {
                StartCoroutine(Attack2());
            }
            else if (!RayHits1() && !isAttack)
            {
                //anim.SetBool("isWalk", true);
            }
            else if (!RayHits1() && !isAttack)
            {
                //anim.SetBool("isWalk", false);
            }
        }
    }

    bool RayHits1()
    {
        RaycastHit[] rayHits1 = // �ٰŸ� ���� Ž�� ����
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
    bool RayHits2()
    {
        RaycastHit[] rayHits2 = // ���Ÿ� ���� Ž�� ����
            Physics.SphereCastAll(transform.position,
                            targetRadius2,
                            transform.forward,
                            targetRange2,
                            LayerMask.GetMask("Player"));

        if (rayHits2.Length > 0)
            return true;
        else
            return false;
    }
}
