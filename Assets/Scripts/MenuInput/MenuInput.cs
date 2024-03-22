using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuInput : MonoBehaviour
{
    [Header("Drag Pause Prefab Here")]
    [SerializeField] GameObject Menu;
    
    private GameObject MovementGameObject;
    private GameObject Player;
    private movementManager MovementManager;
    [HideInInspector] public bool menuOpen = false;
    private PlayerControls input = null;

    [SerializeField] private Animator animator;
    private void Awake() 
    {
        input = new PlayerControls();
    }

    private void OnEnable() 
    {
        input.Enable();
        input.Control.pause.performed += OnPausePerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Control.pause.performed -= OnPausePerformed;
    }
    private void OnPausePerformed(InputAction.CallbackContext context)
    {   

        menuOpen = !menuOpen;
    }
    void Start()
    {
        Menu.SetActive(false);
        MovementGameObject = GameObject.Find("Movement manager");
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(menuOpen)
        {
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            Menu.SetActive(true);
            MovementGameObject.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        {
            Menu.SetActive(false);
            MovementGameObject.SetActive(true);
            Time.timeScale = 1f;
        }
    }
    public void Resume()
    {
        menuOpen = false;
    }
    public void BacktoMenu()
    {
        StartCoroutine(AnimDelay());
    }

    private IEnumerator AnimDelay()
    {
        animator.SetTrigger("EndScene");
        yield return new WaitForSeconds(1.5f);
        
        Time.timeScale = 1f;
        Debug.Log("Change Scene to menu");
        SceneManager.LoadScene(0);
    }
}
