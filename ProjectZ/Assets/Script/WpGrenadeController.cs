using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WpGrenadeController : WeaponController
{
    //����ź �����̴� ����
    public enum PowerMode
    {
        Up, Down
    }
    public PowerMode powerMode = PowerMode.Up;

    //����ź �Ŀ�
    public float power;

    //�Ŀ� �����̴�
    //public Slider powerSlider;
    public Image powerGageFront;
    public Image powerGageBack;
    //�÷��̾�(����ź ������ ���� ���)
    public GameObject player;
    //����ź ������
    public GameObject GrenadePrefab;


    protected override void Start() { base.Start();  }
    protected override void Update(){ base.Update(); }
    protected void OnEnable()   {
        power = 0;
        powerGageFront.rectTransform.sizeDelta = new Vector2(0, 25);
        powerGageBack.gameObject.SetActive(false);
    }
    protected void OnDisable()  { 
        powerGageFront.rectTransform.sizeDelta = new Vector2(0, 25); power = 0;
    }

    public override void Attack(bool shootDown, bool shootUp)
    {
        if (shootDown && !shootUp)
        {
            powerGageBack.gameObject.SetActive(true);
            
            GrenadeCharge();
        }
        else if (shootUp && !shootDown)
        {
            powerGageBack.gameObject.SetActive(false);
            GrenadeShoot();
        }
    }

    public void GrenadeCharge()
    {
        if (curAmmo < oneAmmo) return;

        //�Ŀ� ����
        switch (powerMode)
        {
            //�Ŀ��� �ö� ��
            case PowerMode.Up:
                {
                    
                    //Mathf.MoveTowards (A(power)���� B(1.0f)���� 2.5f�� ������ �ӵ��� �����δ�.)
                    power = Mathf.MoveTowards(power, 1.0f, 2.5f * Time.deltaTime);
                    if (power == 1.0f)
                    {
                        powerMode = PowerMode.Down;
                    }
                    break;
                }

            //�Ŀ��� ������ ��
            case PowerMode.Down:
                {
                    //Mathf.MoveTowards (A(power)���� B(1.0f)���� 2.5f�� ������ �ӵ��� �����δ�.)
                    power = Mathf.MoveTowards(power, 0, 2.5f * Time.deltaTime);
                    if (power == 0)
                    {
                        powerMode = PowerMode.Up;
                    }
                    break;
                }
        }
        //�Ŀ������̴��� value���� power�� ���Թ޴´�.
        powerGageFront.rectTransform.sizeDelta = new Vector2(100f * power, 25);
    }

    public void GrenadeShoot()
    {
        if (curAmmo < oneAmmo) return;

        //����ź ���� -> Instantiate(������) => (����, ���� ��ġ, ���� �� ȸ�� ��)
        GameObject GranadePrfab = Instantiate(GrenadePrefab, spawnPosition[0].position, transform.rotation);
        //GranadePrfab.AddComponent<Rigidbody>();
        //����ź�� AddForce�� ���. AddForce(���� ���� ����, ��� Ÿ��)
        GranadePrfab.GetComponent<Rigidbody>().AddForce(spawnPosition[0].transform.forward * power * 40, ForceMode.Impulse);

        //�Ŀ� �ʱ�ȭ
        power = 0;
        //�Ŀ� �����̴� �ʱ�ȭ
        powerGageFront.rectTransform.sizeDelta = new Vector2(power, 25);

        curAmmo -= oneAmmo;
    }


}
