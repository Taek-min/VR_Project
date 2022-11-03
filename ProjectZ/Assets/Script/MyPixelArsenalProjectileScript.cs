using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelArsenal;

public class MyPixelArsenalProjectileScript : PixelArsenalProjectileScript
{
    public string[] layerMaskList;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        int layerMask = LayerMask.GetMask(layerMaskList);
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity); // Sets rotation to look at direction of movement
        }

        RaycastHit hit;

        float radius; // Sets the radius of the collision detection
        if (transform.GetComponent<SphereCollider>())
            radius = transform.GetComponent<SphereCollider>().radius;
        else
            radius = colliderRadius;

        Vector3 direction = transform.GetComponent<Rigidbody>().velocity; // Gets the direction of the projectile, used for collision detection
        if (transform.GetComponent<Rigidbody>().useGravity)
            direction += Physics.gravity * Time.deltaTime; // Accounts for gravity if enabled
        direction = direction.normalized;

        float detectionDistance = transform.GetComponent<Rigidbody>().velocity.magnitude * Time.deltaTime; // Distance of collision detection for this frame

        if (Physics.SphereCast(transform.position, radius, direction, out hit, detectionDistance, LayerMask.GetMask(layerMaskList))) // Checks if collision will happen
        {
            if (hit.transform.gameObject.CompareTag("EnemyBullet"))
            {
                //EnemyBullet과는 충돌하지 않음
                return;
            }
            transform.position = hit.point + (hit.normal * collideOffset); // Move projectile to point of collision

            GameObject impactP = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject; // Spawns impact effect

            ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>(); // Gets a list of particle systems, as we need to detach the trails
                                                                                 //Component at [0] is that of the parent i.e. this object (if there is any)
            for (int i = 1; i < trails.Length; i++) // Loop to cycle through found particle systems
            {
                ParticleSystem trail = trails[i];

                if (trail.gameObject.name.Contains("Trail"))
                {
                    trail.transform.SetParent(null); // Detaches the trail from the projectile
                    Destroy(trail.gameObject, 2f); // Removes the trail after seconds
                }
            }

            Destroy(projectileParticle, 3f); // Removes particle effect after delay
            Destroy(impactP, 3.5f); // Removes impact effect after delay
            Destroy(gameObject); // Removes the projectile
        }
    }
}
