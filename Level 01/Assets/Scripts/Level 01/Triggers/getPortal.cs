﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPortal : MonoBehaviour
{
    [SerializeField] private Animator myAnimatorController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimatorController.SetBool("getPortal", true);
        }
    }
}

