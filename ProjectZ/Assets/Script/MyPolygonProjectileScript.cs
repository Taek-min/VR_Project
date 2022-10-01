using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolygonArsenal;

public class MyPolygonProjectileScript : PolygonProjectileScript
{
    public GameObject rangeOfAttack;
    public float DestroyTime = 5.0f;
    public bool hasDestroyTime = false;

    protected override void Start()
    {
        base.Start();
        if (hasDestroyTime) Destroy(gameObject, DestroyTime);
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        float rad;
        if (transform.GetComponent<SphereCollider>())
            rad = transform.GetComponent<SphereCollider>().radius;
        else
            rad = colliderRadius;

        Vector3 dir = transform.GetComponent<Rigidbody>().velocity;
        if (transform.GetComponent<Rigidbody>().useGravity)
            dir += Physics.gravity * Time.deltaTime;
        dir = dir.normalized;

        float dist = transform.GetComponent<Rigidbody>().velocity.magnitude * Time.deltaTime;

        if (Physics.SphereCast(transform.position, rad, dir, out hit, dist))
        {
            transform.position = hit.point + (hit.normal * collideOffset);

            GameObject impactP = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
            //
            if (rangeOfAttack)
            {
                GameObject roa = Instantiate(rangeOfAttack, transform.position, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
                Destroy(roa, 0.2f);
            }

            if (hit.transform.tag == "Destructible") // Projectile will destroy objects tagged as Destructible
            {
                Destroy(hit.transform.gameObject);
            }

            foreach (GameObject trail in trailParticles)
            {
                GameObject curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
                curTrail.transform.parent = null;
                Destroy(curTrail, 3f);
            }
            Destroy(projectileParticle, 3f);
            Destroy(impactP, 5.0f);
            Destroy(gameObject);

            ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
            //Component at [0] is that of the parent i.e. this object (if there is any)
            for (int i = 1; i < trails.Length; i++)
            {

                ParticleSystem trail = trails[i];

                if (trail.gameObject.name.Contains("Trail"))
                {
                    trail.transform.SetParent(null);
                    Destroy(trail.gameObject, 2f);
                }
            }
        }

    }
}
