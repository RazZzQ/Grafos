using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Nodos Objetivo;
    public Vector2 Velocidad;
    public float tiempoAllegar;

    public int energiaMaxima = 100;
    public int energiaActual;

    public float tiempoDescanso = 2f;
    private float tiempoDescansoActual = 0f;

    private bool descansando = false;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializar la energía al máximo al inicio
        energiaActual = energiaMaxima;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!descansando)
        {
            if (energiaActual > 0)
            {
                MoverHaciaNodo(Objetivo);
                if (Vector2.Distance(transform.position, Objetivo.transform.position) < 0.1f)
                {
                    Objetivo = Objetivo.getNextNode();
                }
            }
            else
            {
                descansando = true;
                tiempoDescansoActual = 0f;
            }
        }
        else
        {
            tiempoDescansoActual += Time.deltaTime;
            if (tiempoDescansoActual >= tiempoDescanso)
            {
                energiaActual = energiaMaxima;
                descansando = false;
                Objetivo = Objetivo.getNextNode();
            }
        }
    }
    private void MoverHaciaNodo(Nodos nodo)
    {
        transform.position = Vector2.SmoothDamp(transform.position, nodo.transform.position, ref Velocidad, tiempoAllegar);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Nodo" && !descansando)
        {
            energiaActual -= Objetivo.Factordepeso;

            Objetivo = collision.GetComponent<Nodos>().getNextNode();
        }
    }

    private void FindNextNode()
    {
        // Verificar si el jugador no está descansando
        if (!descansando)
        {
            // Encontrar el próximo nodo
            Objetivo = Objetivo.getNextNode();
        }
    }
}
