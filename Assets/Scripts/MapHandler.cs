using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour {
    [SerializeField]
    GameObject centerMap;

    [SerializeField]
    GameObject right;

    [SerializeField]
    GameObject left;

    [SerializeField]
    GameObject up;

    [SerializeField]
    GameObject down;

    [SerializeField]
    Camera video;


    public float step = 20;

    
   

    //from the inspector we assigned the QUAD
    public double tileX, tileY;
    //no las usamos ahora
    public int zoom=14;
    public float centerMapLat = 41.391428f;
    public float centerMapLon = 2.195363f;

    GameObject gpsHandlePointer;

    void Start()
    {
        
        gpsHandlePointer = GameObject.Find("GpsHandler");
        //WorldToTilePos((float)gpsHandlePointer.GetComponent<GpsHandler>().userL.Latitude, (float)gpsHandlePointer.GetComponent<GpsHandler>().userL.Longitude, zoom);
        //comentado ya que se usará mas adelante

        WorldToTilePos( centerMapLon, centerMapLat, zoom);
        Debug.Log("Tiles----- " + tileX + "/" + tileY);

        StartCoroutine(LoadTile(Mathf.FloorToInt((float)tileX), Mathf.FloorToInt((float)tileY), centerMap));
     

        StartCoroutine(LoadTile(Mathf.FloorToInt((float)tileX)+1, Mathf.FloorToInt((float)tileY), right));
        
        StartCoroutine(LoadTile(Mathf.FloorToInt((float)tileX) - 1, Mathf.FloorToInt((float)tileY), left));

        StartCoroutine(LoadTile(Mathf.FloorToInt((float)tileX), Mathf.FloorToInt((float)tileY) -1, up));

        StartCoroutine(LoadTile(Mathf.FloorToInt((float)tileX), Mathf.FloorToInt((float)tileY) +1, down));

        
    }
    public void WorldToTilePos(double lon, double lat, int zoom)
    {
        tileX = (double)((lon + 180.0f) / 360.0f * (1 << zoom));
        tileY = (double)((1.0f - Mathf.Log(Mathf.Tan((float)lat * Mathf.PI/ 180.0f) + 1.0f / Mathf.Cos((float)lat * Mathf.PI / 180.0f)) / Mathf.PI) /2.0f * (1 << zoom));
    }

    IEnumerator LoadTile(int x, int y, GameObject mapPlane)
    {
        //permite realizar la operación varias veces. Es necessari fer servir el startCoroutine, ja que es fa servir un yeld i per tant, estarem esperant
        string url = "http://a.tile.openstreetmap.org/" + zoom + "/" + x+ "/" + y + ".png";
        WWW www = new WWW(url);
        yield return www;
        Texture2D texture = new Texture2D(2, 2, TextureFormat.ARGB32, true);
        www.LoadImageIntoTexture(texture);
        mapPlane.GetComponent<Renderer>().material.mainTexture = texture;
    }

 
    public void ZoomInMap()
    {
        zoom++;

        if(zoom > 19)
        {
            zoom = 19;
        }

        WorldToTilePos(centerMapLon, centerMapLat, zoom);

        StartCoroutine(LoadTile(Mathf.FloorToInt((float)tileX), Mathf.FloorToInt((float)tileY), centerMap));

        StartCoroutine(LoadTile(Mathf.FloorToInt((float)tileX) + 1, Mathf.FloorToInt((float)tileY), right));

        StartCoroutine(LoadTile(Mathf.FloorToInt((float)tileX) - 1, Mathf.FloorToInt((float)tileY), left));

        StartCoroutine(LoadTile(Mathf.FloorToInt((float)tileX), Mathf.FloorToInt((float)tileY) - 1, up));

        StartCoroutine(LoadTile(Mathf.FloorToInt((float)tileX), Mathf.FloorToInt((float)tileY) + 1, down));

    }
    public void ZoomOutMap()
    {
        zoom--;

        if (zoom < 0)
        {
            zoom = 0;
        }

        WorldToTilePos(centerMapLon, centerMapLat, zoom);

        StartCoroutine(LoadTile(Mathf.FloorToInt((float)tileX), Mathf.FloorToInt((float)tileY), centerMap));

        StartCoroutine(LoadTile(Mathf.FloorToInt((float)tileX) + 1, Mathf.FloorToInt((float)tileY), right));

        StartCoroutine(LoadTile(Mathf.FloorToInt((float)tileX) - 1, Mathf.FloorToInt((float)tileY), left));

        StartCoroutine(LoadTile(Mathf.FloorToInt((float)tileX), Mathf.FloorToInt((float)tileY) - 1, up));

        StartCoroutine(LoadTile(Mathf.FloorToInt((float)tileX), Mathf.FloorToInt((float)tileY) + 1, down));

    }
    public void moveRight() {
        Debug.Log("se ha movido a la derecha");
        //Camera.main.gameObject.transform.Translate(step, 0, 0);
        
    }

    public void moveLeft(){
        Debug.Log("se ha movido a la izquierda");
        //Camera.main.gameObject.transform.Translate(-step, 0, 0);
    }
}
