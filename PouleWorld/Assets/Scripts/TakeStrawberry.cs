using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeStrawberry : MonoBehaviour
{

    private bool activate = true;
    private SceneVariables VARIABLES;

    void Start()
    {
        VARIABLES = GameObject.Find("Scene Variables").GetComponent<SceneVariables>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (activate && collider.tag == "Player")
        {
            activate = false;

            VARIABLES.nbStrawberry++;
            Destroy(gameObject);
        }
    }

}
