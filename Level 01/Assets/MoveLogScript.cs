using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLogScript : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] float LogSpeed = 2;
    [SerializeField] float LogSpeed = 1;
    void Start()
    {
       // transform.Rotate(1f, 80f, -20f, Space.Self);
        //var rb = gameObject.GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(1,0,0) * LogSpeed * Time.deltaTime);
    }
    void Update()
    {
        transform.position += new Vector3(1,0,-1) * LogSpeed * Time.deltaTime;
    }
}
