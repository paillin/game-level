using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOther : MonoBehaviour
{
    [SerializeField]
    private Transform other = null;
    [SerializeField]
    [Range(0.1f, 2.9f)]
    private float rigidness = 1.0f;
    private Vector3 movementOffset;
    void Start()
    {
        movementOffset = transform.position - other.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, other.position + movementOffset, rigidness *  Time.deltaTime);
    }
}
