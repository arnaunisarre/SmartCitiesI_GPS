using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GpsHandler : MonoBehaviour {


	[SerializeField]
	private Text userLocationText;


    public double Latitude;
    public double Longitude;


    public bool access = false;


    // Use this for initialization
    IEnumerator Start()
    {
        access = false;


        Latitude = 0.0f;
        Longitude = 0.0f;
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)

        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            access = true;
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        //Input.location.Stop();
    }
    // Update is called once per frame
    void Update () {

        if (access)
        {
            Latitude = (double)Input.location.lastData.latitude;
            Longitude = (double)Input.location.lastData.longitude;
            userLocationText.text = Latitude + "," + Longitude;
        }
    }

	

	/*private double CalculateDistance(Position pointA, Position pointB)
    {
        double earthRadius = 6371;
        double radiantsFactor = System.Math.PI / 180;

        double diffLat = (pointB.Latitude - pointA.Latitude) * radiantsFactor;
        double diffLng = (pointB.Longitude - pointA.Longitude) * radiantsFactor;
        double a = System.Math.Pow(System.Math.Sin(diffLat / 2), 2) +
                    System.Math.Cos(pointA.Latitude * radiantsFactor) *
                    System.Math.Cos(pointB.Latitude * radiantsFactor) *
                    System.Math.Pow(System.Math.Sin(diffLng / 2), 2);
        double c = 2 * System.Math.Atan2(System.Math.Sqrt(a), System.Math.Sqrt(1 - a));
        double res = earthRadius * c;

        return res * 1000;
    }*/

}
