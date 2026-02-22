using System.Data;
using UnityEngine;


[System.Serializable]
public class GridRegion
{
    [Header("Identity")]
    public string gridRegion;
    public ConnectivityType connectivityType;


    [Header("Grid Related parameters")]
    public Vector3 localOrigin;
    public Transform originMaker;
    public int rows;
    public int columns;
    public float rowSpacing;
    public float columnSpacing;

    public bool isSegmentedCol = false;
    public int segmentedColSize = 5;
    public float segmentedColSpacing = 0.12f;

    public float snapRange = 0.05f;

    [HideInInspector]
    public Bounds localBounds;


    public void CalculateBounds(Transform breadboardRoot)
    {
        if(originMaker != null)
        {
            localOrigin = breadboardRoot.InverseTransformPoint(originMaker.position);
        }

        float totalWidth;

        if (isSegmentedCol)
        {
            int lastCol = columns - 1;
            totalWidth = GetColumnXOffset(lastCol) + columnSpacing;
        }
        else
        {
            totalWidth = columns * columnSpacing;
        }


        Vector3 size = new Vector3(totalWidth, 0.01f, rows * rowSpacing);

        Vector3 center = localOrigin + new Vector3(size.x / 2, 0, size.z / 2);

        localBounds = new Bounds(center, size);
    }


    public float GetColumnXOffset(int col)
    {
        if (isSegmentedCol)
        {
            int colBlockIndex = col / segmentedColSize;
            return col * columnSpacing + colBlockIndex * segmentedColSpacing;
        }
        else
        {
            return col * columnSpacing;
        }
    }


    public enum ConnectivityType
    {
        columnsConnected,
        rowsConnected
    }

}
