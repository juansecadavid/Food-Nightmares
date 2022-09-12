using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalsaController : MonoBehaviour
{
    AudioSource ouch;

    public void Start()
    {
        ouch = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EdgarController controller = other.GetComponent<EdgarController>();

        if (controller != null)
        {
            
            ouch.Play();
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        EdgarController controller = other.GetComponent<EdgarController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);           
        }
    }
}
