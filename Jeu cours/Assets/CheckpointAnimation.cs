using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointAnimation : MonoBehaviour
{
    private bool triggered;
    private void Awake()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }

    void Update()
    {
        triggered = transform.parent.gameObject.transform.GetChild(0).gameObject.GetComponent<CheckpointReach>().triggered;
        if (triggered)
        { gameObject.GetComponent<Animator>().enabled = true; }
    }
}
