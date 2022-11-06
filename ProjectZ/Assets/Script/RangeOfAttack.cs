using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeOfAttack : MonoBehaviour
{ 
    //public Collider thisCollider;
    public Bullet bullet;
    public float minSize = 1;
    public float maxSize = 1;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1 / bullet.BulletTimer;
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.localScale.x + speed * maxSize * Time.deltaTime;
        float y = transform.localScale.y + speed * maxSize * Time.deltaTime;
        float z = transform.localScale.z + speed * maxSize * Time.deltaTime;
        //float x = transform.localScale.x * 20 / 3 * maxSize * Time.deltaTime;
        //float y = transform.localScale.y * 20 / 3 * maxSize * Time.deltaTime;
        //float z = transform.localScale.z * 20 / 3 * maxSize * Time.deltaTime;
        Vector3 vec = new Vector3(x, y, z);
        transform.localScale = vec;
    }
}
