using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> bag = new List<GameObject>(); // List to store items
    public GameObject[] inv;
    public bool activate_inv;

    public PlayerController playerController;
    public ShootController shootController;

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

    int pahseInv;
    int iD_options;
    public List<GameObject> options = new List<GameObject>(); // List to store items
    public GameObject selectorOptions;

    void Awake()
    {
        itemDescription.ResetDescription();
    }

    // Start is called before the first frame update
    void Start()
    {
        //playerController = gameObject.GetComponent<PlayerController>();
        //shootController = gameObject.GetComponent<ShootController>();
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
                Debug.Log("inventory slot: " + i);
                if (bag[i].GetComponent<Image>().enabled == true) // if there are an item
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
                    bag[i].GetComponent<Image>().preserveAspect = true;
                    bag[i].GetComponent<Image>().sprite = collider2D.GetComponent<SpriteRenderer>().sprite; //set sprite image (inventory slot)

                    if (collider2D.GetComponent<RevolverAmmoItem>()) // if is a revolver ammo item then add the script component and...
                    {
                        bag[i].AddComponent<RevolverAmmoItem>().ammo = collider2D.GetComponent<RevolverAmmoItem>().ammo;
                        bag[i].GetComponent<RevolverAmmoItem>().title = collider2D.GetComponent<RevolverAmmoItem>().title;
                        bag[i].GetComponent<RevolverAmmoItem>().description = collider2D.GetComponent<RevolverAmmoItem>().description;
                        quantity = bag[i].GetComponent<RevolverAmmoItem>().ammo;
                        //Debug.Log(quantity);
                        bag[i].GetComponent<ItemUI>().SetQuantity(quantity.ToString());
                    }

                    if (collider2D.GetComponent<BateItem>())
                    {
                        bag[i].AddComponent<BateItem>().title = collider2D.GetComponent<BateItem>().title;
                        bag[i].AddComponent<BateItem>().description = collider2D.GetComponent<BateItem>().description;
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

            inv[0].SetActive(true);

            itemDescription.ResetDescription();

            Navegar();

            if (bag[ID].GetComponent<Image>().enabled == true)
            {
                itemDescription.image.enabled = true;
                iSprite = bag[ID].GetComponent<Image>().sprite;
                if (bag[ID].GetComponent<RevolverAmmoItem>())
                {
                    title = bag[ID].GetComponent<RevolverAmmoItem>().title;
                    description = bag[ID].GetComponent<RevolverAmmoItem>().description;
                    itemDescription.SetDescription(iSprite, title, description);
                }
                if (bag[ID].GetComponent<BateItem>())
                {
                    title = bag[ID].GetComponent<BateItem>().title;
                    description = bag[ID].GetComponent<BateItem>().description;
                    itemDescription.SetDescription(iSprite, title, description);

                }
                //opens inventory options only on slots with items
                if (Input.GetKeyDown(KeyCode.E) && activate_inv)
                {
                    pahseInv = 1;
                    inv[1].transform.position = bag[ID].transform.position; //change the position of inventory options to selected item
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
            //need close backpack sound efect

            pahseInv = 0;
            inv[1].SetActive(false);
            inv[0].SetActive(false);

            playerController.enabled = true;
            shootController.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //pahseInv = 0;
            //inv[1].SetActive(false);
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

    void OnTriggerExit2D(Collider2D coll)
    {
        takeItemText.text.enabled = false;
        canTake = false;
    }

    void Navegar()
    {
        switch (pahseInv)
        {
            case 0: //inventory
                selector.SetActive(true);
                inv[1].SetActive(false);
                if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && ID < bag.Count - 1) //right if(e or right arrow)
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

                /*
                if (Input.GetKeyDown(KeyCode.E) && activate_inv)
                {
                    pahseInv = 1;
                }
                */

                break;

            case 1: //inventory options
                inv[1].SetActive(true);

                if (Input.GetKeyDown(KeyCode.W) && iD_options > 0)
                {
                    iD_options--;
                }
                if (Input.GetKeyDown(KeyCode.S) && iD_options < options.Count - 1)
                {
                    iD_options++;
                }
                selectorOptions.transform.position = options[iD_options].transform.position;
                if (Input.GetKeyDown(KeyCode.Q) && activate_inv)
                {
                    pahseInv = 0;
                }
                break;
        }
    }
}
