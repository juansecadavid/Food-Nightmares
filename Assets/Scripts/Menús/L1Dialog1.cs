using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class L1Dialog1 : MonoBehaviour
{
    public TextMeshProUGUI textD;

    [TextArea(3,30)]
    public string[] parrafos;
    int index;
    public float velParrafo;

    public GameObject botonContinuar;
    public GameObject botonQuitar;

    public GameObject panelDialogo;
    public GameObject botonLeer;

    public bool Tiempo;

    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    Rigidbody2D rigidbody2d;

    void Start()
    {
        botonQuitar.SetActive(false);
        botonLeer.SetActive(false);
        panelDialogo.SetActive(false);
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        Tiempo = true;
    }

   
    void Update()
    {
        if(textD.text==parrafos[index])
        {
            botonContinuar.SetActive(true);
        }

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

    IEnumerator TextDialogo()
    {
        botonLeer.SetActive(false);
        botonContinuar.SetActive(false);
        foreach(char letra in parrafos[index].ToCharArray())
        {
            textD.text += letra;

            yield return new WaitForSeconds(velParrafo);
        }
    }

    public void SiguienteParrafo()
    {
        botonContinuar.SetActive(false);
        if(index<parrafos.Length-1)
        {
            index++;
            textD.text = "";
            StartCoroutine(TextDialogo());
        }
        else
        {
            textD.text = "";
            botonContinuar.SetActive(false);
            botonQuitar.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Tiempo==true)
        {
            if (collision.gameObject.CompareTag("Edgar"))
            {
                botonLeer.SetActive(true);
                EdgarController.Moverse = false;
            }
            else
            {
                botonLeer.SetActive(false);
            }
        }


    }


    IEnumerator Temporizador()
    {
        yield return new WaitForSeconds(5);
        Tiempo = true;
    }

    public void ActivarBotonLeer()
    {
        panelDialogo.SetActive(true);
        StartCoroutine(TextDialogo());
        
        
    }

    public void BotonCerrar()
    {
        panelDialogo.SetActive(false);
        botonLeer.SetActive(false);
        EdgarController.Moverse = true;
        Tiempo = false;
        StartCoroutine(Temporizador());
        
    }
}
