using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShell : MonoBehaviour
{
    public float Speed = 1.0f;
    
    void Start()
    {
        
    }

    void Update()
    {
        this.transform.Translate(0,Time.deltaTime * Speed * 0.5f, Time.deltaTime * Speed);
    }
}
