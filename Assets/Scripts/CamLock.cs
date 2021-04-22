using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLock : MonoBehaviour
{
    public GameObject CameraMain;
    public GameObject CameraTransform;
    public GameObject SecondCameraTransform;
    public GameObject Head;
    private Camera cam;

    [SerializeField] GameObject hint;

    [SerializeField]
    private FirstPersonAIO Fps;

    [SerializeField] MainProcess mp;

    RaycastHit hit;

    bool Move = false;
    bool lookTo = false;
    private void Start()
    {
        cam = CameraMain.GetComponent<Camera>();
        Move = true;
        lookTo = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if(Move == true)
        {
            CameraMain.transform.position = Head.transform.position;
            CameraMain.transform.rotation = Head.transform.rotation;
            Fps.playerCanMove = true;
            Fps.enableCameraMovement = true;
            Fps.lockAndHideCursor = true;
            hint.SetActive(true);
        }
        if (Move == false)
        {
            CameraMain.transform.position = CameraTransform.transform.position;
            CameraMain.transform.rotation = CameraTransform.transform.rotation;
            Fps.lockAndHideCursor = false;
            Fps.playerCanMove = false;
            Fps.enableCameraMovement = false;
            hint.SetActive(false);
            if(lookTo == true)
            {
                CameraMain.transform.position = SecondCameraTransform.transform.position;
                CameraMain.transform.rotation = SecondCameraTransform.transform.rotation;
            }
            else
            {
                CameraMain.transform.position = CameraTransform.transform.position;
                CameraMain.transform.rotation = CameraTransform.transform.rotation;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
            mp.SelectOilBut.SetActive(true);
            mp.SelectViscButt.SetActive(false);
            mp.NextButt.SetActive(true);
            mp.PrevButt.SetActive(true);
    }
    private void OnTriggerExit(Collider other) {
        hint.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("CamMove"))
        {
            Move = !Move;
        }
        if (Input.GetButtonDown("LookTo"))
        {
            lookTo = !lookTo;
        }
    }
}
