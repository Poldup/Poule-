using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagement : MonoBehaviour
{
    public static AnimationManagement Instance;
    private int lives;
    public RuntimeAnimatorController normal;
    public RuntimeAnimatorController life0;
    public RuntimeAnimatorController life1;
    public RuntimeAnimatorController life2;
    public RuntimeAnimatorController next;

    private void Start()
    {
        next = normal;
    }

    // Update is called once per frame
    void Update()
    {
        Instance = this;
        lives = GameManager.Instance.lives;
        LifeStateDisplay();
    }

    void LifeStateDisplay()
    {
        RuntimeAnimatorController current;
        current = transform.gameObject.GetComponent<Animator>().runtimeAnimatorController;
        if (lives==0)
        {
            next = life0;
        }
        if (lives == 1)
        {
            next = life1;
        }
        if (lives == 2)
        {
            next = life2;
        }
        if (lives == 3)
        {
            next = normal;
        }
        if (next!= current)
        {
            transform.gameObject.GetComponent<Animator>().runtimeAnimatorController = next;
        }

    }

}
