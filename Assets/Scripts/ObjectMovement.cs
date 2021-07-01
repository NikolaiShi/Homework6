using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private GameObject block;
    [SerializeField]
    public float speed = 1;
    void Start()
    {
        block = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        block.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

}
