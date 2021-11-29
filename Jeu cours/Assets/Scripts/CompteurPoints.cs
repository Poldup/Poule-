using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompteurPoints : MonoBehaviour
{
    private int fraises;
    
    



    
    void Update()
    {
        if (fraises < 1)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        fraises = GameManager.Instance.fraises;
        transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = fraises.ToString();
    }
}
