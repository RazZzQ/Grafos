using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float energiaMaxima = 100f;
    public float energiaActual;
    public float tiempoDescanso = 5f; // Tiempo de descanso en segundos
    private bool descansando = false;
    private float tiempoDescansoActual = 0f;
    public Nodos objetivoNodo; // El nodo hacia el que se dirige el enemigo
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
                // Mover al enemigo hacia el objetivoNodo
                MoverHaciaNodo(objetivoNodo);
                energiaActual = energiaActual - objetivoNodo.PesoDelArco;
                // Si el enemigo llega al objetivoNodo, busca el siguiente nodo
                if (Vector2.Distance(transform.position, objetivoNodo.transform.position) < 0.1f)
                {
                    objetivoNodo = GetNextNode();
                }
            }
            else
            {
                // El enemigo se queda sin energía, inicia el tiempo de descanso
                descansando = true;
                tiempoDescansoActual = 0f;
            }
        }
        else
        {
            // El enemigo está descansando, espera hasta que recupere su energía
            tiempoDescansoActual += Time.deltaTime;
            if (tiempoDescansoActual >= tiempoDescanso)
            {
                // Descanso completo, el enemigo recupera toda su energía
                energiaActual = energiaMaxima;
                descansando = false;
                objetivoNodo = GetNextNode();
            }
        }
    }

    private Nodos GetNextNode()
    {
        int index = Random.Range(0, objetivoNodo.ListaNodosVecinos.Count);
        return objetivoNodo.ListaNodosVecinos[index];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Nodo")
        {
            objetivoNodo = collision.gameObject.GetComponent<Nodos>().GetNextNode();
        }
    }

    private void MoverHaciaNodo(Nodos nodo)
    {
        transform.position = Vector2.SmoothDamp(transform.position, nodo.transform.position, ref velocidad, movimientoTiempo);
    }
}

