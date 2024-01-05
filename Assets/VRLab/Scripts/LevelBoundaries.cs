using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRLabEssentials;

[RequireComponent(typeof(Collider))]
public class LevelBoundaries : MonoBehaviour
{
    [SerializeField] private Transform startPos;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.TryGetComponent(out Tool t))
        {
            if(startPos != null)
            {
                other.transform.position = startPos.position;
            }
            else
            {
                other.transform.position = this.transform.position + (Vector3.up);
            }
            
        }
    }
}
