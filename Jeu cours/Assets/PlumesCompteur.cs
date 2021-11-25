using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlumesCompteur : MonoBehaviour
{
    public static PlumesCompteur Instance;
    public Sprite empty;
    public Sprite white;
    public Sprite golden;

    private int plumes;
    private int activePlumes;
    private int goldenPlumes;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        plumes = GameManager.Instance.plumes;
        goldenPlumes = GameManager.Instance.goldenPlumes;
        DisplayPlumes();

    }

    void DisplayPlumes()
    {
        if (activePlumes < plumes)
        {
            for (int i = activePlumes; i < plumes; i++)
            {

                transform.GetChild(i).gameObject.SetActive(true);

            }
            activePlumes = plumes;
        }
        if (activePlumes > plumes)
        {
            for (int i = plumes; i > activePlumes; i--)
            {

                transform.GetChild(i).gameObject.SetActive(false);

            }
            activePlumes = plumes;
        }
    }


        void PerdredPlume(int cb)
    {

    }
}
