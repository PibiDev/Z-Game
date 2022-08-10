using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    AmmoText aT;
    SpriteRenderer sR;
    BoxCollider2D bC2D;
    Animator animator;
    int maxAmmo = 6; //revolver
    TakeItemText takeItemText;
    float take;
    bool canTake;

    void Awake()
    {
        aT = GameObject.Find("AmmoText").GetComponent<AmmoText>();
        sR = gameObject.GetComponent<SpriteRenderer>();
        bC2D = gameObject.GetComponent<BoxCollider2D>();
        animator = GameObject.Find("AmmoText").GetComponent<Animator>();
        takeItemText = GameObject.Find("Take Item").GetComponent<TakeItemText>();
    }

    void Start()
    {

    }

    void Update(){
        take = Input.GetAxisRaw("Interact"); // E

            if (canTake == true && take > 0) // If player press "E"
            {                
                if (aT.ammoAmount == maxAmmo)
                {
                    aT.text.enabled = true;
                    animator.SetBool("Fade", true);
                    StartCoroutine(Courtine2());
                }
                else
                {
                    aT.text.enabled = true;
                    animator.SetBool("Fade", true);
                    aT.ammoAmount += 1;
                    sR.enabled = false;
                    bC2D.enabled = false;
                    StartCoroutine(Courtine());
                }
            }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            takeItemText.text.enabled = true;

            canTake = true;
            //if (take > 0) // If player press "E"
            //{                
                // if (aT.ammoAmount == maxAmmo)
                // {
                //     aT.text.enabled = true;
                //     animator.SetBool("Fade", true);
                //     StartCoroutine(Courtine2());
                // }
                // else
                // {
                //     aT.text.enabled = true;
                //     animator.SetBool("Fade", true);
                //     aT.ammoAmount += 1;
                //     sR.enabled = false;
                //     bC2D.enabled = false;
                //     StartCoroutine(Courtine());
                // }
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D col){
        takeItemText.text.enabled = false;
        canTake = false;
    }

    IEnumerator Courtine()
    {
        yield return new WaitForSeconds(3);
        animator.SetBool("Fade", false);
        //aT.text.enabled = false;
        Destroy(gameObject);
    }

    IEnumerator Courtine2()
    {
        yield return new WaitForSeconds(3);
        animator.SetBool("Fade", false);
        //aT.text.enabled = false;
    }
}
