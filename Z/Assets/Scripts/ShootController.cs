using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour
{
    [HideInInspector] public float fireRate;
    public Transform firingPoint;
    public GameObject bulletPrefeb;

    float timeUntilFire;
    PlayerController pC;

    [HideInInspector] public float aim; // Left click
    [HideInInspector] public float aim2;// Right click
    private Animator aimAnimation;

    TextSwitch tS;
    AmmoText aT;
    bool isReloading = false;

    Inventory inventory;
    RevolverAmmoItem revolverAmmoItem;
    int totalAmmo;
    int ammoRemainder;
    
    [SerializeField]
    ItemUI itemUI;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1;
        pC = gameObject.GetComponent<PlayerController>();
        aimAnimation = GetComponent<Animator>();
        tS = GameObject.Find("AmmoText").GetComponent<TextSwitch>();
        aT = GameObject.Find("AmmoText").GetComponent<AmmoText>();
        inventory = gameObject.GetComponent<Inventory>();
        revolverAmmoItem = GameObject.Find("Revolver Ammo").GetComponent<RevolverAmmoItem>();
    }

    // Update is called once per frame
    void Update()
    {
        aim = Input.GetAxisRaw("Aim"); //(Right)click 1, left ctrl
        AimAnimation();

        aim2 = Input.GetAxisRaw("Shoot"); //(Left)click 0, space bar
        aimAnimation.SetBool("IsShooting", false); //Turn off IsShooting parameter on the animator (Aiming2 -> Shooting)
        
        //reload = Input.GetKeyDown(KeyCode.R); // R

        //isReloading = false;

        if (aT.ammoAmount == 6 && Input.GetKeyDown(KeyCode.R))
        {
                Debug.Log("can't reload");
                return;
        }

        if (aT.ammoAmount < 6 && Input.GetKeyDown(KeyCode.R)) // Reload "R"
        {
            Debug.Log("R");

            if (isReloading)
            {
                Debug.Log("isReloading return");
                return;
            }

            for (int i = 0; i < inventory.bag.Count; i++){
                if (inventory.bag[i].GetComponent<RevolverAmmoItem>())
                {
                    Debug.Log("bullet = true");
                    //isReloading = true;
                    StartCoroutine(Reload(i));
                    //aT.ammoAmount += revolverAmmoItem.ammo;
                    //delete bullet item from inventory
                    DeleteAmmoInventory(i);
                    //inventory.bag[i].GetComponent<Image>().enabled = false;
                    //inventory.bag[i].GetComponent<Image>().sprite = null;
                    //Destroy(inventory.bag[i].GetComponent<RevolverAmmoItem>());
                    //End of reloading
                    //isReloading = false;
                    //return;       
                    break;
                }
            }
        }

        if (aim > 0){
            tS.enabled = true;
        }

        if(((aim2 > 0 && timeUntilFire < Time.time) && aim > 0) && aT.ammoAmount > 0){//Player can shoot only if right mouse button is pressing and player
            Shoot();
            aT.ammoAmount--; //bullet counter -1
            timeUntilFire = Time.time + fireRate;
        }

        if(((aim2 > 0 && timeUntilFire < Time.time) && aim > 0) && aT.ammoAmount <= 0){
            Debug.Log("out of ammo");
            // need sound efect
        }

        
    }

    void Shoot(){ //Function wich set the shoot direction
        float angle = pC.isFacingRight ? 0f : 180f;
        Instantiate(bulletPrefeb, firingPoint.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));
        ShootingAnimation();
    }

    void AimAnimation() //Animation for aiming
    {
        bool aiming = aim != 0 ? true : false;
        aimAnimation.SetBool("Aiming", aiming);//Up the weapon
        aimAnimation.SetBool("IsAiming", aiming);//Hold the weapon
        
    }

    void ShootingAnimation(){ //Animation for shooting the weapon
        bool aiming2 = aim2 !=0 ? true : false;
        aimAnimation.SetBool("IsShooting", aiming2); //Shoot the weapon
    }

    IEnumerator Reload(int id){
        isReloading = true;
        Debug.Log("Reloadin");
        //add reloaging animation
        //aT.ammoAmount += revolverAmmoItem.ammo;
        //for (int i = 0; i < inventory.bag.Count; i++) //search for ammo in the inventory
        //{
            //Debug.Log("searching ammo");
           // if (inventory.bag[id].GetComponent<RevolverAmmoItem>()) //if there is ammo in the inventory then:
            //{
                //Debug.Log("ammo true");
                totalAmmo = aT.ammoAmount + inventory.bag[id].GetComponent<RevolverAmmoItem>().ammo; //6
                if (totalAmmo >= 6)
                {
                    ammoRemainder = Mathf.Abs(6 - totalAmmo); //3
                    //Debug.Log(Mathf.Abs(ammoRemainder));
                    aT.ammoAmount = totalAmmo - ammoRemainder; // 9-3
                    yield return new WaitForSeconds(1);
                    Debug.Log("Reloaded");
                    isReloading = false;
                }
            //}
        //} 
        else{
            Debug.Log("No >= 6");
            ammoRemainder = 0;
            aT.ammoAmount = totalAmmo;
            yield return new WaitForSeconds(1);
            Debug.Log("Reloaded");
            isReloading = false;
        }

    }

    void DeleteAmmoInventory(int id){
        if (ammoRemainder == 0)
        {
            inventory.bag[id].GetComponent<Image>().enabled = false;
            inventory.bag[id].GetComponent<Image>().sprite = null;
            itemUI.SetQuantity("");
            Destroy(inventory.bag[id].GetComponent<RevolverAmmoItem>());
        }
        else{
            inventory.bag[id].GetComponent<RevolverAmmoItem>().ammo = ammoRemainder;
            
            inventory.bag[id].GetComponent<ItemUI>().SetQuantity(ammoRemainder.ToString());
            //return;

        }
    }
}
