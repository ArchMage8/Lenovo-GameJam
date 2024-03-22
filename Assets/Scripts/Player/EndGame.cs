using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    [SerializeField] private int winIndex;
    [SerializeField] private int deathIndex;

    [SerializeField] private Animator animator;
    

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
            StartCoroutine(WinScene());
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && !collision.GetComponent<EnemyManager>().isPossessed)
        {
            StartCoroutine(DeathScene());   
        }
    }

    private IEnumerator WinScene()
    {
        animator.SetTrigger("EndScene");
        yield return new WaitForSeconds(1.5f);

        Win();
    }

    private IEnumerator DeathScene()
    {
        animator.SetTrigger("EndScene");
        yield return new WaitForSeconds(0.1f);

        Death();
    }
}
