using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengerController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    public void Interact()
    {
        //Debug.Log("You will start a battle"); //prints to console
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
}


