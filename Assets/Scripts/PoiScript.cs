using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiScript : MonoBehaviour {
    [SerializeField]
    double lat;
    [SerializeField]
    double lon;

    GameObject mapHandlerPointer;
    // Use this for initialization
    IEnumerator Start () {

        yield return new WaitForSeconds(3);
        Debug.Log("Pos lat/lon" + lat + "/" + lon);
        mapHandlerPointer = GameObject.Find("MapHandler");
        int x = Mathf.FloorToInt((float)mapHandlerPointer.GetComponent<MapHandler>().tileX);
        int y = Mathf.FloorToInt((float)mapHandlerPointer.GetComponent<MapHandler>().tileY);
        int zoom = mapHandlerPointer.GetComponent<MapHandler>().zoom;

        double a = DrawCubeX(lon, TileToWorldPos(x, y, zoom).X, TileToWorldPos(x + 1, y, zoom).X);
        double b = DrawCubeY(lat, TileToWorldPos(x, y + 1, zoom).Y, TileToWorldPos(x, y, zoom).Y);

        Debug.Log(" Pos" + a + "/" + b);
        this.transform.position = new Vector3((float)a, (float)b, 0.0f);
    }

   
    public struct Point
    {
        public double X;
        public double Y;
    }

 
    // X -> longitud
    // Y -> latitud
    // devuelve la esquina superior izquierda del tile
    public Point TileToWorldPos(double tile_x, double tile_y, int zoom)
    {
        Point p = new Point();
        double n = System.Math.PI - ((2.0 * System.Math.PI * tile_y) / System.Math.Pow(2.0, zoom));

        p.X = ((tile_x / System.Math.Pow(2.0, zoom) * 360.0) - 180.0);
        p.Y = (180.0 / System.Math.PI * System.Math.Atan(System.Math.Sinh(n)));

        return p;
    }

    public double DrawCubeY(double targetLat, double minLat, double maxLat)
    {
        double pixelY = ((targetLat - minLat) / (maxLat - minLat)) ;
        return pixelY;
    }

    public double DrawCubeX(double targetLong, double minLong, double maxLong)
    {
        double pixelX = ((targetLong - minLong) / (maxLong - minLong)) ;
        return pixelX;
    }

}
