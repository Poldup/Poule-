using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointAnimation : MonoBehaviour
{
    public bool triggered;
    private void Awake()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }

    void Update()
    {
        if (triggered)
        { gameObject.GetComponent<Animator>().enabled = true; }
    }
}
