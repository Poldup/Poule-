using UnityEngine;

public class CompteurPoints : MonoBehaviour
{
    private int eggs;
    private bool activated;
    void Awake()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        
        eggs = GameManager.Instance.eggs;
        if (eggs >1 && !activated)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            activated = true;
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = eggs.ToString();
    }
}
