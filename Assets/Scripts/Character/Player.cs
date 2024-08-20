using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 7f;
    [SerializeField] private float acceleration = 5f;      
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private GameInput gameInput;
    //[SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform worldObjectHoldPoint;
    public Transform cameraTransform;

    private Rigidbody rb;

    private bool isWalking;
    //private Vector3 lastInteractDir;
    //private BaseCounter selectedCounter;
    private WorldObject WorldObject;

    private Vector3 currentVelocity;

    public float GetNormalizedSpeed()
    {
        float normalizedSpeed = (currentVelocity.magnitude / moveSpeed) * 5;
        return Mathf.Clamp01(normalizedSpeed); 
    }
    public bool IsWalking()
    {
        return isWalking;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;

        rb = GetComponent<Rigidbody>();
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        /*if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }*/
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        /*if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }*/
    }

    private void Update()
    {
        //HandleMovement();
        //HandleInteractions();
        HandleInput();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void HandleInput()
    {
        var axis = gameInput.GetMovementVectorNormalized();
        Vector3 inputDirection = new Vector3(axis.x, 0f, axis.y).normalized;

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 targetMovement = cameraForward * inputDirection.z + cameraRight * inputDirection.x;

        if (inputDirection.magnitude > 0)
        {
            currentVelocity = Vector3.Lerp(currentVelocity, targetMovement * moveSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }
    }

    private void Movement()
    {
        rb.MovePosition(rb.position + currentVelocity * Time.fixedDeltaTime);

        if (currentVelocity != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(currentVelocity, Vector3.up);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        isWalking = currentVelocity.magnitude > 0.1f;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //Apply a 45 degree rotation to the moveDir vector
        //moveDir = Quaternion.Euler(0, 45, 0) * moveDir;

        //Get the camera's rotation in the Y axis and apply it to the moveDir vector 
        moveDir = Quaternion.Euler(0, 45 + cameraTransform.eulerAngles.y, 0) * moveDir;

        float moveDistance = moveSpeed * Time.deltaTime;
        
        //Get the player's radius and height from the collider
        CapsuleCollider playerCollider = GetComponent<CapsuleCollider>();
        float playerRadius = playerCollider.radius;
        float playerHeight = playerCollider.height;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (canMove)
        {
               transform.position += moveDir * moveDistance;
        }
        else
        {
            // Cannot move towards moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // Can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                // Cannot move only on the X

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // Can move only on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    // Cannot move in any direction
                }
            }

        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }



    //Handle interactions
    /*
    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // Has ClearCounter
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);

            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    */
}
