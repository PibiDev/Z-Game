using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Character attributes
    public float moveHorizontal;

    //References
    private Animator walkingAnimation;

    [HideInInspector] public bool isFacingRight = true;

    [HideInInspector] public Vector2 direccion;

    Transform fP; //FiringPoint

    Transform lantern;

    ShootController sC;

    // Start is called before the first frame update
    void Start()
    {
        walkingAnimation = GetComponent<Animator>();
        sC = gameObject.GetComponent<ShootController>();

        //FiringPoint
        fP = transform.Find("FiringPoint");
        fP.localPosition = new Vector3(0.1f,0.06f,0); //FiringPoint initial position

        lantern = transform.Find("Lantern");
        lantern.localPosition = new Vector3(0.1f,0.06f,-0.201f);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void ProcessInputs() //Function wich detect if the player are moving
    {
        direccion = Vector2.zero;
        moveHorizontal = Input.GetAxisRaw("Horizontal"); //a,d,->,<-
        TurnSide();
        //Direccion();
    }

    void TurnSide() //Function wich detect where is the player view
    {
        if(sC.aim == 0){
            if (moveHorizontal > 0.1f)//Right = +
            {
            direccion += Vector2.right;
            transform.position += transform.right * (Time.deltaTime * 0.25f);
            fP.localPosition = new Vector3(0.1f,0.06f,0);//FiringPoint turn right
            lantern.localPosition = new Vector3(0.1f,0.06f,-0.201f); // lantern
            isFacingRight = true;
            }
            if (moveHorizontal < -0.1f)//Left = -
            {
            direccion += Vector2.left;
            transform.position -= transform.right * (Time.deltaTime * 0.25f);
            fP.localPosition = new Vector3(-0.1f,0.06f,0);//FiringPoint turn left
            lantern.localPosition = new Vector3(-0.1f,0.06f,-0.201f); //lantern
            isFacingRight = false;
            }
            Direccion();
        }
        if(sC.aim > 0){
            if (moveHorizontal > 0.1f)//Right = +
            {
            direccion += Vector2.right;
            fP.localPosition = new Vector3(0.1f,0.06f,0);//FiringPoint turn right
            lantern.localPosition = new Vector3(0.1f,0.06f,-0.201f); // lantern
            
            isFacingRight = true;
            }
            if (moveHorizontal < -0.1f)//Left = -
            {
            direccion += Vector2.left;
            fP.localPosition = new Vector3(-0.1f,0.06f,0);//FiringPoint turn left
            lantern.localPosition = new Vector3(-0.1f,0.06f,-0.201f); //lantern
            
            isFacingRight = false;
            }
            AimingDireccion();
        }
    }

    void Walking()//Player movement
    {
        transform.position += transform.right * (Time.deltaTime * 0.25f);
    }

    public void MoveAnimation(Vector2 direccion){ //Animation and transition
        walkingAnimation.SetLayerWeight(1, 1); //Transition to Walking layer
        walkingAnimation.SetFloat("x", direccion.x); //Transition to left or right on the animator
    }

    public void AimingAnimation(Vector2 direccion){ //Animation and transition
        walkingAnimation.SetLayerWeight(1, 0); //Transition to Walking layer
        walkingAnimation.SetFloat("x", direccion.x); //Transition to left or right on the animator
    }

    void Direccion(){
        if(direccion.x!=0){
            MoveAnimation(direccion);
        } else {
            walkingAnimation.SetLayerWeight(1,0); //Return to Base Layer
        }
    }

    void AimingDireccion(){
        if(direccion.x!=0){
            AimingAnimation(direccion);
        } else {
            walkingAnimation.SetLayerWeight(1,0); //Return to Base Layer
        }
    }

}
