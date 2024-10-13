using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {FreeRoam, Dialog, Battle}; //an array of 3 player states 

public class GameController : MonoBehaviour
{
   [SerializeField] PlayerController playerController; //adds visibility to playerController variable in the inspector

   GameState state;

   private void Start()
   {

    //Allows us to switch between states
    DialogManager.Instance.OnShowDialog += () =>
    {
        state = GameState.Dialog;
    }; 

    DialogManager.Instance.OnHideDialog += () =>
    {
        if (state == GameState.Dialog)
            state = GameState.FreeRoam;
    }; 

   }

   private void Update()
   {
    if (state == GameState.FreeRoam)
    {
        playerController.HandleUpdate();

    } else if (state == GameState.Dialog)
    {
        DialogManager.Instance.HandleUpdate();

    } else if (state == GameState.Battle)
    {

    }

   }
}


