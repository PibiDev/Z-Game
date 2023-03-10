using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1Controller : MonoBehaviour
{
    public GameObject zombie;
    public int hP = 3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hP <= 0)
        {
            Destroy(zombie);
        }
        
    }

    void OnTriggerEnter2D(Collider2D coll){
        if (coll.CompareTag("Bullet"))
        {
            hP--;
        }
        if (coll.CompareTag("Hit"))
        {
            hP = hP-1;
            
        }
        
    }
}
