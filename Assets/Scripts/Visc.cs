using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visc : Oil
{
    [Header("Количество масла")]
    [Range(0, 100)]
    public float oil = 1;
    [Header("Настройки вискозиметра")]
    [SerializeField] GameObject Ray;
    [SerializeField] GameObject Viscometer;
    [SerializeField] GameObject termoPosition;
    [Header("Управление")]
    [SerializeField] GameObject Button;
    public GameObject StartMesButton;
    [SerializeField] GameObject ContMesButton;
    [Header("Дополнительные объекты")]
    public GameObject Syringe;
    [SerializeField] GameObject Funnel;//воронка
    [SerializeField] Countdown Timer;
    public bool heated = false;
    bool startedMes = false;
    bool continuedMes = false;
    [Header("Параметры Материалов/Шейдеров")]
    [SerializeField] Renderer rend1;
    [SerializeField] Renderer rend2;
    [SerializeField] Shader sh1;
    [SerializeField] Renderer rendGlass;
    [SerializeField] Renderer rendGlass2;
    
    [Header("Установка вискозиметра")]
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    bool isFunnel = true;
    [SerializeField]
    bool setup = false;
    float timeconst;
    [SerializeField] MainProcess mp;
    RaycastHit hit;
    float currentLerpTime;
    float lerpTime;
    private void Start()
    {
        Button.SetActive(false);
        isFunnel = true;
        //rend1.material.shader = sh1;
        //rend1.material.shader = Shader.Find("Unlit/SpecialFX/Liquid");
        rend2.material.shader = Shader.Find("Unlit/SpecialFX/Liquid");
        timeconst = oilTime;
        lerpTime = 1f;
        
    }
    private void Update()
    {
        //float fill = Mathf.Lerp(0.35f,0.65f,(oil/100)+0.05f);
        //rend1.material.SetFloat("_FillAmount",fill);

        if(startedMes)
        {
            currentLerpTime += Time.deltaTime/4f;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }

            //Начало, подъём масла вверх
            float perc = currentLerpTime/lerpTime;
            float fill2 = Mathf.Lerp(0.635f,0.43f,perc);
            rend2.material.SetFloat("_FillAmount",fill2);
            if (rend2.material.GetFloat("_FillAmount") == 0.43f)
            {
                startedMes = false;
            }
        }

        if(continuedMes)
        {
            //Продолжение, истекание масла
            float fill3 = Timer.curTime / Timer.timeRemaining;
            float decreasefill = Mathf.Lerp(0.43f,0.485f,fill3);
            rend2.material.SetFloat("_FillAmount",decreasefill);
            if (rend2.material.GetFloat("_FillAmount") == 0.485f)
            {
                continuedMes = false;
            }
        }
    }
    private void OnMouseOver()
    {
        if (90 < oil & oil < 105)
        {
            Button.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        Button.SetActive(false);
    }
    void FixedUpdate()
    {
        Load();
        Setup();
        RayCast();
    }
    void RayCast()
    {
        int cubeLayerIndex = LayerMask.NameToLayer("Tank");
        int tank = (1 << cubeLayerIndex);
        if (Vector3.Dot(transform.up, Vector3.down) > 0)
        {
            Physics.Raycast(Ray.transform.position, -Vector3.up, out hit);
            Debug.DrawLine(Ray.transform.position, hit.point, Color.cyan);

            if (Physics.Raycast(Ray.transform.position, -Vector3.up, out hit, tank))
            {
                if (0 <= oil & oil <= 100)
                {
                    Debug.Log($"Осталось: {oil}");
                    hit.transform.SendMessage("HitByRay");
                    oil -= 10 * Time.deltaTime;
                }
            }
        }
    }
    void HitByRay()
    {
        if (0 <= oil && oil <= 100 && isFunnel)
        {
            //Debug.Log($"Налито: {oil}");
            oil += 10 * Time.deltaTime;
            this.oilV = base.oilV;
            this.oilTemp = base.oilTemp;
            this.oilTime = base.oilTime;

        }
    }
    void Load()
    {

        if (Input.GetButtonDown("IsFunnel"))
        {
            isFunnel = !isFunnel;
        }
        Funnel.SetActive(isFunnel);
    }
    void Setup()
    {
        if (setup)
        {
            isFunnel = false;
            Vector3 directionToMove = termoPosition.transform.position - transform.position;
            directionToMove = directionToMove.normalized * Time.deltaTime * moveSpeed;
            float maxDistance = Vector3.Distance(transform.position, termoPosition.transform.position);
            transform.position = transform.position + Vector3.ClampMagnitude(directionToMove, maxDistance);
            Viscometer.transform.parent = termoPosition.transform;
            mp.SetCloseLook();
            mp.currViscIndex = viscIndex;
            mp.currOilTime = oilTime;
        }
    }
    public void SetSetup()
    {
        setup = !setup;
        mp.ViscHint();
    }
    public void StartMeasuring()
    {
        startedMes = true;
        StartMesButton.SetActive(false);
        ContMesButton.SetActive(true);
    }
    public void ContinueMeasuring()
    {
        continuedMes = true;
        Timer.timeRemaining = oilTime;
        Timer.timerIsRunning = true;
        ContMesButton.SetActive(false);
    }
    
}