using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class gpsLocation : MonoBehaviour
{

    public Text GPSstatus;
    public Text permission;
    public Text LatitudeValue;
    public Text LongitudeValue;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPSLoc());
    }
    IEnumerator GPSLoc()
    {
        if (!Input.location.isEnabledByUser)
        {
            permission.text = "permission is not enabled";
            Debug.Log("Permission is Not Enabled");
            yield break;
        }
        else
        {
            permission.text = "Permission is Enabled";
        }
        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait>0) { 
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if(maxWait < 1)
        {
            GPSstatus.text = "Time out";
            Debug.Log("Time Out");
            yield break;
        }
        if(Input.location.status == LocationServiceStatus.Failed)
        {
            GPSstatus.text = "GPS is not working";
            Debug.Log("GPS is not working");
            yield break;
        }
        else
        {
            Debug.Log("GPS is working");
            GPSstatus.text = "GPS is working";
            InvokeRepeating("GpsUpdate", 0.5f, 1f);
        }
    }

    private void GpsUpdate()
    {
        if(Input.location.status == LocationServiceStatus.Running)
        {
            Debug.Log("GPS is running");
            GPSstatus.text = "GPS is running";
            LatitudeValue.text = Input.location.lastData.latitude.ToString();
            LongitudeValue.text = Input.location.lastData.longitude.ToString();

        }
        else
        {
            GPSstatus.text = "STOP";
        }
    }
    
}
