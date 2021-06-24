using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpPower = 5;
    [SerializeField]
    private float rotationAngleJump;
    [SerializeField]
    private float rotationAngleFall;
    [SerializeField]
    private float timeRotation = 2;
    [SerializeField]
    private AnimationCurve rotationCurve;
    [SerializeField]
    private ForceMode2D forceMode;
    
    private bool isAlive;

    private float currentAngle;
    private Rigidbody2D playerRigidbody;
    private float timeSinceJump;
    
    private Vector3 startPosition;
    private Quaternion startAngle;

    public delegate void MethodContainer();
    public static event MethodContainer EventGameOver;
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        startPosition = playerRigidbody.position;
        startAngle = transform.rotation;
        Reset();
    }

    private void Update()
    {
        timeSinceJump += Time.deltaTime;
        currentAngle = Mathf.Lerp(rotationAngleJump, rotationAngleFall, rotationCurve.Evaluate(timeSinceJump / timeRotation));
        transform.rotation = Quaternion.Euler(0,0, currentAngle);
    }

    public void Jump()
    {
        playerRigidbody.velocity = Vector2.up * jumpPower;
        timeSinceJump = 0;
        currentAngle = rotationAngleJump;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Game Over");
        if (EventGameOver != null) EventGameOver();
    }

    public void Reset()
    {
        timeSinceJump = 0;
        playerRigidbody.position = startPosition;
        transform.rotation = startAngle;
        currentAngle = transform.rotation.z;
        isAlive = true;
    }
}
