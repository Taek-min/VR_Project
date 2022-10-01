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
    public Slider powerSlider;
    //�÷��̾�(����ź ������ ���� ���)
    public GameObject player;
    //����ź ������
    public GameObject GrenadePrefab;


    protected override void Start() { base.Start();  }
    protected override void Update(){ base.Update(); }
    protected void OnEnable()   { powerSlider.value = 0; power = 0; powerSlider.gameObject.SetActive(false); }
    protected void OnDisable()  { powerSlider.value = 0; power = 0; }

    public override void Attack(bool shootDown, bool shootUp)
    {
        if (shootDown && !shootUp)
        {
            powerSlider.gameObject.SetActive(true);
            GrenadeCharge();
        }
        else if (shootUp && !shootDown)
        {
            powerSlider.gameObject.SetActive(false);
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
        powerSlider.value = power;
        //}
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
        powerSlider.value = power;

        curAmmo -= oneAmmo;
    }


}
