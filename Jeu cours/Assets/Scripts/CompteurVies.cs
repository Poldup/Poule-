using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompteurVies : MonoBehaviour
{
    private int lives;
    void Update()
    {
        lives = Mathf.Clamp(GameManager.Instance.heart, 0, 5);
        transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = lives.ToString();
    }
}
