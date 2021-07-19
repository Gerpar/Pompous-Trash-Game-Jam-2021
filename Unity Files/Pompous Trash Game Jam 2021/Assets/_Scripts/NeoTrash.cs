using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rendered obsolete by changes to gameplay.

public class NeoTrash : MonoBehaviour
{
    [HideInInspector]
    public Vector3 startPos;

    private void Start()
    {
        startPos = this.transform.position;
    }
}

