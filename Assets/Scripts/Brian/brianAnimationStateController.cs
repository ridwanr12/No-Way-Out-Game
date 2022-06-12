using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brianAnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkForwardHash, isRunningHash;
    Vector3 velocity;
    public float speed;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkForwardHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        StartCoroutine(CalcSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalkForward = animator.GetBool(isWalkForwardHash);
        // bool runPressed = Input.GetKey(KeyCode.O);
        // bool forwardPressed = Input.GetKey(KeyCode.I);
        
        if(speed < 0.5)
        {
            animator.SetBool(isWalkForwardHash, false);
            animator.SetBool(isRunningHash, false);
        }else if (speed >= 0.5 && speed < 2.5)
        {
            animator.SetBool(isWalkForwardHash, true);
            animator.SetBool(isRunningHash, false);
        }
        else if(speed>=2.5)
        {
            animator.SetBool(isRunningHash, true);
            animator.SetBool(isWalkForwardHash, false);
        }

        // //Walk Forward
        // if (!isWalkForward && forwardPressed)
        // {
        //     animator.SetBool(isWalkForwardHash, true);
        // }

        // if (isWalkForward && !forwardPressed)
        // {
        //     animator.SetBool(isWalkForwardHash, false);
        // }

        // //Run
        // if (!isRunning && (forwardPressed && runPressed))
        // {
        //     animator.SetBool(isRunningHash, true);
        // }

        // if (isRunning && (!forwardPressed || !runPressed))
        // {
        //     animator.SetBool(isRunningHash, false);
        // }
    }

    IEnumerator CalcSpeed()
    {
        bool isPlaying = true;

        while (isPlaying)
        {
            Vector3 prevPos = transform.position;

            yield return new WaitForFixedUpdate();

            speed = Vector3.Distance(transform.position, prevPos) / Time.fixedDeltaTime;
        }
    }
}
