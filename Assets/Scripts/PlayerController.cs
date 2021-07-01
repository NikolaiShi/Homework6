using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    
    private bool isAlive = false;
    private bool redyToStart = true;
    private float currentAngle;
    private Rigidbody2D playerRigidbody;
    private float timeSinceJump;
    
    private Vector3 startPosition;
    private Quaternion startAngle;

    public delegate void MethodContainer();
    public static event MethodContainer EventGameOver;
    public static event MethodContainer AddOnePoint;
    public static event MethodContainer StartGame;
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        startPosition = playerRigidbody.position;
        startAngle = transform.rotation;
        playerRigidbody.isKinematic = true;

    }

    private void Update()
    {
        if (isAlive)
        {
            timeSinceJump += Time.deltaTime;
            currentAngle = Mathf.Lerp(rotationAngleJump, rotationAngleFall, rotationCurve.Evaluate(timeSinceJump / timeRotation));
            transform.rotation = Quaternion.Euler(0, 0, currentAngle);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && redyToStart)
        {
            if (isAlive)
            {
                playerRigidbody.velocity = Vector2.up * jumpPower;
                timeSinceJump = 0;
                currentAngle = rotationAngleJump;
            }
            else
            {
                playerRigidbody.isKinematic = false;
                isAlive = true;
                if (StartGame != null) StartGame();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Game Over");
        redyToStart = false;
        if (EventGameOver != null) EventGameOver();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (AddOnePoint != null) AddOnePoint();
    }
    
    public void Restart()
    {
        playerRigidbody.isKinematic = true;
        playerRigidbody.position = startPosition;
        transform.rotation = startAngle;
        currentAngle = transform.rotation.z;
        timeSinceJump = 0;
        isAlive = false;
        redyToStart = true;
    }
}
