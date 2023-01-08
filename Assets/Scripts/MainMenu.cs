using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    private PlayerInput playerInput;

    public void NewGame()
    {
        Debug.Log( "NEw gane");
        GameManager.instance.NewGame();
    }

    public void Quit()
    {
        GameManager.instance.Close();
    }

    private void Awake()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        SetupActions();
    }

    void SetupActions()
    {
        playerInput.actions["Start"].performed += StartGame;
    }

    void StartGame(InputAction.CallbackContext context)
    {
        GameManager.instance.NewGame();
    }
}
