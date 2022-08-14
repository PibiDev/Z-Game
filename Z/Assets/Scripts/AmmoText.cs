using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoText : MonoBehaviour
{
    public Text text;
    public int ammoAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>(); 
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ammoAmount > 0){
            text.text = ammoAmount + "/6";
        }
        else{
            text.text = "0/6";
        }
    }
}
