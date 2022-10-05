using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
������ GameObject�� ExpObject�̸� ����ġ ���
*/

public class LevelSystem : MonoBehaviour
{
    public int level;
    public int expMaxVal;
    public int expNowVal = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ExpObject")
        {
            ExpObject expObj = other.gameObject.GetComponent<ExpObject>();
            if (expObj != null)
                CalcExp(expObj.expPoint);
            else
                Debug.Log("ExpObject�� �����ϴ�.");
        }
    }

    private void CalcExp(int expPoint)
    {
        int expBuf = expNowVal + expPoint;
        if (expBuf >= expMaxVal)
        {   // ����ġ�� Max��ġ�� �Ѿ�� ������
            RiseLevel(expPoint);
        }
        else
        {   // ����ġ�� Max��ġ�� �̴��̸� ���� ����ġ ���
            RiseExp(expPoint);
        }
    }

    private void RiseExp(int expPoint)
    {   // ����ġ�� ����ϰ� UI�� �ݿ�
        expNowVal += expPoint;
        Debug.Log(expNowVal);
    }
    private void RiseLevel(int expPoint)
    {   // ������ ����ϰ� UI�� �ݿ�
        // �нú긦 1����Ʈ �ø� �� ����
        // ������ �ʿ��� �� ����ġ���� ��� ����
        int expBuf;

        expBuf = expNowVal + expPoint - expMaxVal;
        expMaxVal += expMaxVal / 2;

        level++;
        Debug.Log(level);
    }
}
