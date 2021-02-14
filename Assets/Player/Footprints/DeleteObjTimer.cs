using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObjTimer : MonoBehaviour
{
    public float Timer = 10f;
    void Start()
    {
        StartCoroutine("deleteObjTimer");
    }
    IEnumerator deleteObjTimer()
    {
        yield return new WaitForSeconds(Timer);
        Destroy(gameObject);
    }
}
