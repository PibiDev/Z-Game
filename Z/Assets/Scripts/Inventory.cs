using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> bag = new List<GameObject>(); // List to store items
    public GameObject inv;
    public bool activate_inv;
    PlayerController playerController;
    ShootController shootController;

    TakeItemText takeItemText;
    float take;
    new Collider2D collider2D;
    bool canTake;

    public GameObject selector;
    public int ID;

    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        shootController = gameObject.GetComponent<ShootController>();
        takeItemText = GameObject.Find("Take Item").GetComponent<TakeItemText>();
         
    }

    // Update is called once per frame
    void Update()
    {

        Navegar();

        take = Input.GetAxisRaw("Interact"); // E

            if (canTake == true && take > 0)
            {
                Destroy(collider2D.gameObject);
                
                for (int i = 0; i < bag.Count; i++)
                {
                    if (bag[i].GetComponent<Image>().enabled == false)
                    {
                        bag[i].GetComponent<Image>().enabled = true;
                        bag[i].GetComponent<Image>().sprite = collider2D.GetComponent<SpriteRenderer>().sprite;
                        break;
                    }
                }

            }

        if (activate_inv)
        {
            //open backpack sound efect
            playerController.enabled = false;
            shootController.enabled = false;

            inv.SetActive(true);
        }
        else
        {
            //close backpack sound efect
            inv.SetActive(false);

            playerController.enabled = true;
            shootController.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            activate_inv = !activate_inv;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Item"))
        {

            takeItemText.text.enabled = true;

            canTake = true;

            collider2D = coll;


        }
    }

    void OnTriggerExit2D(Collider2D coll){
        takeItemText.text.enabled = false;
        canTake = false;
    }

    void Navegar(){
        if (Input.GetAxisRaw("Horizontal") > 0 && ID<bag.Count-1) //right
        {
            ID++;
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && ID > 0) //right
        {
            ID--;
        }
        if (Input.GetAxisRaw("Vertical") > 0 && ID > 2) //right
        {
            ID -= 3;
        }
        if (Input.GetAxisRaw("Vertical") < 0 && ID < 6) //right
        {
            ID += 3;
        }
        selector.transform.position = bag[ID].transform.position;
    }
}
