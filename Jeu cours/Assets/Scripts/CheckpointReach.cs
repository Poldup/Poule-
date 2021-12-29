using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointReach : MonoBehaviour
{
    public GameObject playerSpawn;
    

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Poule"))
        {
            transform.parent.gameObject.transform.GetChild(1).gameObject.GetComponent<CheckpointAnimation>().triggered = true;
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
