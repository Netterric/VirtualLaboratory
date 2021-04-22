using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Oil : MonoBehaviour
{
    [Header("Параметры масел")]
    //В старте подхватывать данные из бд
    public float oilV;//Вязкость
    public float oilTemp;//Температуа
    public float oilTime;//Время обработки в секундах
    public float viscIndex;//Постоянная вискозиметра
}
