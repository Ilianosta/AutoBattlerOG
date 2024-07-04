using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimEvents : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    public void EndTurn()
    {
        characterController.EndTurn();
    }
}
