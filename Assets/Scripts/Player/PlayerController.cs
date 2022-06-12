using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    Vector3 velocity;
    private Camera playerCamera;

    [Header("Movement Parameters")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float walkingSpeed = 2f;
    [SerializeField] private float runningSpeed = 5f;
    [SerializeField] private float crouchSpeed = 1.25f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 40.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;
    private float rotationX = 0;

    [Header("Crouch Parameters")]
    [SerializeField] private bool isCrouching;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool duringCrouchAnimation;
    [SerializeField] public float crouchHeight = 0.99f;
    [SerializeField] private float standHeight = 1.8f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchCenter = new Vector3(0,0.51f,0);
    [SerializeField] private Vector3 standCenter = new Vector3(0, 0.9f, 0);

    [Header("Camera Parameters")]
    [SerializeField] Vector3 idleRunCamera = new Vector3(0, (float)1.4, (float)0.31);
    [SerializeField] Vector3 walkForwardCamera = new Vector3(0, (float)1.4, (float)0.4);
    [SerializeField] Vector3 crouchCamera = new Vector3(0, (float)0.8, (float)0.7);
    [SerializeField] Vector3 crouchForwardCamera = new Vector3(0, (float)0.51, (float)0.85);
    [SerializeField] Vector3 crouchBackwardCamera = new Vector3(0, (float)0.72, (float)0.27);
    [SerializeField] Vector3 sidewardCamera = new Vector3((float)0.14, (float)1.19, (float)0.58);
    [SerializeField] Vector3 backwardCamera = new Vector3((float)-0.4, (float)0.97, (float)0.84);

    //ANIMATION
    Animator animator;
    int isWalkForwardHash, isRunningHash, isCrouchHash, isCrawlForwardHash, isCrawlBackwardHash, isWalkBackwardHash, isWalkLeftwardHash, isWalkRightwardHash;

    private bool shouldCrouch => Input.GetKeyDown(KeyCode.LeftControl) && !duringCrouchAnimation && characterController.isGrounded;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //ANIMATION
        animator = GetComponent<Animator>();

        //Define Parameter untuk meningkatkan performa
        isWalkForwardHash = Animator.StringToHash("isWalkForward");
        isWalkLeftwardHash = Animator.StringToHash("isWalkLeftward");
        isWalkRightwardHash = Animator.StringToHash("isWalkRightward");
        isWalkBackwardHash = Animator.StringToHash("isWalkBackward");
        isRunningHash = Animator.StringToHash("isRunning");
        isCrouchHash = Animator.StringToHash("isCrouch");
        isCrawlForwardHash = Animator.StringToHash("isCrawlForward");
        isCrawlBackwardHash = Animator.StringToHash("isCrawlBackward");
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        HandleMouseLook();
        HandleCameraPosition();

        if (canCrouch)
        {
            HandleCrouch();
        }

        HandleAnimation();
    }

    private void HandleCameraPosition()
    {
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool forwardPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool rightwardPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool leftwardPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool backwardPressed = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        Vector3 newPos = isCrouching ? crouchCamera : (forwardPressed) ? walkForwardCamera :(backwardPressed)?backwardCamera: (leftwardPressed || rightwardPressed) ? sidewardCamera : (isCrouching && forwardPressed) ? crouchForwardCamera : (isCrouching && backwardPressed) ? crouchBackwardCamera : idleRunCamera;

        playerCamera.transform.localPosition = Vector3.Lerp(
                playerCamera.transform.localPosition,
                newPos,
                Time.deltaTime * 10f);
    }

    private void HandleMovementInput()
    {
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool forwardPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        //Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        speed = (forwardPressed && runPressed && !isCrouching) ? runningSpeed : isCrouching ? crouchSpeed : walkingSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void HandleCrouch()
    {
        if(shouldCrouch)
            StartCoroutine(CrouchStand());
    }

    private IEnumerator CrouchStand()
    {
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
        {
            yield break;
        }

        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standCenter : crouchCenter;
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching;
        duringCrouchAnimation = false;
    }

    private void HandleAnimation()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalkForward = animator.GetBool(isWalkForwardHash);
        bool isWalkRightward = animator.GetBool(isWalkRightwardHash);
        bool isWalkLeftward = animator.GetBool(isWalkLeftwardHash);
        bool isWalkBackward = animator.GetBool(isWalkBackwardHash);
        bool isCrouch = animator.GetBool(isCrouchHash);
        bool isCrawlForward = animator.GetBool(isCrawlForwardHash);
        bool isCrawlBackward = animator.GetBool(isCrawlBackwardHash);

        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool forwardPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool rightwardPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool leftwardPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool backwardPressed = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        //Walk Forward
        if (!isWalkForward && forwardPressed && !isCrouching)
        {
            animator.SetBool(isWalkForwardHash, true);
        }

        if (isWalkForward && !forwardPressed)
        {
            animator.SetBool(isWalkForwardHash, false);
        }

        //Walk Rightward
        if (!isWalkRightward && rightwardPressed && !isCrouching)
        {
            animator.SetBool(isWalkRightwardHash, true);
        }

        if (isWalkRightward && !rightwardPressed)
        {
            animator.SetBool(isWalkRightwardHash, false);
        }

        //Walk Leftward
        if (!isWalkLeftward && leftwardPressed && !isCrouching)
        {
            animator.SetBool(isWalkLeftwardHash, true);
        }

        if (isWalkLeftward && !leftwardPressed)
        {
            animator.SetBool(isWalkLeftwardHash, false);
        }

        //Run
        if (!isRunning && (forwardPressed && runPressed))
        {
            animator.SetBool(isRunningHash, true);
        }

        if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }

        //Walk Backward
        if (!isWalkBackward && backwardPressed && !isCrouching)
        {
            animator.SetBool(isWalkBackwardHash, true);
        }
        if (isWalkBackward && !backwardPressed)
        {
            animator.SetBool(isWalkBackwardHash, false);
        }

        //Crouch Idle
        animator.SetBool(isCrouchHash, isCrouching);
        

        //Crawl Forward
        if (!isCrawlForward && (forwardPressed && isCrouching))
        {
            animator.SetBool(isCrawlForwardHash, true);

        }

        if (isCrawlForward && (!forwardPressed || !isCrouching))
        {
            animator.SetBool(isCrawlForwardHash, false);
            animator.SetBool(isWalkForwardHash, true);
        }

        if (!forwardPressed && isCrouching)
        {
            animator.SetBool(isCrawlForwardHash, false);
            animator.SetBool(isCrouchHash, true);
        }

        //Crawl Backward
        if (!isCrawlBackward && (backwardPressed && isCrouching))
        {
            animator.SetBool(isCrawlBackwardHash, true);
        }

        if (isCrawlBackward && (!backwardPressed || !isCrouching))
        {
            animator.SetBool(isCrawlBackwardHash, false);
            animator.SetBool(isWalkBackwardHash, true);
        }

        if (!backwardPressed && isCrouching)
        {
            animator.SetBool(isCrawlBackwardHash, false);
            animator.SetBool(isCrouchHash, true);
        }
    }
}
