using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManzanPW : MonoBehaviour
{
    private SoundManager soundManager;
    public int health = 1;
    void OnTriggerEnter2D(Collider2D other)
    {
        EdgarController controller = other.GetComponent<EdgarController>();

        if (controller != null)
        {
            if (controller.health < controller.MaxHealth)
            {
                controller.ChangeHealth(health);
                soundManager.SeleccionAudios(2, 0.7f);
                Destroy(gameObject);
            }
        }
    }

    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    Rigidbody2D rigidbody2d;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        soundManager = FindObjectOfType<SoundManager>();
        timer = changeTime;
    }

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
