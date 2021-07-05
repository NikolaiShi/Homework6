using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private GameObject objectOfMotion;
    [SerializeField]
    public float speed = 1;
    void Start()
    {
        objectOfMotion = gameObject;
    }

    void Update()
    {
        objectOfMotion.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

}
