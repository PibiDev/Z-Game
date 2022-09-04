using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{

    public AnimatorOverrideController revolver;
    public AnimatorOverrideController bate;
    public AnimatorOverrideController withouthWeapon;

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Revolver(){
        GetComponent<Animator>().runtimeAnimatorController = revolver as RuntimeAnimatorController;
        if (playerController.isFacingRight == false)
        {
            playerController.IdleAnimation(Vector2.left);
            
        }
    }

    public void Bate(){
        GetComponent<Animator>().runtimeAnimatorController = bate as RuntimeAnimatorController;
        if (playerController.isFacingRight == false)
        {
            playerController.IdleAnimation(Vector2.left);
            
        }
    }

    public void WithoutWeapon(){
        GetComponent<Animator>().runtimeAnimatorController = withouthWeapon as RuntimeAnimatorController;
        if (playerController.isFacingRight == false)
        {
            playerController.IdleAnimation(Vector2.left);
            
        }
    }
}
