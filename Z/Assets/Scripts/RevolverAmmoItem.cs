using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverAmmoItem : MonoBehaviour
{
    public string title;
    public string description;
    public int ammo = 1;

    // Start is called before the first frame update
    void Start()
    {  
        title = "Cartridge";
        description = "A cartridge or a round is a type of pre-assembled firearm ammunition packaging a projectile.";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
