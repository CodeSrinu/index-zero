using System.Data;
using UnityEngine;


[System.Serializable]
public class GridRegion
{

    public string gridRegion;
    public ConnectivityType connectivityType;


    [Header("Grid parameters")]
    public Vector3 localOrigin;
    public Transform originMaker;
    public int rows;
    public int columns;
    public float rowSpacing;
    public float columnSpacing;


    public float snapRange = 0.05f;

    [HideInInspector]
    public Bounds localBounds;


    public void CalculateBounds(Transform breadboardRoot)
    {
        if(originMaker != null)
        {
            localOrigin = breadboardRoot.InverseTransformPoint(originMaker.position);
        }

        Vector3 size = new Vector3(columns * columnSpacing, 0.01f, rows * rowSpacing);

        Vector3 center = localOrigin + new Vector3(size.x / 2, 0, size.z / 2);

        localBounds = new Bounds(center, size);
    }


    public enum ConnectivityType
    {
        columnsConnected,
        rowsConnected
    }

}
