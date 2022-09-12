using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefesController : MonoBehaviour
{
    public GameObject jefe1;
    public GameObject jefe2;
    public GameObject PanelWin;
    void Start()
    {
        jefe1.SetActive(false);
        jefe2.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject==isActiveAndEnabled)
        {
            jefe1.SetActive(true);
            jefe2.SetActive(true);
        }

        Panel();
    } 
    void Panel()
    {
        JefeFinal2 other = GetComponent<JefeFinal2>();
        if (other.currentHealth < 0)
        {
            PanelWin.SetActive(true);
        }
    }
}
