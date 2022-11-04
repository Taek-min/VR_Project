using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public enum Type { A, B, C, D, E };
    public Type enemyType;
    // Start is called before the first frame update
    public float maxHealth;       // �ִ� ü��
    public float curHealth;       // ���� ü��
    public Transform Target;// B48 �׺���̼�
    public BoxCollider meleeArea;   // ���� ���� ����
    public GameObject bullet;
    public bool isChase;
    public bool isAttack;
    public bool isDead;

    protected Rigidbody rigid;
    protected BoxCollider boxCollider;
    protected Renderer[] meshs;//B46 �ǰݷ���
    protected NavMeshAgent nav;// B48 �׺���̼�
    protected Animator anim;// B48 �ִϸ��̼�

    public GameObject eventsys;

    public GameObject playerObj;
    public GameObject selfObject;
    public int expPoint;

    public Transform enemyUI;

    //HP�� �����
    private float hpPer;

    //HP �����̴�
    public Slider hpSlider;

    public void EnemyUICam()
    { 
        //�� UI�� �÷��̾�(ī�޶�)�� �ٶ� 
        Vector3 UIPos = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z) - new Vector3(enemyUI.position.x, 0, enemyUI.position.z);
        Quaternion UIRot = Quaternion.LookRotation(UIPos);
        enemyUI.rotation = UIRot;
    }

    private void Awake()//B46
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<Renderer>();//B46 �ǰݷ��� //GetComponent -> GetComponentInChildren ���� B48 ������Ʈ �߰�
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();// B48 �ִϸ��̼�

        if(enemyType != Type.D)
            Invoke("ChaseStart", 2);
    }


    protected void OnTriggerEnter(Collider other)//B46
    {
        if (other.tag == "Bullet" && !isDead)
        {
            Bullet bullet = other.GetComponent<Bullet>();
            UI UI = eventsys.GetComponent<UI>();
            Debug.Log(bullet.damage.ToString());
            curHealth -= bullet.damage + (bullet.damage * UI.AddDamage);
            Debug.Log("Range: " + curHealth);
            Vector3 reactVec = transform.position - other.transform.position;
            if(!bullet.isPenetration)
                Destroy(other.gameObject);
            StartCoroutineOnDamage(reactVec, false);//B46 �ǰݷ���
        }
    }

    private void OnTriggerStay(Collider other)//B46{
    {
        if (other.tag == "Fire")
        {
            Bullet Fire = other.GetComponent<Bullet>();
            curHealth -= Fire.Fire;
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutineOnDamage(reactVec, false);//B46 �ǰݷ���
        }
    }

    public void StartCoroutineOnDamage(Vector3 reactVec, bool isGrenade)
    {
        if (coA != null)
        {
            StopCoroutine(coA);
        }

        coA = StartCoroutine(OnDamage(reactVec, isGrenade));
    }
    Coroutine coA;
    IEnumerator OnDamage(Vector3 reactVec, bool isGrenade)//B46 �ǰݷ���
    {
        foreach(Renderer mesh in meshs)
            mesh.material.color = Color.red;

        if (curHealth > 0)
        {
            yield return new WaitForSeconds(0.5f);
            foreach (Renderer mesh in meshs)
            {
                mesh.material.color = Color.white;
            }
        }
        else
        {
            foreach (Renderer mesh in meshs)
                mesh.material.color = Color.gray;
            gameObject.layer = LayerMask.NameToLayer("EnemyDead");  //Layer�� EnemyDead�� �ٲ�
            isDead = true;
            isChase = false;// B48 �ִϸ��̼�
            nav.enabled = false;// B48 �ִϸ��̼�
            anim.SetTrigger("doDie");// B48 �ִϸ��̼�

            Vector3 deadPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);
            Debug.Log("����!!!!!!!!!!!!!!!!");

            //yield return new WaitForSeconds(2f);
            Debug.Log("�ڷ�ƾ 2�� ��� ��");
            //3. ExpSystem ������Ʈ ã��
            //2. ��ġ�� expPoint ����
            //1. ExpObject ����
            Debug.Log("Expppppppppp");
            Debug.Log("���� ��ǥ : " + deadPos);
            if(expPoint != 0)
            {
                ExpSystem.instance.CreateExp(expPoint, deadPos);
            }
            Destroy(gameObject, 2f);

            //a public���� ������Ʈ �ֱ�
            //b Gameobject.Find("ObjectName").getComponent<>()
            //c GameObject.FindObjectOfType<ExpSystem>().
            //d GameObject.FindGameObjectWithTag("TagName")    
        }
    }

    public void HitByGrenade(Vector3 explosionPos)//B47 ����ź�ǰ�
    {
        curHealth -= 100;
        Vector3 reactVec = transform.position - explosionPos;
        StartCoroutineOnDamage(reactVec, true);
    }

    void ChaseStart()// B48 �ִϸ��̼�
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }

    void Start()
    {
        
    }
    void FreezeVelocity()
    {
        if (isChase || enemyType == Type.D)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
    void Targeting()
    {
        if (!isDead && enemyType != Type.D)
        {
            float targetRadius = 0;
            float targetRange = 0;

            switch (enemyType)
            {
                case Type.A:
                    targetRadius = 1.5f;
                    targetRange = 3f;
                    break;
                case Type.B:
                    targetRadius = 1f;
                    targetRange = 12f;
                    break;
                case Type.C:
                    targetRadius = 0.5f;// ȸ�� �ӵ�
                    targetRange = 25f;  // Ÿ�� ����
                    break;
            }

            RaycastHit[] rayHits =
                Physics.SphereCastAll(transform.position,
                                        targetRadius,
                                        transform.forward,
                                        targetRange,
                                        LayerMask.GetMask("Player"));
            if (rayHits.Length > 0 && !isAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }
    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        switch(enemyType)
        {
            case Type.A:
                yield return new WaitForSeconds(0.2f);  // ��������
                meleeArea.enabled = true;

                yield return new WaitForSeconds(1f);    // ������
                meleeArea.enabled = false;

                yield return new WaitForSeconds(1f);    // �ĵ�����
                break;
            case Type.B:
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 20, ForceMode.Impulse);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                meleeArea.enabled = false;
                
                yield return new WaitForSeconds(1f);
                break;
            case Type.C:
                yield return new WaitForSeconds(0.5f);
                GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = transform.forward * 10;  // ����ü �ӵ�

                yield return new WaitForSeconds(0.2f);

                break;
        }

        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);

    }
    void FixedUpdate()
    {
        FreezeVelocity();
        Targeting();
    }

    public void EnemyHPUpdate()
    {
        //HP ����� ���
        hpPer = curHealth / maxHealth;


        //HP�����̴��� HP�ܿ��� ǥ��
        hpSlider.value = hpPer;

    }
    // Update is called once per frame
    void Update()
    {
        //Player player = playerObj.GetComponent<Player>();
        //if (player.heart <= 0)
        //{
        //    Destroy(selfObject);
        //}
        EnemyUICam();
        EnemyHPUpdate();
        if (nav.enabled)// B48 �ִϸ��̼� 
        {
            nav.SetDestination(Target.position);
            nav.isStopped = !isChase;
            //anim.SetBool("isWalk", isChase);
        }
    }

}
