using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour
{   
    [SerializeField]
    public Image image;
    [SerializeField]
    TMP_Text title;
    [SerializeField]
    TMP_Text description;

    void Awake(){

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetDescription(){
        this.image.gameObject.SetActive(false);
        this.title.text = "";
        this.description.text = "";
    }

    public void SetDescription(Sprite iSprite,string iTitle, string iDescription){
        this.image.gameObject.SetActive(true);
        this.image.sprite = iSprite;
        this.image.preserveAspect = true;
        this.title.text = iTitle;
        this.description.text = iDescription;
    }
    
}
