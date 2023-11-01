using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Nodos CurrentNode;
    private Vector2 Velocidad;
    public float MovementTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, CurrentNode.transform.position, ref Velocidad, MovementTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Nodo")
        {
            CurrentNode = collision.gameObject.GetComponent<Nodos>().GetNextNode();
        }
    }
}
