using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainProcess : MonoBehaviour
{
    [Header("Текущее состояние")]
    public Stage stage;
    [Header("Количество повторений")]
    [Range(0, 3)]
    [SerializeField] int currentIteration;
    [Header("Ближнее рассмотрение")]
    [SerializeField] GameObject closeLookImage; 
    [Header("Термостат")]
    [SerializeField] Thermostat thermo;
    [Header("Положение по-умолчанию")]
    [SerializeField] Transform defaultOilPosition;
    [SerializeField] Transform defaultViscPosition;
    [Header("Установка температуры")]
    [SerializeField] GameObject tempInput;
    [SerializeField] InputField tempInputField;
    //Кнопки управления
    [Header("Кнопки управления")]
    [SerializeField]
    public GameObject NextButt;
    [SerializeField]
    public GameObject PrevButt;
    //Меню выбора масла
    [Header("Меню выбора масла")]
    [SerializeField]
    public GameObject SelectOilBut;
    [SerializeField]
    private GameObject oilHandler;
    [SerializeField]
    private GameObject[] oilTypes;
    [SerializeField]
    private int selected = 0;
    //меню выбора вискозиметра
    [Header("Меню выбора вискозиметра")]
    [SerializeField]
    public GameObject SelectViscButt;
    [SerializeField]
    private GameObject viscHandler;
    [SerializeField]
    private GameObject[] viscTypes;
    [SerializeField]
    private int viscSelected = 0;
    //Подсказки
    [Header("Подсказки")]
    [SerializeField] GameObject hint1;
    [SerializeField] GameObject hint2;
    [SerializeField] GameObject hint3;
    [SerializeField] GameObject hint4;
    [SerializeField] GameObject hint5;

    public float currViscIndex;
    public float currOilTime;
    //Состояния
    public enum Stage
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth
    }
    public void SetStage(Stage value)
    {
        switch (stage)
        {
            case Stage.First:
                break;
            case Stage.Second:
                break;
            case Stage.Third:
                break;
            case Stage.Fourth:
                break;
            case Stage.Fifth:
                break;
        }
        stage = value;
    }
    private void Start()
    {
        StageFirst();
        currentIteration = 0;
        hint1.SetActive(false);
        hint2.SetActive(false);
        hint3.SetActive(false);
        hint4.SetActive(false);
        hint5.SetActive(false);
    }
    public void StageFirst()
    {
        oilHandler.SetActive(true);
        viscHandler.SetActive(false);
        //SelectOilBut.SetActive(true);
        //SelectViscButt.SetActive(false);
        //NextButt.SetActive(true);
        //PrevButt.SetActive(true);
        SetStage(Stage.First);
    }
    public void StageSecond()
    {
        oilHandler.SetActive(false);
        SelectOilBut.SetActive(false);
        viscHandler.SetActive(true);
        SelectViscButt.SetActive(true);
        NextButt.SetActive(true);
        PrevButt.SetActive(true);
        SetStage(Stage.Second);
    }
    public void StageThird()
    {
        oilHandler.SetActive(false);
        viscHandler.SetActive(false);
        SelectOilBut.SetActive(false);
        SelectViscButt.SetActive(false);
        NextButt.SetActive(false);
        PrevButt.SetActive(false);
        GameObject oilTank = Instantiate(oilTypes[selected], defaultOilPosition.position, defaultOilPosition.rotation);
        oilTank.transform.Find("Canvas").gameObject.SetActive(false);
        oilTank.name = "oilTank";
        GameObject visc = Instantiate(viscTypes[viscSelected], defaultViscPosition.position, defaultViscPosition.rotation);
        visc.transform.Find("CanvasDescription").gameObject.SetActive(false);
        visc.name = "visc";
        hint1.SetActive(true);
        SetStage(Stage.Third);
    }
    public void ChangeHint()
    {
        hint1.SetActive(false);
        hint2.SetActive(true);
    }
    public void StageFourth()
    {
        SelectOilBut.SetActive(false);
        SelectViscButt.SetActive(false);
        NextButt.SetActive(false);
        PrevButt.SetActive(false);
        currentIteration += 1;
        hint3.SetActive(false);
        hint4.SetActive(true);
        SetStage(Stage.Fourth);
    }
    public void StageFifth()
    {
        SetStage(Stage.Fifth);

    }
    public void lastHint(){
        hint4.SetActive(false);
        hint5.SetActive(true);
        hint5.GetComponent<Text>().text = "Вязкость масла равна = " + (currViscIndex*currOilTime);
    }

    public void SetTime()
    {

        for (int i = 0; i < 3; i++)
        {
            if(int.Parse(tempInputField.text) == 40)
            {
                thermo.timeField = 40;
                thermo.Heat();
                tempInput.SetActive(false);
                break;
            }
            else
            {

            }
        }
    }

    public void SetCloseLook()
    {
        closeLookImage.SetActive(true);
    }
    public void ViscHint(){
        hint2.SetActive(false);
        hint3.SetActive(true);
    }
    public void CloseCloseLook()
    {
        closeLookImage.SetActive(false);
    }

    //Кнопки для меню выбора масла
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
        StageSecond();
    }
    //Меню выбора вискозиметра
    public void NextVisc()
    {
        viscTypes[viscSelected].SetActive(false);
        viscSelected = (viscSelected + 1) % viscTypes.Length;
        viscTypes[viscSelected].SetActive(true);
    }
    public void PreviousVisc()
    {
        viscTypes[viscSelected].SetActive(false);
        viscSelected--;
        if (viscSelected < 0)
        {
            viscSelected += viscTypes.Length;
        }
        viscTypes[viscSelected].SetActive(true);
    }
    
    public void TimeInput()
    {
        tempInput.SetActive(true);
    }

    public void SelectVisc()
    {
        for (int i = 0; i < 3; i++)
        {
            if (oilTypes[selected].GetComponent<OilTank>().viscIndex == viscTypes[viscSelected].GetComponent<Visc>().viscIndex)
            {
                viscHandler.SetActive(false);
                SelectViscButt.SetActive(false);
                StageThird();
                break;
            }
        }
    }
}
