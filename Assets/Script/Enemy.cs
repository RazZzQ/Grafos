using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float energiaMaxima = 100f;
    public float energiaActual;
    public float tiempoDescanso = 5f;
    private bool descansando = false;
    private float tiempoDescansoActual = 0f;
    public Nodos objetivoNodo;

    private Vector2 velocidad;
    public float movimientoTiempo;

    // Start is called before the first frame update
    void Start()
    {
        energiaActual = energiaMaxima;
        objetivoNodo = GetNextNode();
    }

    // Update is called once per frame
    void Update()
    {
        if (!descansando)
        {
            if (energiaActual > 0)
            {
                MoverHaciaNodo(objetivoNodo);
                if (Vector2.Distance(transform.position, objetivoNodo.transform.position) < 0.1f)
                {
                    objetivoNodo = GetNextNode();
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
                objetivoNodo = GetNextNode();
            }
        }
    }

    private int CompararDistancias(Nodos a, Nodos b)
    {
        float distanciaA = Vector2.Distance(transform.position, a.transform.position);
        float distanciaB = Vector2.Distance(transform.position, b.transform.position);
        return distanciaA.CompareTo(distanciaB);
    }

    private Nodos GetNextNode()
    {
        // Lista temporal para almacenar los nodos ordenados por distancia
        List<Nodos> nodosOrdenados = new List<Nodos>(objetivoNodo.ListaNodosVecinos);

        // Ordenar la lista de nodos utilizando el método CompararDistancias como comparador
        nodosOrdenados.Sort(CompararDistancias);

        // Devolver el nodo más cercano
        return nodosOrdenados[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Nodo")
        {
            energiaActual = energiaActual - objetivoNodo.PesoDelArco;
            objetivoNodo = collision.gameObject.GetComponent<Nodos>().GetNextNode();
        }
    }

    private void MoverHaciaNodo(Nodos nodo)
    {
        transform.position = Vector2.SmoothDamp(transform.position, nodo.transform.position, ref velocidad, movimientoTiempo);
    }
}

