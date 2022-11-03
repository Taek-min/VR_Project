using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
����ġ ��ġ
����ġ �𵨸�

����ġ ���
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
