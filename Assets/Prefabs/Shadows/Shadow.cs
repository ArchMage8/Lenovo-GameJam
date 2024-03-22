using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Shadow : MonoBehaviour
{
    private movementManager movementManager;
    private GameObject target;
    private GameObject MovementManager;
    private ShadowCaster2D ShadowCaster2D;

    // Start is called before the first frame update
    void Start()
    {
        MovementManager = GameObject.Find("Movement manager");
        movementManager = MovementManager.GetComponent<movementManager>();
        ShadowCaster2D = GetComponent<ShadowCaster2D>();
    }

    // Update is called once per frame
    void Update()
    {
        target = movementManager.target;
        if(transform.position.y >= target.transform.position.y){
            ShadowCaster2D.enabled = false;
        }
        else{
            ShadowCaster2D.enabled = true;
        }


    }
}
