﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyOnTriggerEnter : MonoBehaviour
{
    public LayerMask layerMask;

    private void OnTriggerEnter(Collider other)
    {
        if (layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
            Destroy(gameObject);
        }
    }
}
