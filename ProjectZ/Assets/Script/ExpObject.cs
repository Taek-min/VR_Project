using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
경험치 수치
경험치 모델링

경험치 상승
 */
public class ExpObject : MonoBehaviour
{
    public int expPoint;
    public Renderer expRen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ExpSystem.instance.CalcExp(expPoint);
            Destroy(gameObject);
        }
    }
}
