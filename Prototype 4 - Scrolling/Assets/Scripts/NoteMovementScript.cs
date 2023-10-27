using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

public class NoteMovementScript : MonoBehaviour
{
    public float Speed { get; set; }
    public Note Note { get; set; }

    private void Start()
    {
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(-Speed*5*Time.fixedDeltaTime, 0, 0);
    }
}
