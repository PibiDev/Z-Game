using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    private Light lantern;
    float lightOn;

    // Start is called before the first frame update
    void Start()
    {
        lantern = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        //lightOn = Input.GetAxisRaw("TurnLight");
        if (Input.GetKeyUp(KeyCode.F))
        {
            lantern.enabled = !lantern.enabled;
        }
    }
}
