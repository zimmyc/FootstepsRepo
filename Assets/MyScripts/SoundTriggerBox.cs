using System;
using UnityEngine;
using UnityEngine.Events;

public class SoundTriggerBox : MonoBehaviour
{
    public UnityEvent triggerEvent;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerEvent.Invoke();
        }
    }
}
