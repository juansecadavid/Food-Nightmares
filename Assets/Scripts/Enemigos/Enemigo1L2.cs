using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo1L2 : MonoBehaviour
{
    public bool Nivel1;
    public bool Nivel2;
    public bool Nivel3;

    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public int MaxHealth = 10;
    int currentHealth;
    public int dañoMisil1 = 1;
    Rigidbody2D rigidbody2d;
    float timer;
    public bool diferente;
    private float tiempoLanzamiento;
    int direction = 1;
    public GameObject projectilePrefab;
    public GameObject projectilePrefab2;
    public Transform Punto_instancia;
    Vector2 lookDirection = new Vector2(1, 0);


    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        currentHealth = MaxHealth;
        if (Nivel1 == true)
            speed = Random.Range(1.0f, 2.0f);
        if (Nivel2 == true)
            speed = Random.Range(3.0f, 4.0f);
        if (Nivel3 == true)
            speed = Random.Range(4.0f, 5.0f);
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        CambioDireccion();
        if (Nivel1 == true)
        {
            if (tiempoLanzamiento >= 2)
            {
                /*Instantiate(projectilePrefab, Punto_instancia.position, Quaternion.identity);*/
                Launch();
                tiempoLanzamiento = 0;
            }
        }
        if (Nivel2 == true)
        {
            if (tiempoLanzamiento >= 1)
            {
                /*Instantiate(projectilePrefab, Punto_instancia.position, Quaternion.identity);*/
                Launch2();
                tiempoLanzamiento = 0;
            }
        }
        if (Nivel3 == true)
        {

            if (tiempoLanzamiento >= 2)
            {
                /*Instantiate(projectilePrefab, Punto_instancia.position, Quaternion.identity);*/
                Launch2();
                tiempoLanzamiento = 1.5f;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        if(diferente==true)
        {
            if (vertical)
            {
                position.y = position.y + Time.deltaTime * speed * direction;

                lookDirection.Set(0, direction);


                animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", direction);
            }
            else
            {
                position.x = position.x + Time.deltaTime * speed * direction;

                lookDirection.Set(direction, 0);


                animator.SetFloat("Move X", direction);
                animator.SetFloat("Move Y", 0);


            }
        }
        else
        {
            if (vertical)
            {
                position.y = position.y + Time.deltaTime * speed * -direction;

                lookDirection.Set(0, -direction);


                animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", -direction);
            }
            else
            {
                position.x = position.x + Time.deltaTime * speed * -direction;

                lookDirection.Set(-direction, 0);


                animator.SetFloat("Move X", -direction);
                animator.SetFloat("Move Y", 0);


            }
        }
        rigidbody2d.MovePosition(position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.tag == "Misil")
        {
            currentHealth = currentHealth - dañoMisil1;
            Debug.Log(currentHealth + "/" + MaxHealth);
        }*/
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EdgarController player = other.gameObject.GetComponent<EdgarController>();

        if (player != null)
        {
            player.ChangeHealth(0);
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
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

    public void ChangeHealth(int amount)
    {
        currentHealth = currentHealth + amount;
        Debug.Log(currentHealth + "/" + MaxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        LanzadorEnemy projectile = projectileObject.GetComponent<LanzadorEnemy>();
        projectile.Launch(lookDirection, 300);
    }

    void Launch2()
    {
        GameObject projectileObject = Instantiate(projectilePrefab2, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        LanzadorEnemy2 projectile = projectileObject.GetComponent<LanzadorEnemy2>();
        projectile.Launch(lookDirection, 300);
    }
    private void OnDestroy()
    {
        EdgarController.score += 100;
    }
}
