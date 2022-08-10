using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Character attributes
    public float moveHorizontal;
    [HideInInspector] public bool isFacingRight = true;
    [HideInInspector] public Vector2 direccion;
    public float run;

    //References
    private Animator animator;
    Transform fP; //FiringPoint
    Transform lantern; //Point light
    ShootController sC; //ShootController

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sC = gameObject.GetComponent<ShootController>();

        //FiringPoint
        fP = transform.Find("FiringPoint");
        fP.localPosition = new Vector3(0.1f, 0.06f, 0); //FiringPoint initial position

        lantern = transform.Find("Lantern");
        lantern.localPosition = new Vector3(0.1f, 0.06f, -0.201f);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void ProcessInputs() //Function wich detect if the player are moving
    {
        animator.SetBool("IsRunning", false);
        direccion = Vector2.zero;
        run = Input.GetAxisRaw("Run");
        moveHorizontal = Input.GetAxisRaw("Horizontal"); //a,d,->,<-
        TurnSide();
    }

    void TurnSide() //Function wich detect where is the player view
    {
        if (sC.aim == 0)
        { //if player is not aiming
            if (moveHorizontal > 0.1f)//Right = +
            {
                animator.SetBool("IsRunning", false);
                direccion += Vector2.right;
                transform.position += transform.right * (Time.deltaTime * 0.25f);
                fP.localPosition = new Vector3(0.1f, 0.06f, 0);//FiringPoint turn right
                lantern.localPosition = new Vector3(0.1f, 0.06f, -0.201f); // lantern
                isFacingRight = true;
            }
            if (moveHorizontal < -0.1f)//Left = -
            {
                animator.SetBool("IsRunning", false);
                direccion += Vector2.left;
                transform.position -= transform.right * (Time.deltaTime * 0.25f);
                fP.localPosition = new Vector3(-0.1f, 0.06f, 0);//FiringPoint turn left
                lantern.localPosition = new Vector3(-0.1f, 0.06f, -0.201f); //lantern
                isFacingRight = false;
            }
            if (run > 0 && moveHorizontal > 0.1f) //Run z,r
            {
                animator.SetBool("IsRunning", true);
                direccion += Vector2.right;
                transform.position += transform.right * (Time.deltaTime * 0.30f);
                fP.localPosition = new Vector3(0.1f, 0.06f, 0);//FiringPoint turn right
                lantern.localPosition = new Vector3(0.1f, 0.06f, -0.201f); // lantern
                isFacingRight = true;
            }
            if (run > 0 && moveHorizontal < -0.1f)
            {
                animator.SetBool("IsRunning", true);
                direccion += Vector2.left;
                transform.position -= transform.right * (Time.deltaTime * 0.30f);
                fP.localPosition = new Vector3(-0.1f, 0.06f, 0);//FiringPoint turn left
                lantern.localPosition = new Vector3(-0.1f, 0.06f, -0.201f); //lantern
                isFacingRight = false;
            }
            Direccion();
        }
        if (sC.aim > 0)
        {
            animator.SetLayerWeight(1, 0);
            if (moveHorizontal > 0.1f)//Right = +
            {
                direccion += Vector2.right;
                fP.localPosition = new Vector3(0.1f, 0.06f, 0);//FiringPoint turn right
                lantern.localPosition = new Vector3(0.1f, 0.06f, -0.201f); // lantern

                isFacingRight = true;
            }
            if (moveHorizontal < -0.1f)//Left = -
            {
                direccion += Vector2.left;
                fP.localPosition = new Vector3(-0.1f, 0.06f, 0);//FiringPoint turn left
                lantern.localPosition = new Vector3(-0.1f, 0.06f, -0.201f); //lantern

                isFacingRight = false;
            }
            AimingDireccion();
        }
    }

    public void MoveAnimation(Vector2 direccion)
    {
        animator.SetLayerWeight(1, 1); //Transition to Walking layer
        animator.SetFloat("x", direccion.x); //Transition to left or right on the animator
    }

    public void AimingAnimation(Vector2 direccion)
    {
        animator.SetLayerWeight(0, 1); //Transition to base layer
        animator.SetFloat("x", direccion.x); //Transition to left or right on the animator
    }

    void Direccion()
    {
        if (direccion.x != 0)
        {
            MoveAnimation(direccion);
        }
        else
        {
            animator.SetLayerWeight(1, 0); //Return to Base Layer
        }
    }

    void AimingDireccion()
    {
        if (direccion.x != 0)
        {
            AimingAnimation(direccion);
        }
        else
        {
            animator.SetLayerWeight(0, 1); //Return to Base Layer
        }
    }

}
