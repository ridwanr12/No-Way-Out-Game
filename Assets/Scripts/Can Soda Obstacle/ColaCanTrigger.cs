using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaCanTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject brian;
    public brianNavMesh chasingPlayer;
    public AI patroll;
    public ChasingCanSoda chasingCanSoda;
    public float Distance_;

    private void OnTriggerEnter(Collider target)
    {
        StartCoroutine(ExampleCoroutine(target));
    }

    IEnumerator ExampleCoroutine(Collider target)
    {
        if (target.tag == "Player" && Distance_ < 112)
        {
            Debug.Log("Kesentuh Player dan jarak kurang dari 15");
            // chasingPlayer.enabled = true;
            patroll.enabled = false;
            chasingCanSoda.enabled = true;
            yield return new WaitForSeconds(10);
            chasingCanSoda.enabled = false;
            patroll.enabled = true;
        }


        // //Print the time of when the function is first called.
        // Debug.Log("Started Coroutine at timestamp : " + Time.time);

        // //yield on a new YieldInstruction that waits for 5 seconds.
        // yield return new WaitForSeconds(5);

        // //After we have waited 5 seconds print the time again.
        // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }


    // Update is called once per frame
    void Update()
    {
        Distance_ = Vector3.Distance(player.transform.position, brian.transform.position);
    }
}
