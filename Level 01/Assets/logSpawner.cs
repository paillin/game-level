using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class logSpawner : MonoBehaviour
{
    [SerializeField] GameObject Log1;
    [SerializeField] GameObject Log2;
    [SerializeField] GameObject Location;
    [SerializeField] GameObject Parent;
    void Start()
    {
        StartCoroutine(MakeALog());
    }

    IEnumerator MakeALog()
    {
        // var time = Random.Range(2f, 10f);
        var time = Random.Range(2f, 10f);
        yield return new WaitForSeconds(time);
        NewLog();
        StartCoroutine(MakeALog());
    } 

    private void NewLog()
    {
        GameObject log = Random.Range(0, 1) == 1 ? Log1 : Log2;
        var x = Random.Range(9f, 24f);
        Instantiate(log, Location.transform.position + new Vector3(x,0,0), log.transform.rotation, Parent.transform);
    }
}
