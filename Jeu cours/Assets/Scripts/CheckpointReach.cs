using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointReach : MonoBehaviour
{
    public GameObject playerSpawn;
    public bool triggered;
    

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Poule"))
        {
            triggered = true;
            playerSpawn.transform.position = transform.position;
            foreach (GameObject egg in GameManager.Instance.pickedEggs)
            {
                Destroy(egg);
            }
            GameManager.Instance.pickedEggs.Clear();
            foreach (GameObject feather in GameManager.Instance.pickedGFeathers)
            {
                Destroy(feather);
            }
            GameManager.Instance.pickedGFeathers.Clear();
            Destroy(gameObject);
        }
    }
}
