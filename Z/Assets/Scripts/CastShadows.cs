using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastShadows : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

//asignar a un sprite para que tenga sombras en el entorno 3D