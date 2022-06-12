using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCondition : MonoBehaviour
{
    public GameObject brian;
    public float distanceBrian;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distanceBrian = Vector3.Distance(transform.position, brian.transform.position);
        if (distanceBrian < 1.3)
        {
            Debug.Log("Yah kalah nih");
            SceneManager.LoadScene("Lose Menu");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
