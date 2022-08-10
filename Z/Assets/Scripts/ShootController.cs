using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1;
        pC = gameObject.GetComponent<PlayerController>();
        aimAnimation = GetComponent<Animator>();
        tS = GameObject.Find("AmmoText").GetComponent<TextSwitch>();
        aT = GameObject.Find("AmmoText").GetComponent<AmmoText>();
    }

    // Update is called once per frame
    void Update()
    {
        aim = Input.GetAxisRaw("Aim"); //(Right)click 1, left ctrl
        AimAnimation();

        aim2 = Input.GetAxisRaw("Shoot"); //(Left)click 0, space bar
        aimAnimation.SetBool("IsShooting", false); //Turn off IsShooting parameter on the animator (Aiming2 -> Shooting)
        
        if (aim > 0){
            tS.enabled = true;
        }

        if(((aim2 > 0 && timeUntilFire < Time.time) && aim > 0) && aT.ammoAmount > 0){//Player can shoot only if right mouse button is pressing and player
            Shoot();
            aT.ammoAmount--;
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
}
