using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float speed = 500;
    public float distance = 3;
    public float time = 5;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * speed);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        /*if(collision.transform.CompareTag("Jefe")||collision.transform.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }*/

        int dañoMisil = -2;

        Enemigo1 controller = other.transform.GetComponent<Enemigo1>();
        if (controller != null)
        {
            controller.ChangeHealth(dañoMisil);
        }

        Enemigo1L2 variable = other.transform.GetComponent<Enemigo1L2>();
        if (variable != null)
            variable.ChangeHealth(dañoMisil);

        if (other.otherCollider)
        {
            Destroy(gameObject);
        }

    }
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
