using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{
    public List<Nodo> NodosVecinos;
    public float PesoDelArco;

    public Nodo GetNextNode()
    {
        int index = Random.Range(0, NodosVecinos.Count);
        return NodosVecinos[index];
    }
}
