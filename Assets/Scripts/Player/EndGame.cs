using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    [SerializeField] private int winIndex;
    [SerializeField] private int deathIndex;
    

    private void Win()
    {
        SceneManager.LoadScene(winIndex);
    }

    private void Death()
    {
        SceneManager.LoadScene(deathIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Win"))
        {
            Win();
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Death();
        }
    }

}
