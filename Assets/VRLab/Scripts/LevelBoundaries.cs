using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LevelBoundaries : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(startPos != null)
            {
                other.transform.position = startPos.position;
            }
            else
            {
                other.transform.position = this.transform.position;
            }
            
        }
    }
}
