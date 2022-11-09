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
    public UI ui;

    public int level;           // ���� ����
    public int expMaxVal;       // �ִ� ����ġ��
    public int expNowVal = 0;   // ���� ����ġ��
    public bool isMaxLv = false;
    public GameObject expFactory;
    public GameObject BossGmObj;

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
        if (isMaxLv)
        {
            ui.expGageImage.rectTransform.sizeDelta = new Vector2(600, 50);
            return false;
        }
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
        if(isMaxLv)
        {
            ui.expGageImage.rectTransform.sizeDelta = new Vector2(600, 50);
            return;
        }
        expNowVal += expPoint;
        Debug.Log(expNowVal.ToString() + " " + expMaxVal.ToString());
        testText[Exp].text = expNowVal.ToString() + ", " + expMaxVal.ToString();
        float expPersent = (float)expNowVal / (float)expMaxVal;
        ui.expGageImage.rectTransform.sizeDelta = new Vector2(600*expPersent, 50);
        Debug.Log("expPersent: " + expPersent.ToString());
    }
    private void RiseLevel(int expPoint)
    {   // ������ ����ϰ� UI�� �ݿ�
        // �нú긦 1����Ʈ �ø� �� ����
        // ������ �ʿ��� �� ����ġ���� ��� ����
        if(isMaxLv)
        {
            ui.expGageImage.rectTransform.sizeDelta = new Vector2(600, 50);
            return;
        }
        int expBuf = expPoint;
        //9 10 9 8 15 
        ui.LvValImageList[level].SetActive(false);
        expBuf = expNowVal + expBuf;
        while(expBuf >= expMaxVal && !(level >= 10))
        {
            expBuf = expBuf - expMaxVal;
            expMaxVal += expMaxVal / 2;
            expNowVal = 0;
            level++;
            ui.On_LevelUp();
        }
        //������ �ϸ� UI�� ������ ���

        RiseExp(expBuf);

        if (level >= 10)
        {
            isMaxLv = true;
            ui.LvValImageList[10].SetActive(true);
            ui.expGageImage.rectTransform.sizeDelta = new Vector2(600, 50);
            ui.BossHpUI.SetActive(true);
            BossGmObj.SetActive(true);
            return;
        }
        else
        {
            ui.LvValImageList[level].SetActive(true);
        }

        Debug.Log(level);
        testText[Level].text = level.ToString();


        //������ 10�� �Ǹ� ���� ����
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
