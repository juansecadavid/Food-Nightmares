using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzadorEnemy : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float speed = 500;
    public float time = 5;
    float timer;
    private float tiempoLanzamiento;
    int direction = 1;
    public float changeTime = 3.0f;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        Enemigo1 player = GetComponent < Enemigo1 >();
        rigidbody2d.AddForce(direction* speed);
    }

    public void CambioDireccion()
    {

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;

        }

        tiempoLanzamiento += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
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
