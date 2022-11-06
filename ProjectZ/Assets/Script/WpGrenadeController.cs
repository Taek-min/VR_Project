using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WpGrenadeController : WeaponController
{
    //수류탄 슬라이더 상태
    public enum PowerMode
    {
        Up, Down
    }
    public PowerMode powerMode = PowerMode.Up;

    //수류탄 파워
    public float power;

    //파워 슬라이더
    public Slider powerSlider;
    //플레이어(수류탄 던지는 각도 계산)
    public GameObject player;
    //수류탄 프리팹
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

        //파워 상태
        switch (powerMode)
        {
            //파워가 올라갈 때
            case PowerMode.Up:
                {
                    
                    //Mathf.MoveTowards (A(power)에서 B(1.0f)까지 2.5f의 일정한 속도로 움직인다.)
                    power = Mathf.MoveTowards(power, 1.0f, 2.5f * Time.deltaTime);
                    if (power == 1.0f)
                    {
                        powerMode = PowerMode.Down;
                    }
                    break;
                }

            //파워가 내려갈 때
            case PowerMode.Down:
                {
                    //Mathf.MoveTowards (A(power)에서 B(1.0f)까지 2.5f의 일정한 속도로 움직인다.)
                    power = Mathf.MoveTowards(power, 0, 2.5f * Time.deltaTime);
                    if (power == 0)
                    {
                        powerMode = PowerMode.Up;
                    }
                    break;
                }
        }
        //파워슬라이더의 value값에 power를 대입받는다.
        powerSlider.value = power;
        //}
    }

    public void GrenadeShoot()
    {
        if (curAmmo < oneAmmo) return;

        //수류탄 생성 -> Instantiate(생성자) => (원본, 생성 위치, 생성 시 회전 값)
        GameObject GranadePrfab = Instantiate(GrenadePrefab, spawnPosition[0].position, transform.rotation);
        //GranadePrfab.AddComponent<Rigidbody>();
        //수류탄을 AddForce로 쏜다. AddForce(힘을 보낼 방향, 충격 타입)
        GranadePrfab.GetComponent<Rigidbody>().AddForce(spawnPosition[0].transform.forward * power * 40, ForceMode.Impulse);

        //파워 초기화
        power = 0;
        //파워 슬라이더 초기화
        powerSlider.value = power;

        curAmmo -= oneAmmo;
    }


}
