using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Nodos : MonoBehaviour
{
    public Nodos[] NodeControl;
    public int Factordepeso; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Nodos getNextNode()
    {
        int index = Random.Range(0, NodeControl.Length);
        return NodeControl[index];
    }
}
