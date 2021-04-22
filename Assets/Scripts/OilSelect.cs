using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject[] oilTypes;
    [SerializeField]
    private int selected = 0;
    
    public void Next()
    {
        oilTypes[selected].SetActive(false);
        selected = (selected + 1) % oilTypes.Length;
        oilTypes[selected].SetActive(true);

    }

    public void Previous()
    {
        oilTypes[selected].SetActive(false);
        selected--;
        if (selected < 0)
        {
            selected += oilTypes.Length;
        }
        oilTypes[selected].SetActive(true);
    }

    public void Select()
    {
        PlayerPrefs.SetInt("SelectedOil",selected);
    }
}
