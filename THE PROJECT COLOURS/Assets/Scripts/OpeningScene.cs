using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScene : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -7)//if the player went down in the pipe, load level1
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
