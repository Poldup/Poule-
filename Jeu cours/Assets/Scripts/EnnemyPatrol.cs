using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    public SpriteRenderer graphics;
    public bool autoFlip;
    private Transform target;
    private int destPoint =0;
    private bool padeja;
    public bool sound;
    public AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        // Si l'ennemi est quasiment arriv? ? sa destination
        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            if (autoFlip)
            {
                graphics.flipX = !graphics.flipX;
            }
            if (sound && padeja && destPoint == 0)
            {
                padeja = false;
                source.Play();
            }
        }
        if (Vector3.Distance(transform.position, target.position) > 0.35f)
        {
            padeja = true;
        }
    }
}
