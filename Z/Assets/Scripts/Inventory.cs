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
    int quantity;

    void Awake(){
        itemDescription.ResetDescription();
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
        take = Input.GetAxisRaw("Interact"); // E

            if (canTake == true && take > 0)
            {
                Destroy(collider2D.gameObject);

                for (int i = 0; i < bag.Count; i++)
                {
                    Debug.Log("inventory slot: "+i);
                    if (bag[i].GetComponent<Image>().enabled == true) // if there are a item
                    {
                        if (bag[i].GetComponent<RevolverAmmoItem>() && collider2D.GetComponent<RevolverAmmoItem>()) // and that item is a revolver ammo
                        {
                        bag[i].GetComponent<RevolverAmmoItem>().ammo += collider2D.GetComponent<RevolverAmmoItem>().ammo;
                        quantity = bag[i].GetComponent<RevolverAmmoItem>().ammo;
                        Debug.Log(quantity);
                        bag[i].GetComponent<ItemUI>().SetQuantity(quantity.ToString());
                        break;
                        }
                    }

                    if (bag[i].GetComponent<Image>().enabled == false) // if inventory slot is empty
                    {
                        bag[i].GetComponent<Image>().enabled = true; // image true
                        bag[i].GetComponent<Image>().sprite = collider2D.GetComponent<SpriteRenderer>().sprite; //set sprite image
                        
                        if (collider2D.GetComponent<RevolverAmmoItem>()) // if is a revolver ammo item then add the script component and...
                        {
                            bag[i].AddComponent<RevolverAmmoItem>().ammo = collider2D.GetComponent<RevolverAmmoItem>().ammo;
                            bag[i].GetComponent<RevolverAmmoItem>().title = collider2D.GetComponent<RevolverAmmoItem>().title;
                            bag[i].GetComponent<RevolverAmmoItem>().description = collider2D.GetComponent<RevolverAmmoItem>().description;
                            quantity = bag[i].GetComponent<RevolverAmmoItem>().ammo;
                            Debug.Log(quantity);
                            bag[i].GetComponent<ItemUI>().SetQuantity(quantity.ToString());
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
            {   itemDescription.image.enabled = true;
                iSprite = bag[ID].GetComponent<Image>().sprite;
                if (bag[ID].GetComponent<RevolverAmmoItem>())
                {
                    title = bag[ID].GetComponent<RevolverAmmoItem>().title;
                    description = bag[ID].GetComponent<RevolverAmmoItem>().description;
                    itemDescription.SetDescription(iSprite, title, description);
                    
                }
            }
            else
            {
                itemDescription.image.enabled = false;
                iSprite = null;
                title = "Not found";
                description = "Not found";
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
