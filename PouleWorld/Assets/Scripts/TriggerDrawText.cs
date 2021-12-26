using TMPro;
using UnityEngine;

public class TriggerDrawText : MonoBehaviour
{
    public GameObject[] textObjects;

    private TMP_Text[] textComponents;

    void Start()
    {
        textComponents = new TMP_Text[textObjects.Length];
        for (int i = 0; i < textObjects.Length; i++)
        {
            textComponents[i] = textObjects[i].GetComponent<TMP_Text>();
            textComponents[i].enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            foreach (TMP_Text text in textComponents)
            {
                text.enabled = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            foreach (TMP_Text text in textComponents)
            {
                text.enabled = false;
            }
        }
    }
}
