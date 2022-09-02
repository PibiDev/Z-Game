using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSwitch : MonoBehaviour
{
    AmmoText aT;
    TextSwitch tS;
    public ShootController sC;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //sC = GameObject.Find("Player").GetComponent<ShootController>();
        aT = gameObject.GetComponent<AmmoText>();
        tS = gameObject.GetComponent<TextSwitch>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(sC.aim > 0){
            aT.text.enabled = true; //ammo
            animator.SetBool("Fade", true);
        }
        else
        {
            animator.SetBool("Fade", false);
            //aT.text.enabled = false; //ammo
            tS.enabled = false;
        }

    }

}
