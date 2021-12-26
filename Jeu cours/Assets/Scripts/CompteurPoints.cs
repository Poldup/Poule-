using UnityEngine;

public class CompteurPoints : MonoBehaviour
{
    private int eggs;

    void Update()
    {
        eggs = GameManager.Instance.eggs;
        transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = eggs.ToString();
    }
}
