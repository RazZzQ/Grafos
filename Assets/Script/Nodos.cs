using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Nodos : MonoBehaviour
{
    public List<Nodos> ListaNodosVecinos;
    public float PesoDelArco;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Nodos GetNextNode()
    {
        int index = Random.Range(0, ListaNodosVecinos.Count);
        return ListaNodosVecinos[index];
    }
}
