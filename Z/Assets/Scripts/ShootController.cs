using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour
{
    float fireRate;
    float HitRate;
    public Transform firingPoint;
    public GameObject bulletPrefeb;

    public float timeUntilFire;
    PlayerController pC;

    public float aim; // Left click
    public float aim2;// Right click
    public Animator aimAnimation;

    TextSwitch tS;
    AmmoText aT;
    bool isReloading = false;

    Inventory inventory;
    int totalAmmo;
    int ammoRemainder;

    public GameObject equipdSlot;

    public bool isAiming = false;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 0.65f;
        HitRate = 0.97f; //37 frames bate
        pC = gameObject.GetComponent<PlayerController>();
        aimAnimation = GetComponent<Animator>();
        tS = GameObject.Find("AmmoText").GetComponent<TextSwitch>();
        aT = GameObject.Find("AmmoText").GetComponent<AmmoText>();
        inventory = gameObject.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        aim = Input.GetAxisRaw("Aim"); //(Right)click 1, left ctrl
        aim2 = Input.GetAxisRaw("Shoot"); //(Left)click 0, space bar

        if (equipdSlot.GetComponent<RevolverItem>()) //can use revolver functions only if is equiped
        {
            //Debug.Log("equipdSlot.GetComponent<RevolverItem>()");
            AimAnimation();

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
                if (isAiming)
                {
                    return;
                }
                aT.ammoAmount--; //bullet counter -1
                timeUntilFire = Time.time + fireRate;
                Shoot();
            }

            if (((aim2 > 0 && timeUntilFire < Time.time) && aim > 0) && aT.ammoAmount <= 0)
            {
                Debug.Log("out of ammo");
                // need sound efect
            }
        }

        //primero se tiene que mover antes que atacar
        if (equipdSlot.GetComponent<BateItem>()) //if bate item is equiped
        {

            AimAnimation();

            aimAnimation.SetBool("IsShooting", false);

            if (((aim2 > 0 && timeUntilFire < Time.time) && aim > 0))
            {//Player can shoot only if right mouse button is pressing and player

                timeUntilFire = Time.time + HitRate;
                ShootingAnimation();

            }
        }
    }

    void Shoot()
    { //Function wich set the shoot direction
        float angle = pC.isFacingRight ? 0f : 180f;
        ShootingAnimation();
        Instantiate(bulletPrefeb, firingPoint.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));
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
            yield return new WaitForSeconds(1);
            aT.ammoAmount = totalAmmo - ammoRemainder; // 9-3
            Debug.Log("Reloaded");
            isReloading = false;
        }

        else
        {
            //Debug.Log("No >= 6");
            ammoRemainder = 0;
            yield return new WaitForSeconds(1);
            aT.ammoAmount = totalAmmo;
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
