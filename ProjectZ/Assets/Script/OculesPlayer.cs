using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OculesPlayer : Player
{
    public Transform cam;
    public float DashCoolTime;
    public float DashSpeed;
    public WpUIController wpUIController;

    protected bool isDashCoolTime;
    protected bool isDash;
    protected bool shotDown;   //�߻��ư
    protected bool shotUp;     //�߻��ư
    protected bool moveDown;    //�̵���ư
    protected bool dashDown;    //�뽬��ư
    protected bool swapDown;    //��ü��ư
    protected bool jumpDown;    //������ư

    protected bool possibleShot;   //�߻� ������ �����ΰ�
    protected bool possibleMove;    //�̵� ������ �����ΰ�
    protected bool possibleDash;    //�뽬 ������ �����ΰ�
    protected bool possibleSwap;    //��ü ������ �����ΰ�
    protected bool possibleJump;    //���� ������ �����ΰ�

    protected Vector3 DashVec;
    protected Vector3 headingForward;
    protected Vector3 headingRight;
    //�÷��̾� ��������
    protected Transform forwards;

    public UI UI;

    //HP�� �����
    private float hpPer;

    //HP �����̴�
    public Slider hpSlider;

    //������
    public GameObject RightHand;
    public GameObject LeftHand;
    public GameObject LeftLaser;

    // Start is called before the first frame update
    protected override void Start()
    {
        CreateTarget();
        possibleMove = true;
        possibleDash = true;
        possibleSwap = true;
        possibleJump = true;
        possibleShot = true;
    }

    private void CreateTarget()
    {
        GameObject target = Instantiate(new GameObject(), this.transform.position, this.transform.rotation, this.transform);
        target.transform.name = "Target";
        forwards = target.transform;
    }

    
    private void GetOVRInput()
    {
        moveDown = OVRInput.Get(OVRInput.Touch.PrimaryThumbstick);
        dashDown = OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick);
        shotDown = OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);   //B43 ���ݽ���
        shotUp = OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger);   //B43 ���ݽ���
        swapDown = OVRInput.Get(OVRInput.Touch.SecondaryThumbstick);
    }
    private void GetKeyInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        moveDown = OVRInput.Get(OVRInput.Touch.PrimaryThumbstick);
        dashDown = Input.GetButtonDown("Jump") || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick);
        shotDown = Input.GetButton("Fire1") || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);    //B43 ���ݽ���
        shotUp = Input.GetButtonUp("Fire1") || OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger);  //B43 ���ݽ���
        swapDown = OVRInput.Get(OVRInput.Touch.SecondaryThumbstick);
        //jumpDown = Input.GetButtonDown("Jump");
    }

    protected int ChoiceWp(Vector2 vec)
    {
        if (Mathf.Abs(vec.x) > Mathf.Abs(vec.y))
        {
            //������
            if (vec.x > 0)
            {
               // testText.text = "x>0";
                return 1;
            }
            //����
            else if (vec.x < 0)
            {//
                //testText.text = "x<0";
                return 2;
            }
        }
        else if (Mathf.Abs(vec.x) < Mathf.Abs(vec.y))
        {
            //����
            if (vec.y > 0)
            {
                //testText.text = "y>0";
                return 0;
            }
            //�Ʒ���
            else if (vec.y < 0)
            {
                //testText.text = "y<0";
                return 0;
            }
        }
        //testText.text = "0";
        return 0;
    }

    protected override void Swap()
    {
        if (!possibleSwap) return;  //��ü�� �� ���� �����̸� �ߴ�

        //testText.text = "OP Swap()";
        //������ ���̽�ƽ�� ���������� ���� ������ �ִ� ���Ⱑ ��������� ��� ȭ������� ����
        if (swapDown)
        {
            Vector2 vec = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            
            //���̽�ƽ �̵� �Ÿ��� 0.8 �̻��̸� ��üX
            if (Vector2.Distance(vec, Vector2.zero) > 0.8)
                ShowWp(ChoiceWp(OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick)));
            
            //testText.text = vec.ToString();
        }
    }
    protected override void ShowWp(int wpIndex)
    {
        //�ε��� ��ȣ�� �̻��ϸ� �ߴ�, ���� ���� ��ȣ�� �Ű����� ��ȣ�� ������ �ߴ�
        if (wpIndex < 0 || wpIndex > weapons.Length - 1 || equipWeaponIndex == wpIndex)
        {
            return;
        }

        if (weapons[equipWeaponIndex] != null)
        {
            weapons[equipWeaponIndex].GetComponent<WeaponController>().gun.SetActive(false);    
        }
        equipWeaponIndex = wpIndex;
        //equipWeapon = weapons[equipWeaponIndex].GetComponent<Weapon>();
        //weapons[equipWeaponIndex].gun.SetActive(true);
        //testText.text = "139";

        weapons[equipWeaponIndex].GetComponent<WeaponController>().gun.SetActive(true);
        if(wpUIController)
        {
            wpUIController.ShowWpUI(equipWeaponIndex);
        }

    }
    public void HPUpdate()
    {
        //HP ����� ���
        hpPer = heart / maxHeart;


        //HP�����̴��� HP�ܿ��� ǥ��
        hpSlider.value = hpPer;

    }


    public void UICam()
    {
        //�÷��̾� HP�� �÷��̾�(ī�޶�)�� �ٶ� 
        Vector3 UIPos = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z) - new Vector3(hpSlider.transform.position.x, 0, hpSlider.transform.position.z);
        Quaternion UIRot = Quaternion.LookRotation(UIPos);
        hpSlider.transform.rotation = UIRot;
    }

    public void Dash()
    {
        if (!possibleDash) return;

        Vector2 pos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        moveVec = new Vector3(pos.x, 0, pos.y);
        if (moveVec != Vector3.zero && !isJump && !isDash && !isSwap && !fDown && dashDown)
        {
            DashVec = moveVec;
            speed *= DashSpeed;
            isDash = true;
            isDashCoolTime = true;

            Invoke("DashOut", 0.4f);
        }
    }
    public void DashOut()
    {
        speed /= DashSpeed;
        isDash = false;
        Invoke("DashTimer", DashCoolTime);
    }
    public void DashTimer()
    {
        isDashCoolTime = false;
    }
    public void Shootting()
    {
        if(shotDown || shotUp)
        {
            //equipWeapon2.Attack(shotDown, shotUp);
            weapons[equipWeaponIndex].GetComponent<WeaponController>().Attack(shotDown, shotUp);
        }
    }
    protected override void DiePlayer()
    {
        if (heart <= 0)
        {
            eventsys.GetComponent<UI>().LevelUpUI.SetActive(false);
            gameObj.SetActive(false);
            DeadUI.SetActive(true);

            RightHand.SetActive(false);
            LeftHand.SetActive(false);

            LeftLaser.SetActive(true);
            gameObject.transform.position = new Vector3(0, 4, -9);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }    
    public void CamVec()
    {
        forwards.transform.rotation = cam.transform.rotation;
        forwards.transform.rotation = Quaternion.Euler(forwards.rotation.eulerAngles.x, forwards.transform.rotation.eulerAngles.y - this.transform.rotation.eulerAngles.y, forwards.rotation.eulerAngles.z);
        headingForward = forwards.transform.forward;
        headingRight = forwards.transform.right;
    }
    protected override void Move()
    {
        //testText.text = "OP Move()";

        CamVec();

        if (moveDown)
        {
            Vector2 pos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            Vector3 f = new Vector3(headingForward.x * speed * Time.deltaTime, 0, headingForward.z * speed * Time.deltaTime);
            this.transform.Translate(f * pos.y);
            Vector3 r = new Vector3(headingRight.x * speed * Time.deltaTime, 0, headingRight.z * speed * Time.deltaTime);
            this.transform.Translate(r * pos.x);
            //testText.text = pos.ToString();
        }

    }
    // Update is called once per frame
    public override void Update()
    {
        //GetOVRInput();
        GetKeyInput();
        Move();
        {
            float mouseX = 0;
            float mouseY = 0;
            float mouseSpeed = 5.0f;


            mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
            //mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y") * mouseSpeed;
            //mouseY += Input.GetAxis("Mouse Y");
            mouseY = Mathf.Clamp(mouseY, -90, 90);    //�� ������ ���� ����
            cam.eulerAngles += new Vector3(-mouseY, mouseX, 0);
        }
        {
            Vector3 mv = new Vector3(hAxis, 0, vAxis);
            transform.Translate(cam.transform.TransformDirection(mv) * Time.deltaTime * speed);
            if(Input.GetKeyDown(KeyCode.E))
            {
                DeadUI.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                DeadUI.SetActive(true);
                Debug.Log("Ű ����");
            }
        }
        Swap();
        Dash();
        //Jump();
        Shootting();
        HPUpdate();
        UICam();
        DiePlayer();
    }
}
