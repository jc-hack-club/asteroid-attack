using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{

    public GameObject Bullet;
    Vector2 bulletPos;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            fire();
            GetComponent<Sound>().bulletSource.Play();
        }
    }

    void fire() {
        bulletPos = transform.position;
        Instantiate(Bullet, bulletPos, transform.rotation);
    }
}
