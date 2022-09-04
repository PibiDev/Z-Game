using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour
{
    public float fireRate;
    public Transform firingPoint;
    public GameObject bulletPrefeb;

    float timeUntilFire;
    PlayerController pC;

    public float aim; // Left click
    public float aim2;// Right click
    private Animator aimAnimation;

    TextSwitch tS;
    AmmoText aT;
    bool isReloading = false;

    Inventory inventory;
    int totalAmmo;
    int ammoRemainder;

    public GameObject equipdSlot;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1;
        pC = gameObject.GetComponent<PlayerController>();
        aimAnimation = GetComponent<Animator>();
        tS = GameObject.Find("AmmoText").GetComponent<TextSwitch>();
        aT = GameObject.Find("AmmoText").GetComponent<AmmoText>();
        inventory = gameObject.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (equipdSlot.GetComponent<RevolverItem>()) //can use revolver functions only if is equiped
        {
            //Debug.Log("equipdSlot.GetComponent<RevolverItem>()");
            aim = Input.GetAxisRaw("Aim"); //(Right)click 1, left ctrl
            AimAnimation();

            aim2 = Input.GetAxisRaw("Shoot"); //(Left)click 0, space bar
            aimAnimation.SetBool("IsShooting", false); //Turn off IsShooting parameter on the animator (Aiming2 -> Shooting)

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

                for (int i = 0; i < inventory.bag.Count; i++)
                {
                    if (inventory.bag[i].GetComponent<RevolverAmmoItem>())
                    {
                        //Debug.Log("bullet = true");
                        StartCoroutine(Reload(i));
                        DeleteAmmoInventory(i);
                        break;
                    }
                }
            }

            if (aim > 0)
            {
                tS.enabled = true;
            }

            if (((aim2 > 0 && timeUntilFire < Time.time) && aim > 0) && aT.ammoAmount > 0)
            {//Player can shoot only if right mouse button is pressing and player
                Shoot();
                aT.ammoAmount--; //bullet counter -1
                timeUntilFire = Time.time + fireRate;
            }

            if (((aim2 > 0 && timeUntilFire < Time.time) && aim > 0) && aT.ammoAmount <= 0)
            {
                Debug.Log("out of ammo");
                // need sound efect
            }
        }

        if (equipdSlot.GetComponent<BateItem>()) //if bate item is equiped
        {
            aim = Input.GetAxisRaw("Aim"); //(Right)click 1, left ctrl
            AimAnimation();

            aim2 = Input.GetAxisRaw("Shoot"); //(Left)click 0, space bar
            aimAnimation.SetBool("IsShooting", false);

            if (((aim2 > 0 && timeUntilFire < Time.time) && aim > 0))
            {//Player can shoot only if right mouse button is pressing and player

                ShootingAnimation();

                timeUntilFire = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    { //Function wich set the shoot direction
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

    void ShootingAnimation()
    { //Animation for shooting the weapon
        bool aiming2 = aim2 != 0 ? true : false;
        aimAnimation.SetBool("IsShooting", aiming2); //Shoot the weapon
    }

    IEnumerator Reload(int id)
    {
        isReloading = true;
        Debug.Log("Reloadin");

        totalAmmo = aT.ammoAmount + inventory.bag[id].GetComponent<RevolverAmmoItem>().ammo; //6
        if (totalAmmo >= 6)
        {
            ammoRemainder = Mathf.Abs(6 - totalAmmo); //3
            aT.ammoAmount = totalAmmo - ammoRemainder; // 9-3
            yield return new WaitForSeconds(1);
            Debug.Log("Reloaded");
            isReloading = false;
        }

        else
        {
            Debug.Log("No >= 6");
            ammoRemainder = 0;
            aT.ammoAmount = totalAmmo;
            yield return new WaitForSeconds(1);
            Debug.Log("Reloaded");
            isReloading = false;
        }

    }

    void DeleteAmmoInventory(int id)
    {
        if (ammoRemainder == 0)
        {
            inventory.bag[id].GetComponent<Image>().sprite = null; //delete sprite
            inventory.bag[id].GetComponent<Image>().enabled = false; //desactivate image component
            inventory.bag[id].GetComponent<ItemUI>().SetQuantity("");
            Destroy(inventory.bag[id].GetComponent<RevolverAmmoItem>());//delete script
        }
        else
        {
            inventory.bag[id].GetComponent<RevolverAmmoItem>().ammo = ammoRemainder;

            inventory.bag[id].GetComponent<ItemUI>().SetQuantity(ammoRemainder.ToString());
        }
    }

}
