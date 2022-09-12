using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EdgarController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float Speed = 3.0f;
    public int MaxHealth = 10;
    int currentHealth;
    public static int score = 0;
    public static bool Moverse;
    public TextMeshProUGUI PanelScore;
    public int health { get { return currentHealth; } }

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    public float EfectoPwUp1=5;
    public int ModificadorPwUp1 = 2;
    public GameObject ScrewPrefab;
    public Slider slider;

    public GameObject projectilePrefab;
    public GameObject jefe;

    public GameObject panelReinicio;

    public TextMeshProUGUI Contador;
    public TextMeshProUGUI ContadorN;

    public float speedAgua;

    public static bool shoot1enable;

    int estrellasAcumuladas;
    Vector2 lookDirection = new Vector2(1, 0);
    bool enableShooting = false;
    int Shoots;
    float horizontal;
    float vertical;
    Animator animator;
    AudioSource beep1;
    private SoundManager soundManager;
    public GameObject PanelWin;
    public TextMeshProUGUI ScorePausa;
    int acumulacionagua;
    void Start()
    {
        beep1 = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = MaxHealth;
        jefe.SetActive(false);
        estrellasAcumuladas = 0;
        animator = GetComponent<Animator>();
        panelReinicio.SetActive(false);
        PanelWin.SetActive(false);
        Time.timeScale = 1;
        soundManager = FindObjectOfType<SoundManager>();
        Contador.text = ": " + estrellasAcumuladas;
        Shoots = 0;
        ContadorN.text = ": " + Shoots;
        shoot1enable = true;
        PanelScore.text = "Score: " + score;
        ScorePausa.text = "Score: " + score;
        Moverse = true;
        acumulacionagua = 0;
        Speed = speedAgua;
    }

    // Update is called once per frame
    void Update()
    {
       if(Moverse==true)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            Vector2 move = new Vector2(horizontal, vertical);
            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize();
            }
            animator.SetFloat("Move X", lookDirection.x);
            animator.SetFloat("Move Y", lookDirection.y);
            animator.SetFloat("Speed", move.magnitude);
        }
       else
        {
            animator.SetFloat("Speed", 0);
        }

        Contador.text = ": " + estrellasAcumuladas;
        ContadorN.text = "; " + Shoots;
        PanelScore.text = "Score: " + score;
        ScorePausa.text = "Score: " + score;

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        if(jefe!=null&&estrellasAcumuladas==4)
        {
            jefe.SetActive(true);
            //soundManager.SeleccionAudios(3, 0.4f);      
        }

        if(shoot1enable==true)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Launch();
            }
        }
     
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
           if(enableShooting&&Shoots>0)
            {
                LaunchScrew();
                Shoots--;
            }
        }
        if (currentHealth <= 0)
        {
            Time.timeScale = 0;
            panelReinicio.SetActive(true);
            
        }

        /*if(Speed==3)
        {
            StartCoroutine(TemporizadorStamina());
            
        }*/

        slider.value = currentHealth;
        slider.maxValue = MaxHealth;
    }

    void FixedUpdate()
    {
       if(Moverse==true)
        {
            Vector2 position = rigidbody2d.position;
            position.x = position.x + Speed * horizontal * Time.deltaTime;
            position.y = position.y + Speed * vertical * Time.deltaTime;
            rigidbody2d.MovePosition(position);
        }

        
    }


   
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
       
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, MaxHealth);
        Debug.Log("Vida Edgar: "+currentHealth + "/" + MaxHealth);
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.CompareTag("Powerup"))
        {
            
            Speed = Speed * ModificadorPwUp1;
            soundManager.SeleccionAudios(0, 0.7f);
            StartCoroutine(TemporizadorPowerUp1());
            score += 20;
        }
        if (other.CompareTag("MisilEnemy"))
        {
            
            currentHealth = currentHealth - 1;
            soundManager.SeleccionAudios(1, 0.7f);
            Debug.Log("Vida Edgar: "+currentHealth + "/" + MaxHealth);
        }
        if(other.CompareTag("Manzana"))
        {
            
            score += 20;
        }
        if (other.CompareTag("Prueba"))
        {
            soundManager.SeleccionAudios(0, 0.7f);
            enableShooting = true;
            Shoots = Shoots+5;
            score += 20;
            Debug.Log($"Shoots:{Shoots}");
        }

        if(other.CompareTag("Estrella"))
        {
            soundManager.SeleccionAudios(0, 0.7f);
            estrellasAcumuladas++;
            Debug.Log(estrellasAcumuladas);   
            /*if(estrellasAcumuladas==4)
                soundManager.SeleccionAudios(3, 0.6f);*/
            score += 50;
        }

        if (other.CompareTag("Agua"))
        {
            acumulacionagua += 5;
            soundManager.SeleccionAudios(1, 0.7f);
            Speed = speedAgua;
            StartCoroutine(TemporizadorStamina());
           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.transform.CompareTag("Jefe"))
        {
            soundManager.SeleccionAudios(1, 0.7f);
            currentHealth = currentHealth - 1;
            Debug.Log("Vida Edgar: "+currentHealth + "/" + MaxHealth);
        }
        if (collision.transform.CompareTag("Enemy"))
        {
            soundManager.SeleccionAudios(1, 0.7f);
            currentHealth = currentHealth - 1;
            Debug.Log("Vida Edgar: "+currentHealth + "/" + MaxHealth);
        }
        
    }

    IEnumerator TemporizadorStamina()
    {
        yield return new WaitForSeconds(acumulacionagua);
        Speed = (Speed / 2);
        
    }
    IEnumerator TemporizadorPowerUp1()
    {
        yield return new WaitForSeconds(EfectoPwUp1);
        Speed = (Speed / 2);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Lanzador1 projectile = projectileObject.GetComponent<Lanzador1>();
        projectile.Launch(lookDirection, 300);        
    }
    void LaunchScrew()
    {
        GameObject ScrewObject =Instantiate(ScrewPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Prueba Screwprojectile = ScrewObject.GetComponent<Prueba>();
        Screwprojectile.Launch(lookDirection, 300);
    }

    public void ReinicioPartida1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void ReinicioPartida2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void ReinicioPartida3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }
}
