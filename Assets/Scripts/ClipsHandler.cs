using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClipsHandler : MonoBehaviour
{
    private float initialDelay = 3f;
    private bool canChange = false;
    [SerializeField] private int targetIndex;
    [SerializeField] private bool auto;

    [SerializeField] private GameObject PressText;
    [SerializeField] private Animator animator;


    private void Start()
    {
        if (!auto)
        {
            StartCoroutine(InitialDelay());
            PressText.SetActive(false);
        }
    }

    private void Update()
    {
        if (canChange)
        {
            if (Input.anyKeyDown)
            {
                StartCoroutine(NextScene());
            }
        }

        else if (auto)
        {
            StartCoroutine(AutoTimer());
        }
}

    private IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(initialDelay);
        canChange = true;
        PressText.SetActive (true);
        
    }

    private IEnumerator AutoTimer()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(NextScene());
    }
    
    private IEnumerator NextScene()
    {
        animator.SetTrigger("EndScene");
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(targetIndex);
    }
}
