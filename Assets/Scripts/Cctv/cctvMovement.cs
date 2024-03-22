using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cctvMovement : MonoBehaviour
{
    [SerializeField] FieldOfView fieldOfView;
    public float maxLeft;
    public float maxRight;
    private Rigidbody2D rb;
    public float speed;
    public float waitTime;
    private bool turningRight = false;
    public bool cctvOn;
    public GameObject flashlight;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = speed;
        // maxRight *= -1;
    }

    // Update is called once per frame
    void Update()
    {   
        rb.velocity = new Vector2(0,0);
        if(cctvOn){
            rotationMovement();
            // rb.angularVelocity = speed;
            if(fieldOfView.targetObject != null){
                Death();
            }
        }
        else{
            rb.angularVelocity = 0;
            flashlight.SetActive(false);
        }
    }

    void rotationMovement(){
        if(rb.rotation < -maxRight && turningRight){
            rb.angularVelocity = 0;
            rb.rotation = -maxRight;
            StartCoroutine(turnLeft()); 
        }
        if(rb.rotation > maxLeft && !turningRight){
            rb.angularVelocity = 0;
            rb.rotation = maxLeft;
            StartCoroutine(turnRight());
        }
    }
    IEnumerator turnLeft()
    {
        yield return new WaitForSeconds(waitTime);
        rb.angularVelocity = speed;
        turningRight = false;
    }
    IEnumerator turnRight()
    {
        yield return new WaitForSeconds(waitTime);
        rb.angularVelocity = -speed;
        turningRight = true;
    }
    
    private void Death()
    {
        SceneManager.LoadScene(10);


    }
}
