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

    AmmoText ammoText;

    [SerializeField]
    ItemDescription itemDescription;
    string title;
    string description;
    Sprite iSprite;

    void Awake(){
        //itemDescription.ResetDescription();
    }

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

        //Navegar();

        take = Input.GetAxisRaw("Interact"); // E

            if (canTake == true && take > 0)
            {
                Destroy(collider2D.gameObject);
                
                for (int i = 0; i < bag.Count; i++)
                {
                    //if is ammo and ammo <= 5 then: ammo += new ammo, if result of ammo += new ammo == ammo < 6 then: get the diference and create other ammo slot with the Remainder 
                    if (bag[i].GetComponent<Image>().enabled == false)
                    {
                        bag[i].GetComponent<Image>().enabled = true;
                        bag[i].GetComponent<Image>().sprite = collider2D.GetComponent<SpriteRenderer>().sprite;
                        //bag[i].AddComponent<Text>().text = collider2D.GetComponent<Text>().text;
                        if (collider2D.GetComponent<RevolverAmmoItem>()) // if is a revolver ammo item then add the script component
                        {
                            bag[i].AddComponent<RevolverAmmoItem>().ammo = collider2D.GetComponent<RevolverAmmoItem>().ammo;
                            bag[i].GetComponent<RevolverAmmoItem>().title = collider2D.GetComponent<RevolverAmmoItem>().title;
                            bag[i].GetComponent<RevolverAmmoItem>().description = collider2D.GetComponent<RevolverAmmoItem>().description;
                        }
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

            itemDescription.ResetDescription();

            Navegar();

            if (bag[ID].GetComponent<Image>().enabled == true)
            {
                iSprite = bag[ID].GetComponent<Image>().sprite;
                title = bag[ID].GetComponent<RevolverAmmoItem>().title;
                description = bag[ID].GetComponent<RevolverAmmoItem>().description;
                itemDescription.SetDescription(iSprite, title, description);
            }
        }
        else
        {
            //close backpack sound efect
            inv.SetActive(false);

            playerController.enabled = true;
            shootController.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
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
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && ID<bag.Count-1) //right if(e or right arrow)
        {
            ID++;
        }
        if (Input.GetKeyDown(KeyCode.A) && ID > 0) //left
        {
            ID--;
        }
        if (Input.GetKeyDown(KeyCode.W) && ID > 2) //up
        {
            ID -= 3;
        }
        if (Input.GetKeyDown(KeyCode.S) && ID < 6) //down
        {
            ID += 3;
        }
        selector.transform.position = bag[ID].transform.position;
    }
}
