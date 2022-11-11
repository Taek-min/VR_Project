using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;//B42 공전구현
    public float orbitSpeed;//B42 공전구현
    Vector3 offSet;//B42 공전구현

    void Start()
    {
        offSet = transform.position - target.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offSet;
        transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);
        offSet = transform.position - target.position;

    }
}
