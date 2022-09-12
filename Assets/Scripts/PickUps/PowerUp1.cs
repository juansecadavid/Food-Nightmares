using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp1 : MonoBehaviour
{

    AudioSource SonidoPw;

    private void Start()
    {
        SonidoPw = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        EdgarController controller = other.GetComponent<EdgarController>();
        if (other.CompareTag("Edgar"))
        {
            SonidoPw.Play();
            if (SonidoPw.isPlaying)
            {
                Destroy(gameObject);
                //controller.Speed = 3;
            }
        }
    }

    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    Rigidbody2D rigidbody2d;

    private void Update()
    {
        CambioDireccion();
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        rigidbody2d.MovePosition(position);
    }

    public void CambioDireccion()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

}
