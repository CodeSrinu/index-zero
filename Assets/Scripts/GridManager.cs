using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 5;
    public int columns = 5;

    public float snapRange = 0.1f;
    public float spacing = 0.2f;


    public bool showGizmos;
    private Vector3 gridOrigin;

    private void Start()
    {
        gridOrigin = transform.position;
    }

    public bool TryGetSnapPoint(Vector3 objWorldPos, out Vector3 snapPoint)
    {
        Vector3 localPos = objWorldPos - gridOrigin;

        int col = Mathf.RoundToInt(localPos.x / spacing);
        int row = Mathf.RoundToInt(localPos.y / spacing);

        col = Mathf.Clamp(col, 0, columns - 1);
        row = Mathf.Clamp(row, 0, rows - 1);

        snapPoint = gridOrigin + new Vector3(col * spacing, 0, row * spacing);

        float distance = Vector3.Distance(objWorldPos, snapPoint);
        return distance <= snapRange;

    }


    private void OnDrawGizmos()
    {
        Vector3 origin = Application.isPlaying ? gridOrigin : transform.position;
        Gizmos.color = Color.yellow;

        for(int col = 0; col < rows;  col++)
        {
            for (int row = 0; row < columns; row++)
            {
                Vector3 pointPos = gridOrigin + new Vector3(col * spacing, 0, row * spacing);
                Gizmos.DrawWireSphere(pointPos, snapRange);
            }
        }
    }
}
