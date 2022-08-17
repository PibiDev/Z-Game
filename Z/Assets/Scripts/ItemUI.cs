using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [SerializeField]
    public TMP_Text quantity;

    // Start is called before the first frame update
    void Awake(){
        //ResetQuantity();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetQuantity(){
        this.quantity.text = "";
    }

    public void SetQuantity(string iQuantity){
        this.quantity.text = iQuantity;
    }
}
