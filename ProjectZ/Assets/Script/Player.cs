using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;

    Vector3 moveVec;
    Vector3 dodgeVec;

    Rigidbody rigid;

    Animator anim;

    bool wDown;
    bool jDown;
    bool iDown; // B41 �����Լ�
    bool isDodge;
    bool isJump;

    bool isMove;


    bool isMoveingShot;

    GameObject nearObject; // B41 ������Ʈ ����

    public GameObject[] weapons; // B41 ������Ʈ ����, ����Ƽ���� ����
    public bool[] hasWeapons; // B41 ������Ʈ ����, ����Ƽ���� ����

    bool sDown1; // B41 ���ⱳü
    bool sDown2; // B41 ���ⱳü
    bool sDown3; // B41 ���ⱳü
    //GameObject equipWeapon; // B41 ���ⱳü -> B43 ���ݽ������� ����
    bool isSwap; // B41 ���ⱳü
    int equipWeaponIndex = -1; // B41 ���ⱳü

    public int ammo; //B42 ��������
    public int coin; //B42 ��������
    public int heart; //B42 ��������
    public int hasGrenades; //B42 ��������

    public int maxAmmo;//B42 ��������
    public int maxCoin;//B42 ��������
    public int maxHeart;//B42 ��������
    public int maxHasGrenades;//B42 ��������


    public GameObject[] grenades;//B42 ����ü�����

    bool fDown; //B43 ���ݽ���
    float fireDelay; //B43 ���ݽ���
    bool isFireReady = true; //B43 ���ݽ���
    Weapon equipWeapon; //B43 ���ݽ��� (B41 ���ⱳü�� ����)
    public ParticleSystem FireAtt;

    bool rDown; //B44 ����������
    bool isReload; //B44 ����������

    public Camera followCamera; //B44 ���콺ȸ��

    public GameObject grenadeObj;//B47
    bool gDown;





    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        FireAtt.Stop();
    }
    void GetInput()// Input Manager���� ����
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButton("Fire1");   //B43 ���ݽ���
        gDown = Input.GetButtonDown("Fire2");   //B47 ����ź ��ô
        rDown = Input.GetButtonDown("Reload"); // B44 ����������
        iDown = Input.GetButtonDown("Interation"); // B41 �����Լ�
        sDown1 = Input.GetButtonDown("Swap1"); // B41 ���ⱳü
        sDown2 = Input.GetButtonDown("Swap2"); // B41 ���ⱳü
        sDown3 = Input.GetButtonDown("Swap3"); // B41 ���ⱳü
    }

    void Move()
    {
        if (!isMoveingShot)
        {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
            if (isDodge)
            {
                moveVec = dodgeVec;
            }
            //if (isSwap || !isFireReady) //�������̸� Vector3.zero ����  if(isSwap) -> if(isSwap || !isFireReady) //�̵��߿��� ���ݺҰ� B43 ���ݽ���
            //    moveVec = Vector3.zero;

            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

            anim.SetBool("isRun", moveVec != Vector3.zero);
            anim.SetBool("isWalk", wDown);

        }

    }

    void MovingShot()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        if (fDown && isFireReady && !isDodge && !isSwap)
        {
            if (equipWeapon == null)
                return;
            isMoveingShot = true;
            fireDelay += Time.deltaTime;
            isFireReady = equipWeapon.rate < fireDelay;

            equipWeapon.Use();
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doMShot"); //B44 �߻籸��   anim.SetTrigger("doSwing"); -> ������ B43 ���ݽ���
            fireDelay = 0;
            if (fDown && isFireReady && !isDodge && !isSwap && equipWeaponIndex == 1)
            {
                anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee && moveVec == Vector3.zero ? "doSwing" : "doShot");
                Debug.Log("Fire");
                FireAtt.Play();
                FireAtt.GetComponent<CapsuleCollider>().enabled = true;
            }
            else if (!fDown)
            {
                FireAtt.GetComponent<CapsuleCollider>().enabled = false;
            }
                anim.SetBool("isShot", false);
        }
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;
        anim.SetBool("isMovingShot", (moveVec != Vector3.zero && fDown));



        isMoveingShot = false;

    }

    void Turn()
    {
        //1 Ű���忡 ���� ȸ��
        transform.LookAt(transform.position + moveVec);

        //2 ���콺�� ���� ȸ�� B44 ���콺ȸ��
        if (fDown)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 1000))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0;
                transform.LookAt(transform.position + nextVec);
            }
        }
    }
    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && !isJump && !isDodge && !isSwap)
        {
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")//�ٴ��� Cube��� ������Ʈ�� ������ �۵� �ױ׿� Floor��� �����ؾ���
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }

    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isJump && !isDodge && !isSwap)
        {
            dodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("DodgeOut", 0.4f);
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }

    private void OnTriggerStay(Collider other) //B41 ������Ʈ ����
    {
        if (other.tag == "Weapon")
            nearObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)//B41 ������Ʈ ����
    {
        if (other.tag == "Weapon")
            nearObject = null;
    }
    void Interation()// B41 �����Լ�
    {
        hasWeapons[2] = true;
        hasWeapons[1] = true;

        hasWeapons[0] = true;
        //if (iDown && nearObject != null && !isJump && !isDodge)
        //{
        //    if (nearObject.tag == "Weapon") {
        //        Item item = nearObject.GetComponent<Item>();
        //        int weaponIndex = item.value;
        //        hasWeapons[weaponIndex] = true;

        //        Destroy(nearObject);
        //    }
        //}
    }

    void SwapOut() // B41 ���ⱳü
    {
        isSwap = false;
    }


    void Swap() // B41 ���ⱳü
    {
        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;
        if (sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2))
            return;

        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;


        if ((sDown1 || sDown2 || sDown3) && !isJump && !isDodge)
        {
            if (equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);//B43 ���ݽ���     equipWeapon.SetActive(false); -> B41 ���ⱳü�� ������

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();//B43 ���ݽ���     equipWeapon = weapons[weaponIndex]; -> B41 ���ⱳü�� ������
            equipWeapon.gameObject.SetActive(true);//B43 ���ݽ���     equipWeapon.SetActive(true); -> B41 ���ⱳü�� ������

            anim.SetTrigger("doSwap");

            isSwap = true;

            Invoke("SwapOut", 0.4f);
        }
    }

    private void OnTriggerEnter(Collider other)//B42 ������ �Լ�
    {
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Ammo:
                    ammo += item.value;
                    if (ammo > maxAmmo)
                        ammo = maxAmmo;
                    break;

                case Item.Type.Coin:
                    coin += item.value;
                    if (coin > maxCoin)
                        coin = maxCoin;
                    break;

                case Item.Type.Heart:
                    heart += item.value;
                    if (heart > maxHeart)
                        heart = maxHeart;
                    break;

                case Item.Type.Grenade:
                    grenades[hasGrenades].SetActive(true);// ����ź ������ŭ �����ϰ� ���̱� (B42 ��������)
                    hasGrenades += item.value;
                    if (hasGrenades > maxHasGrenades)
                        hasGrenades = maxHasGrenades;
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    public bool FireAttBool;

    void Attack()//B43 ���ݽ���
    {
        if (equipWeapon == null)
            return;

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if (fDown && isFireReady && !isDodge && !isSwap && moveVec == Vector3.zero && !isMoveingShot && equipWeaponIndex == 2)
        {
            equipWeapon.Use();
            if (moveVec == Vector3.zero)
            {
                anim.SetBool("isMovingShot", false);
            }
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee && moveVec == Vector3.zero ? "doSwing" : "doShot"); //B44 �߻籸��   anim.SetTrigger("doSwing"); -> ������ B43 ���ݽ���
            fireDelay = 0;
        }
        else if (fDown && isFireReady && !isDodge && !isSwap && moveVec == Vector3.zero && !isMoveingShot && equipWeaponIndex == 1)
        {

            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee && moveVec == Vector3.zero ? "doSwing" : "doShot");
            FireAtt.Play();
            FireAtt.GetComponent<CapsuleCollider>().enabled = true;
        }
        else if (!fDown)
        {
            FireAtt.GetComponent<CapsuleCollider>().enabled = false;
            FireAtt.Stop();
        }

    }

    void Reload() //B44 ����������
    {
        if (equipWeapon == null)
            return;

        if (equipWeapon.type == Weapon.Type.Melee)
            return;

        if (ammo == 0)
            return;

        if (rDown && !isJump && !isDodge && !isSwap && isFireReady)
        {
            anim.SetTrigger("doReload");
            isReload = true;

            Invoke("ReloadOut", 2f);
        }
    }
    void Grenade()
    {
        if (hasGrenades == 0)
            return;
        if (gDown && !isReload && !isSwap)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 1000))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 10;

                GameObject instantGrenade = Instantiate(grenadeObj, transform.position, transform.rotation);
                Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>();
                rigidGrenade.AddForce(nextVec, ForceMode.Impulse);
                rigidGrenade.AddTorque(Vector3.back * 10, ForceMode.Impulse);

                hasGrenades--;
                grenades[hasGrenades].SetActive(false);
            }
        }
    }

    void ReloadOut() //B44 ����������
    {
        int reAmmo = ammo < equipWeapon.maxAmmo ? ammo : equipWeapon.maxAmmo;
        equipWeapon.curAmmo = equipWeapon.maxAmmo;
        ammo -= reAmmo;
        isReload = false;
    }
    //void FreezeVelocity()
    //{
    //    rigid.velocity = Vector3.zero;
    //    rigid.angularVelocity = Vector3.zero;
    //}
    //void FixedUpdate()
    //{
    //    FreezeVelocity();
    //}

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Turn();
        Move();
        MovingShot();
        Jump();
        Dodge();
        Interation(); // B41 �����Լ�
        Swap(); //B41 ���ⱳü
        Attack();//B43 ���ݽ���
        Reload();//B44 ����������
        Grenade();

    }


}
