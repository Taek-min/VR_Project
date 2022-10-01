using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float DodgeCoolTime;

    public float DodgeSpeed;

    public float DamageCoolTime = 1;
    float hAxis;
    float vAxis;
    
    protected Vector3 moveVec;
    protected Vector3 dodgeVec;


    protected Rigidbody rigid;

    Animator anim;
    protected MeshRenderer[] meshs;

    protected bool wDown;
    protected bool jDown;
    protected bool iDown; // B41 무기입수
    protected bool isDodge;

    protected bool isJump;
    protected bool isBorder;
    protected bool isDodgeCoolTime;

    protected bool isDamage;
    protected bool isMove;
    protected bool isMoveingShot;

    protected GameObject nearObject; // B41 오브젝트 감지

    public GameObject[] weapons; // B41 오브젝트 감지, 유니티에서 설정
    public bool[] hasWeapons; // B41 오브젝트 감지, 유니티에서 설정

    protected bool sDown1; // B41 무기교체
    protected bool sDown2; // B41 무기교체
    protected bool sDown3; // B41 무기교체
    //GameObject equipWeapon; // B41 무기교체 -> B43 공격실행으로 수정
    protected bool isSwap; // B41 무기교체
    public int equipWeaponIndex = -1; // B41 무기교체

    public float ammo; //B42 변수선언
    public int coin; //B42 변수선언
    public float heart; //B42 변수선언
    public int hasGrenades; //B42 변수선언

    public float maxAmmo;//B42 변수생성
    public int maxCoin;//B42 변수생성
    public int maxHeart;//B42 변수생성
    public int maxHasGrenades;//B42 변수생성


    public GameObject[] grenades;//B42 공전체만들기

    protected bool fDown; //B43 공격실행
    protected float fireDelay; //B43 공격실행
    protected bool isFireReady = true; //B43 공격실행
    protected Weapon equipWeapon; //B43 공격실행 (B41 무기교체를 수정)
    public ParticleSystem FireAtt;

    bool rDown; //B44 재장전구현
    bool isReload; //B44 재장전구현

    public Camera followCamera; //B44 마우스회전

    public GameObject grenadeObj;//B47
    bool gDown;
    bool isMSwap;

    public GameObject eventsys;
    public Transform PlayerTransform;

    public GameObject gameObj;
    public GameObject DeadUI;
    public Text testText;

    



    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        UI UI = eventsys.GetComponent<UI>();
        heart += UI.AddHP;
        speed += UI.AddSpeed;
    }

    protected virtual void Start()
    {
        //CreateTarget();
        //FireAtt.Stop();
        int weaponIndex = 2;
        if (equipWeapon != null)
            equipWeapon.gameObject.SetActive(false);//B43 공격실행     equipWeapon.SetActive(false); -> B41 무기교체로 수정전

        equipWeaponIndex = weaponIndex;
        equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();//B43 공격실행     equipWeapon = weapons[weaponIndex]; -> B41 무기교체로 수정전
        equipWeapon.gameObject.SetActive(true);//B43 공격실행     equipWeapon.SetActive(true); -> B41 무기교체로 수정전

    }
    protected virtual void GetInput()// Input Manager에서 셋팅
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButton("Fire1");   //B43 공격실행
        gDown = Input.GetButtonDown("Fire2");   //B47 수류탄 투척
        rDown = Input.GetButtonDown("Reload"); // B44 재장전구현
        iDown = Input.GetButtonDown("Interation"); // B41 무기입수
        sDown1 = Input.GetButtonDown("Swap1"); // B41 무기교체
        sDown2 = Input.GetButtonDown("Swap2"); // B41 무기교체
    }

    //public void Move()
    protected virtual void Move()
    {
        //testText.text = "P Move()";

        //forwards.transform.rotation = cam.transform.rotation;
        //forwards.transform.rotation = Quaternion.Euler(forwards.rotation.eulerAngles.x, forwards.transform.rotation.eulerAngles.y - this.transform.rotation.eulerAngles.y, forwards.rotation.eulerAngles.z);
        //headingForward = forwards.transform.forward;
        //headingRight = forwards.transform.right;

        //if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick))
        //{
        //    Vector2 pos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        //    Vector3 f = new Vector3(headingForward.x * speed * Time.deltaTime, 0, headingForward.z * speed * Time.deltaTime);
        //    this.transform.Translate(f * pos.y);
        //    Vector3 r = new Vector3(headingRight.x * speed * Time.deltaTime, 0, headingRight.z * speed * Time.deltaTime);
        //    this.transform.Translate(r * pos.x);
        //    testText.text = pos.ToString();
        //}

    }

    void MovingShot()
    {
        if (isDodge) return;
        moveVec = new Vector3(hAxis + vAxis, 0, vAxis - hAxis).normalized;
        if (isDodge)
            moveVec = dodgeVec;
        if (fDown && isFireReady && !isDodge && !isSwap)
        {
            if (equipWeapon == null)
                return;
            isMoveingShot = true;
            fireDelay += Time.deltaTime;
            isFireReady = equipWeapon.rate < fireDelay;

            equipWeapon.Use();
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doMShot"); //B44 발사구현   anim.SetTrigger("doSwing"); -> 수정전 B43 공격실행
            fireDelay = 0;
            if (fDown && isFireReady && !isDodge && !isSwap && equipWeaponIndex == 1 && (equipWeapon.curAmmo >= equipWeapon.oneAmmo))
            {
                anim.SetBool("isShot", false);
                anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee && moveVec == Vector3.zero ? "doSwing" : "doShot");
                equipWeapon.curAmmo -=1;
                FireAtt.Play();
                FireAtt.GetComponent<CapsuleCollider>().enabled = true;
                anim.SetBool("isShot", false);
            }
            else if (!fDown)
            {
                FireAtt.GetComponent<CapsuleCollider>().enabled = false;
            }
            else if (equipWeaponIndex == 1 && equipWeapon.curAmmo < equipWeapon.oneAmmo)
            {
                Invoke("FireStop", 5f);
                FireAtt.GetComponent<CapsuleCollider>().enabled = false;
                
            }
            anim.SetBool("isShot", false);
        }
        if (!isBorder)
            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;
        anim.SetBool("isMovingShot", (moveVec != Vector3.zero && fDown));



        isMoveingShot = false;

    }


    void MovingSwap() // B41 무기교체
    {
        if (sDown1 && (!hasWeapons[2] || equipWeaponIndex == 2))
            return;
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;


        int weaponIndex = -1;
        if (sDown1) weaponIndex = 2;
        if (sDown2) weaponIndex = 1;
      


        if ((sDown1 || sDown2) && !isJump && !isDodge && moveVec != Vector3.zero && !fDown)
        {
            if (equipWeapon != null)
            {
                equipWeapon.Gun.SetActive(false);//B43 공격실행     equipWeapon.SetActive(false); -> B41 무기교체로 수정전
                //Gun.SetActive(false);
            }
            anim.SetBool("isMSwap", true);
            anim.SetTrigger("doMSwap");
            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();//B43 공격실행     equipWeapon = weapons[weaponIndex]; -> B41 무기교체로 수정전
            equipWeapon.Gun.SetActive(true);//B43 공격실행     equipWeapon.SetActive(true); -> B41 무기교체로 수정전



        } else
        {
            anim.SetBool("isMSwap", false);
        }
        isMSwap = true;

        Invoke("MSwapOut", 0.5f);
    }
    void MSwapOut() // B41 무기교체
    {
        isSwap = false;
    }

    void Turn()
    {
        //1 키보드에 의한 회전
        transform.LookAt(transform.position + moveVec);

        //2 마우스에 의한 회전 B44 마우스회전
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


    protected void Jump()
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
        if (collision.gameObject.tag == "Floor")//바닥이 Cube라는 오브젝트에 다으면 작동 테그에 Floor라고 지정해야함
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }
    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }
    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 2, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 2, LayerMask.GetMask("Wall"));
    }
    void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }
    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isJump && !isDodge && !isSwap && !fDown)
        {
            dodgeVec = moveVec;
            speed *= DodgeSpeed;
            anim.SetTrigger("doDodge");
            isDodge = true;
            isDodgeCoolTime = true;

            Invoke("DodgeOut", 0.4f);
        }
    }

    void DodgeOut()
    {
        speed /= DodgeSpeed;
        isDodge = false;
        Invoke("DodgeTimer", DodgeCoolTime);
    }
    void DodgeTimer()
    {
        isDodgeCoolTime = false;
    }
    private void OnTriggerStay(Collider other) //B41 오브젝트 감지
    {
        if (other.tag == "Weapon")
            nearObject = other.gameObject;
        
    }
    
    IEnumerator OnDamage(bool isBossAtk)
    {
        isDamage = true;
        foreach (MeshRenderer mesh in meshs)
            mesh.material.color = Color.yellow;

        if (isBossAtk)
            rigid.AddForce(transform.forward * -25, ForceMode.Impulse);

        yield return new WaitForSeconds(DamageCoolTime * 1 / 3);
        rigid.velocity = Vector3.zero;
        yield return new WaitForSeconds(DamageCoolTime * 2 / 3);
        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
            mesh.material.color = Color.white;

        //if (isBossAtk)
        rigid.velocity = Vector3.zero;
    }
    private void OnTriggerExit(Collider other)//B41 오브젝트 감지 //사용X
    {
        if (other.tag == "Weapon")
            nearObject = null;
    }
    void Interation()// B41 무기입수 //사용X
    {
        hasWeapons[2] = true;
        hasWeapons[1] = true;
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

    void SwapOut() // B41 무기교체
    {
        isSwap = false;
    }


    protected virtual void Swap() // B41 무기교체
    {
        if (sDown1 && (equipWeaponIndex == 2))
            return;
        if (sDown2 && (equipWeaponIndex == 1))
            return;

        int weaponIndex = -1;
        if (sDown1) weaponIndex = 2;
        if (sDown2) weaponIndex = 1;

        if ((sDown1 || sDown2) && !isJump && !isDodge && moveVec == Vector3.zero)
        {
            ShowWp(weaponIndex);
            anim.SetTrigger("doSwap");

            isSwap = true;

            Invoke("SwapOut", 0.4f);
        }
    }

    private void OnTriggerEnter(Collider other)//B42 아이템 입수
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
                    
                    hasGrenades += item.value;
                    if (hasGrenades > maxHasGrenades)
                        hasGrenades = maxHasGrenades;
                    grenades[hasGrenades-1].SetActive(true);// 수류탄 갯수만큼 증가하게 보이기 (B42 공전구현)
                    break;
            }
            Destroy(other.gameObject);
        }
        if (other.tag == "EnemyBullet")
        {
            if (!isDamage)
            {
                Bullet enemyBullet = other.GetComponent<Bullet>();
                heart -= enemyBullet.damage;

                bool isBossAtk = other.name == "BossMeleeArea";
                StartCoroutine(OnDamage(isBossAtk));
            }
            if (other.GetComponent<Rigidbody>() != null)
            {
                Destroy(other.gameObject);
            }
        }
    }

    public bool FireAttBool;

    void Attack()//B43 공격실행
    {
        if (equipWeapon == null)
            return;

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;
        if(fDown && moveVec == Vector3.zero)
        {
            anim.SetTrigger("doShot");
            anim.SetBool("isShot", true);
        }

        if (fDown && isFireReady && !isDodge && !isSwap && moveVec == Vector3.zero && !isMoveingShot && equipWeaponIndex == 2)
        {
            anim.SetBool("isShot", true);
            equipWeapon.Use();
            if (moveVec == Vector3.zero)
            {
                anim.SetBool("isMovingShot", false);
                anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee && moveVec == Vector3.zero ? "doSwing" : "doShot"); //B44 발사구현   anim.SetTrigger("doSwing"); -> 수정전 B43 공격실행
                fireDelay = 0;
            }         
            
        }
        else if (fDown && isFireReady && !isDodge && !isSwap && moveVec == Vector3.zero && !isMoveingShot && equipWeaponIndex == 1 && equipWeapon.curAmmo > equipWeapon.oneAmmo)
        {
            anim.SetBool("isShot", true);
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee && moveVec == Vector3.zero ? "doSwing" : "doShot");
            FireAtt.Play();
            FireAtt.GetComponent<CapsuleCollider>().enabled = true;
        }
        else if (!fDown)
        {
            FireAtt.GetComponent<CapsuleCollider>().enabled = false;
            FireAtt.Stop();
        }
        else if (equipWeaponIndex == 1 && fDown && equipWeapon.curAmmo < equipWeapon.oneAmmo)
        {
            FireAtt.GetComponent<CapsuleCollider>().enabled = false;
            FireAtt.Stop();
            anim.SetBool("isMovingShot", false);
            
        }

    }

    void ReloadOut() //B44 재장전구현
    {
        float reAmmo = ammo < equipWeapon.maxAmmo ? ammo : equipWeapon.maxAmmo;
        equipWeapon.curAmmo = equipWeapon.maxAmmo;
        ammo -= reAmmo;
        isReload = false;
    }

    void Reload() //B44 재장전구현
    {
        if (equipWeapon == null)
            return;

        if (equipWeapon.type == Weapon.Type.Melee)
            return;

        if (ammo == 0)
            return;

        if (rDown && !isJump && !isDodge && !isSwap && isFireReady && equipWeapon.type == Weapon.Type.FireGun)
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


    // Update is called once per frame

    protected virtual void DiePlayer()
    {
        if(heart <= 0)
        {
            gameObj.SetActive(false);
            DeadUI.SetActive(true);
        }
    }
 
    protected virtual void ShowWp(int wpIndex)
    {
        //인덱스 번호가 이상하면 중단, 현재 무기 번호와 매개변수 번호가 같으면 중단
        if (wpIndex < 0 || weapons.Length - 1 < wpIndex || equipWeaponIndex == wpIndex)
            return;

        if (weapons[equipWeaponIndex] != null)
        {
            
            weapons[equipWeaponIndex].GetComponent<Weapon>().Gun.SetActive(false);//B43 공격실행     equipWeapon.SetActive(false); -> B41 무기교체로 수정전
        }
        equipWeaponIndex = wpIndex;
        //equipWeapon = weapons[equipWeaponIndex].GetComponent<Weapon>();
        //weapons[equipWeaponIndex].gun.SetActive(true);
        weapons[equipWeaponIndex].GetComponent<Weapon>().Gun.SetActive(true);
    }

    public virtual void Update()
    {
        Move();
        Swap();
        GetInput();
        Turn();
        //Move();
        MovingShot();
        MovingSwap();
        Dodge();
        Jump();
        //Interation(); // B41 무기입수
        //Swap(); //B41 무기교체
        Attack();//B43 공격실행
        Reload();//B44 재장전구현
        Grenade();
        DiePlayer();
    }
}
