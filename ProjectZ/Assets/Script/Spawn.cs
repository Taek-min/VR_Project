using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public int indexNum;
    public bool isSpawn;
    public GameObject[] EnemyList;
    public Transform meObject;

    public Transform EnemyP;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("EnemySpawn", 1.0f, 25.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void EnemySpawn()
    {
        Debug.Log(indexNum);

        //if (!isSpawn) return;

        int EnemyNum = Random.Range(0, 9);
        Debug.Log("?ε???????" + indexNum);
        float posX = meObject.transform.position.x + Random.Range(-10f, 10f);  // X??ǥ ????
        Debug.Log("x????" + indexNum);
        float posZ = meObject.transform.position.z + Random.Range(-10f, 10f);  // Y??ǥ ????
        Debug.Log("y????" + indexNum);
        Vector3 createPos = new Vector3(posX, 0, posZ);
        Debug.Log("???? ????" + indexNum++);

        GameObject enemy = Instantiate(EnemyList[EnemyNum], createPos, Quaternion.identity, EnemyP);
        enemy.SetActive(true);
        Debug.Log("??????!!!!" + enemy.transform.localPosition);

    }

    void OnCollisionEnter(Collision other)
    {
            Debug.Log("??????");

        if (other.gameObject.tag == "Player")
        {
            isSpawn = true;
        }
    }
    void OnCollisionExit(Collision other)
    {
            Debug.Log("????");
        if (other.gameObject.tag == "Player")
        {

            isSpawn = false;
        }
    }
}
