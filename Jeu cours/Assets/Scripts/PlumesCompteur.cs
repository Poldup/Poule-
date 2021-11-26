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
    private int totPlumes;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        plumes = Mathf.Clamp(GameManager.Instance.plumes, 0, 100);
        goldenPlumes = GameManager.Instance.goldenPlumes;
        totPlumes = PlayerControler.Instance.comptPlumes;
        DisplayPlumes();

    }

    void DisplayPlumes()
    {
        
        if (activePlumes < totPlumes)
        {
            
            for (int i = activePlumes; i < totPlumes; i++)
            {
                
                if (i<goldenPlumes)
                {
                    transform.GetChild(i).GetComponent<Image>().sprite = golden;
                }
                else
                {
                    transform.GetChild(i).GetComponent<Image>().sprite = white;
                }
                transform.GetChild(i).gameObject.SetActive(true);

            }
            activePlumes = totPlumes;
        }

        if (activePlumes > totPlumes)
        {
            
            for (int i = activePlumes; i >= totPlumes; i--)
            {
                if (i>=goldenPlumes)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    transform.GetChild(i).GetComponent<Image>().sprite = empty;
                }
                
            }
            activePlumes = totPlumes;
        }
    }


        void PerdredPlume(int cb)
    {

    }
}
