using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float BulletTimer = 5;
    public bool isDestroy;
    public bool isMelee;
    public bool isRock;

    public int Fire;
    public GameObject eventsys;

    private void OnCollisionEnter(Collision collision)
    {
        if(!isRock && collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 3);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(!isMelee && other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        UI UI = eventsys.GetComponent<UI>();
        damage = 20;
        Fire = 5 + UI.Adddamage;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(isDestroy)
            Invoke("Des", BulletTimer);

    }

    // Update is called once per frame
    void Update()
    {

        UI UI = eventsys.GetComponent<UI>();
        Fire = 1 + UI.Adddamage;
    }

    void Des()
    {
        Destroy(gameObject);
    }
}
