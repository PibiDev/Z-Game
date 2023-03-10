using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 1f;
    public float bulletDamage = 10f;
    public Rigidbody2D rb2d;

    private void FixedUpdate(){
        rb2d.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D coll){
        Destroy(gameObject);
    }

}
