using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectArrayExample : MonoBehaviour
{
    public bool test = false;
    [SerializeField]
    private GameObject[] targetGameObjects;
    

    void Start()
    {
        

    }
    void Update(){
        if(test){
            foreach (GameObject obj in targetGameObjects)
            {
                if (obj != null)
                {
                    obj.GetComponent<cctvMovement>().cctvOn = false;
                }
            }
            test = false;
        }
    }
}