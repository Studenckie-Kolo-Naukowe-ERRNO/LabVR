using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(CharacterController))]
public class ErrnoCharacterController : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float currentCharacterSpeed;

    private Vector3 lastPos;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        currentCharacterSpeed = Vector3.Distance(lastPos, transform.position)/Time.deltaTime;
        lastPos = transform.position;
    }
}
