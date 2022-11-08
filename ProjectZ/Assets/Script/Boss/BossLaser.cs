using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossLaser : Bullet
{
    Rigidbody rigid;
    bool isShoot; // 기를 모으고 쏘는 타이밍을 관리할 bool변수 추가 

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
    }

    IEnumerator GainPowerTimer() // 쏘는 타이밍을 관리할 코루틴 생성 
    {
        yield return new WaitForSeconds(2.2f); // 2.2초가 지나면 
        isShoot = true; // 쏴라!
    }

    IEnumerator GainPower()
    {
        while (!isShoot) // isShoot가 아닐때 까지는 반복 
        {
            rigid.velocity = transform.forward * 20;
            yield return null; // While문에는 yield return null 포함 !!
        }

    }
}
