using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTank : Oil
{
    [Header("Количество масла")]
    [Range (0, 100)]
    public float oil = 100;

    [Header("Параметры объекта")]
    public GameObject Ray;
    public GameObject Tank;
    RaycastHit hit;
    [SerializeField] GameObject button;
    [SerializeField] GameObject defaultPosition;
    [SerializeField] MainProcess mp;
    private void Start()
    {
        this.oilV = base.oilV;
        this.oilTemp = base.oilTemp;
        this.oilTime = base.oilTime;
    }
    void FixedUpdate()
    {
        int cubeLayerIndex = LayerMask.NameToLayer("Tank");
        int tank = (1 << cubeLayerIndex);
        if (Vector3.Dot(transform.up, Vector3.down) > 0)
        {
            Physics.Raycast(Ray.transform.position, -Vector3.up, out hit);
            Debug.DrawLine(Ray.transform.position, hit.point, Color.cyan);

            if (Physics.Raycast(Ray.transform.position, -Vector3.up, out hit, tank))
            {
                if (0 <= oil & oil <= 100) {
                    Debug.Log($"Осталось: {oil}");
                    hit.transform.SendMessage("HitByRay");
                    oil -= 10 * Time.deltaTime;
                }
            }
        }
    }
    void HitByRay()
    {
        if (0 <= oil & oil <= 100)
        {
            //Debug.Log($"Налито: {oil}");
            oil += 10 * Time.deltaTime;
        }
    }

    private void OnMouseOver()
    {
        button.SetActive(true);
    }
    private void OnMouseExit()
    {
       button.SetActive(false);
    }
    IEnumerator waiter()
    {
        mp.ChangeHint();
        transform.position = GameObject.Find("visc").transform.position+new Vector3(0,0.25f,-0.2f);
        transform.eulerAngles = new Vector3(100,0,0);
        yield return new WaitForSeconds(10);
        transform.position = defaultPosition.transform.position;
        transform.eulerAngles = new Vector3(0,0,0);
    }
    public void FuelVisc()
    {
        StartCoroutine(waiter());
    }
}
