using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private GameObject block;
    [SerializeField]
    private float speed = 1;
    void Start()
    {
        block = gameObject;
        StartCoroutine(DestroyCoroutin());
        DestroyObject(block, 16);
    }

    // Update is called once per frame
    void Update()
    {
        block.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    IEnumerator DestroyCoroutin()
    {

        yield return new WaitForSeconds(10);
        
    }
}
