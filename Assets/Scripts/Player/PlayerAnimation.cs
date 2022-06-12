using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    int isWalkForwardHash, isRunningHash, isCrouchHash, isCrawlForwardHash, isCrawlBackwardHash, isWalkBackwardHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        //Define Parameter untuk meningkatkan performa
        isWalkForwardHash = Animator.StringToHash("isWalkForward");
        isWalkBackwardHash = Animator.StringToHash("isWalkBackward");
        isRunningHash = Animator.StringToHash("isRunning");
        isCrouchHash = Animator.StringToHash("isCrouch");
        isCrawlForwardHash = Animator.StringToHash("isCrawlForward");
        isCrawlBackwardHash = Animator.StringToHash("isCrawlBackward");
    }

    // Update is called once per frame
    void Update()
    {
        //Animation
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalkForward = animator.GetBool(isWalkForwardHash);
        bool isWalkBackward = animator.GetBool(isWalkBackwardHash);
        bool isCrouch = animator.GetBool(isCrouchHash);
        bool isCrawlForward = animator.GetBool(isCrawlForwardHash);
        bool isCrawlBackward = animator.GetBool(isCrawlBackwardHash);

        bool runPressed = Input.GetKey("left shift");
        bool forwardPressed = Input.GetKey("w");
        bool crouchPressed = Input.GetKey(KeyCode.LeftControl);
        bool backwardPressed = Input.GetKey("s");

        //Walk Forward
        if (!isWalkForward && forwardPressed && !crouchPressed)
        {
            animator.SetBool(isWalkForwardHash, true);
        }

        if (isWalkForward && !forwardPressed)
        {
            animator.SetBool(isWalkForwardHash, false);
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
        if (!isWalkBackward && backwardPressed && !crouchPressed)
        {
            animator.SetBool(isWalkBackwardHash, true);
        }
        if (isWalkBackward && !backwardPressed)
        {
            animator.SetBool(isWalkBackwardHash, false);
        }

        //Crouch idle
        if (!isCrouch && crouchPressed)
        {
            animator.SetBool(isCrouchHash, true);
        }

        if (isCrouch && !crouchPressed)
        {
            animator.SetBool(isCrouchHash, false);
        }

        //Crawl Forward
        if (!isCrawlForward && (forwardPressed && crouchPressed))
        {
            animator.SetBool(isCrawlForwardHash, true);
        }

        if (isCrawlForward && (!forwardPressed || !crouchPressed))
        {
            animator.SetBool(isCrawlForwardHash, false);
            animator.SetBool(isWalkForwardHash, true);
        }

        if (!forwardPressed && crouchPressed)
        {
            animator.SetBool(isCrawlForwardHash, false);
            animator.SetBool(isCrouchHash, true);
        }

        //Crawl Backward
        if (!isCrawlBackward && (backwardPressed && crouchPressed))
        {
            animator.SetBool(isCrawlBackwardHash, true);
        }

        if (isCrawlBackward && (!backwardPressed || !crouchPressed))
        {
            animator.SetBool(isCrawlBackwardHash, false);
            animator.SetBool(isWalkBackwardHash, true);
        }

        if (!backwardPressed && crouchPressed)
        {
            animator.SetBool(isCrawlBackwardHash, false);
            animator.SetBool(isCrouchHash, true);
        }

    }
}
