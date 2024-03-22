using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    [SerializeField] private int sceneDestination;


    public void GoToScene()
    {
        SceneManager.LoadScene(sceneDestination);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
