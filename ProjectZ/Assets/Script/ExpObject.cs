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
        if (other.tag == "ExpObject")
        {
            ExpObject expObj = other.gameObject.GetComponent<ExpObject>();
            if (expObj != null)
                ExpSystem.instance.CalcExp(expObj.expPoint);
            else
                Debug.Log("ExpObject�� �����ϴ�.");
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
