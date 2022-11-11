using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum Type { A, B, C, D, E };
    public Type enemyType;
    // Start is called before the first frame update
    public int maxHealth;       // �ִ� ü��
    public int curHealth;       // ���� ü��
    public Transform Target;// B48 �׺���̼�
    public BoxCollider meleeArea;   // ���� ���� ����
    public GameObject bullet;
    public bool isChase;
    public bool isAttack;
    public bool isDead;

    protected Rigidbody rigid;
    protected BoxCollider boxCollider;
    protected MeshRenderer[] meshs;//B46 �ǰݷ���
    protected NavMeshAgent nav;// B48 �׺���̼�
    protected Animator anim;// B48 �ִϸ��̼�

    public GameObject eventsys;

    public GameObject playerObj;
    public GameObject selfObject;


    private void Awake()//B46
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();//B46 �ǰݷ��� //GetComponent -> GetComponentInChildren ���� B48 ������Ʈ �߰�
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();// B48 �ִϸ��̼�

        if(enemyType != Type.D)
            Invoke("ChaseStart", 2);
    }


    private void OnTriggerEnter(Collider other)//B46
    {
        if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
             UI UI = eventsys.GetComponent<UI>();
            curHealth -= bullet.damage;
            curHealth -= UI.Adddamage*6;
            Debug.Log("Range: " + curHealth);
            Vector3 reactVec = transform.position - other.transform.position;
            Destroy(other.gameObject);
            StartCoroutine(OnDamage(reactVec, false));//B46 �ǰݷ���
        }
    }

    private void OnTriggerStay(Collider other)//B46{
    {
        if (other.tag == "Fire")
        {
            Bullet Fire = other.GetComponent<Bullet>();
            curHealth -= Fire.Fire;
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec, false));//B46 �ǰݷ���
        }
    }


    IEnumerator OnDamage(Vector3 reactVec, bool isGrenade)//B46 �ǰݷ���
    {
        foreach(MeshRenderer mesh in meshs)
            mesh.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.white;
        }
        else
        {
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.gray;
            gameObject.layer = 14;
            isDead = true;
            isChase = false;// B48 �ִϸ��̼�
            nav.enabled = false;// B48 �ִϸ��̼�
            anim.SetTrigger("doDie");// B48 �ִϸ��̼�

            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);
            Destroy(gameObject, 4);//4���մ� ����
        }
    }

    public void HitByGrenade(Vector3 explosionPos)//B47 ����ź�ǰ�
    {
        curHealth -= 100;
        Vector3 reactVec = transform.position - explosionPos;
        StartCoroutine(OnDamage(reactVec, true));
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

    // Update is called once per frame
    void Update()
    {
        //Player player = playerObj.GetComponent<Player>();
        //if (player.heart <= 0)
        //{
        //    Destroy(selfObject);
        //}
        
        if (nav.enabled)// B48 �ִϸ��̼� 
        {
            nav.SetDestination(Target.position);
            nav.isStopped = !isChase;
            //anim.SetBool("isWalk", isChase);
        }
    }

}