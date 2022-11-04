using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
������ GameObject�� ExpObject�̸� ����ġ ���
*/

public class ExpSystem : MonoBehaviour
{
    // �̱���
    public static ExpSystem instance;

    public int level;           // ���� ����
    public int expMaxVal;       // �ִ� ����ġ��
    public int expNowVal = 0;   // ���� ����ġ��
    public GameObject expFactory;

    public Text[] testText;

    public const int Level = 0;
    public const int Exp = 1;
    public const int Log = 2;

    private void Awake()
    {
        // �̱��� ����
        if (instance == null)
            instance = this;
    }

    public bool CalcExp(int expPoint)
    {
        int expBuf = expNowVal + expPoint;
        if (expBuf < expMaxVal)
        {   // ����ġ�� Max��ġ�� �̴��̸� ���� ����ġ ���
            RiseExp(expPoint);
            return true;
        }
        else
        {   // ����ġ�� Max��ġ�� �Ѿ�� ������
            RiseLevel(expPoint);
            return false;
        }
    }

    private void RiseExp(int expPoint)
    {   // ����ġ�� ����ϰ� UI�� �ݿ�
        expNowVal += expPoint;
        Debug.Log(expNowVal);
        testText[Exp].text = expNowVal.ToString() + ", " + expMaxVal.ToString();
    }
    private void RiseLevel(int expPoint)
    {   // ������ ����ϰ� UI�� �ݿ�
        // �нú긦 1����Ʈ �ø� �� ����
        // ������ �ʿ��� �� ����ġ���� ��� ����
        int expBuf;

        expBuf = expNowVal + expPoint - expMaxVal;
        expMaxVal += expMaxVal / 2;
        expNowVal = 0;
        level++;

        RiseExp(expBuf);

        Debug.Log(level);
        testText[Level].text = level.ToString();

    }

    public void CreateExp(int expPoint, Vector3 enemyPos)
    {
        //1. ExpObject�� �����Ѵ�.
        GameObject exp = Instantiate(expFactory);
        //2. enemyVec�� ��ġ��Ų��.
        exp.transform.position = new Vector3(enemyPos.x, 2, enemyPos.z);
        //3. expPoint�� �Է��Ѵ�.
        exp.GetComponent<ExpObject>().expPoint = expPoint;
    }
}
