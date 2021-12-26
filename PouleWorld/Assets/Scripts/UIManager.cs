using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject panelFeatherSilver;
    public GameObject featherSilverImage;
    public GameObject featherGoldImage;
    public TMP_Text textScore;

    private SceneVariables VARIABLES;

    void Start()
    {
        VARIABLES = GameObject.Find("Scene Variables").GetComponent<SceneVariables>();
    }

    void Update()
    {
        foreach (Transform child in panelFeatherSilver.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < VARIABLES.nbFeatherSilver; i++)
        {
            var newImage = Instantiate(featherSilverImage, new Vector3(0, 0, 0), Quaternion.identity);
            newImage.transform.SetParent(panelFeatherSilver.transform);
        }

        for (int i = 0; i < VARIABLES.nbFeatherGold; i++)
        {
            var newImage = Instantiate(featherGoldImage, new Vector3(0, 0, 0), Quaternion.identity);
            newImage.transform.SetParent(panelFeatherSilver.transform);
        }

        textScore.text = VARIABLES.nbStrawberry + "";
    }

}
