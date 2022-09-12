using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeFinal3 : MonoBehaviour
{
    Vector2 enemyPos;
    public GameObject playerM;
    bool perseguir;
    public float vel;
    Rigidbody2D rigidbody2D;
    public float MaxHealth = 10;
    public float currentHealth;
    public float da�oMisil1 = 1;
    public float da�oMisil2 = 2;
    public GameObject PanelWin;

    private SoundManager rugido;
    private EdgarController controller;
    float distancia;
    Vector2 lookDirection = new Vector2(1, 0);

    Animator animator;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = MaxHealth;
        animator = GetComponent<Animator>();
        rugido = FindObjectOfType<SoundManager>();
        controller = GetComponent<EdgarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (perseguir)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyPos, vel * Time.deltaTime);
            distancia = Vector2.Distance(enemyPos, transform.position);




            lookDirection.Set(enemyPos.x, enemyPos.y);
            animator.SetFloat("Move X", lookDirection.x);
            animator.SetFloat("Move Y", lookDirection.y);


        }
        if (Vector2.Distance(transform.position, enemyPos) > 12f)
        {
            perseguir = false;
        }

        //eso();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Edgar"))
        {
            enemyPos = playerM.transform.position;
            perseguir = true;
        }

    }

    void eso()
    {
        EdgarController.shoot1enable = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EdgarController player = collision.gameObject.GetComponent<EdgarController>();

        if (player != null)
        {
            player.ChangeHealth(0);
        }

        if (collision.transform.CompareTag("Misil"))
        {
            currentHealth = currentHealth - da�oMisil1;
            Debug.Log(currentHealth + "/" + MaxHealth);
        }

        if (collision.transform.CompareTag("MisilPw"))
        {
            currentHealth = currentHealth - da�oMisil2;
            Debug.Log(currentHealth + "/" + MaxHealth);
        }
        if (currentHealth <= 0)
        {
            PanelWin.SetActive(true);
            Destroy(gameObject, 0);
        }
    }
    private void OnDestroy()
    {
        EdgarController.score += 1000;
    }
}
