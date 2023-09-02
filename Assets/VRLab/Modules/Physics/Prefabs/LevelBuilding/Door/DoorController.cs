using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoors(bool newState)
    {
        animator.SetBool("DoorState", newState);
    }

    [ContextMenu("ToggleDoors")]
    public void ToggleDoors()
    {
        OpenDoors(!animator.GetBool("DoorState"));
    }
}
