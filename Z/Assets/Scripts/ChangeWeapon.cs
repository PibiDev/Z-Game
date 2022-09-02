using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{

    public AnimatorOverrideController revolver;
    public AnimatorOverrideController bate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Revolver(){
        GetComponent<Animator>().runtimeAnimatorController = revolver as RuntimeAnimatorController;
    }

    public void Bate(){
        GetComponent<Animator>().runtimeAnimatorController = bate as RuntimeAnimatorController;
    }
}
