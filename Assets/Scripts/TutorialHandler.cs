using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialHandler : MonoBehaviour
{
    public GameObject[] objectsToSwitch;
    private int currentIndex = 0;
    private bool canChange = false;

    [SerializeField] private Animator animator;
    [SerializeField] private int targetIndex;
    [SerializeField] private GameObject pressText;

    void Start()
    {
        for (int i = 0; i < objectsToSwitch.Length; i++)
        {
            objectsToSwitch[i].SetActive(i == 0);
        }

        pressText.SetActive(false);
        StartCoroutine(Timer());
    }

    void Update()
    {
        if (canChange)
        {
            if (Input.anyKey)
            {
                    pressText.SetActive(false);
                    objectsToSwitch[currentIndex].SetActive(false);
                    currentIndex = (currentIndex + 1) % objectsToSwitch.Length;
                    objectsToSwitch[currentIndex].SetActive(true);
                    canChange = false;

                StartCoroutine(Timer());

                if (currentIndex == objectsToSwitch.Length - 1)
                {
                    StartCoroutine(nextScene());
                }

            }
        }
    }

    
    
    private IEnumerator nextScene()
    {
        animator.SetTrigger("EndScene");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(targetIndex);
    }
    
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(3f);
        canChange = true;
        pressText.SetActive(true);
    }
    
}
