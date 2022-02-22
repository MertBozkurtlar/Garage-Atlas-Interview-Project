using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterState", menuName = "Garage Atlas Interview Project/CharacterStateSO", order = 0)]
public class CharacterStateSO : ScriptableObject
{
    public PlayerMovement playerMovement;
    public Rigidbody rigidbody;
    public CharacterController characterController;
}