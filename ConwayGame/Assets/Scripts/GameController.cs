using UnityEngine;

public class GameController : MonoBehaviour
{
    public int GridColumns = 25;
    public int GridRows = 25;
    public Grid gridReference;
    public Camera Camera;

    private float time;
    public float gameTickPeriod = 0.05f;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= gameTickPeriod)
        {
            time = 0.0f;
            GameTick();
        }
    }

    private void Awake()
    {
        gridReference.CreateGrid(GridRows, GridColumns);
        Camera.transform.position = new Vector3(GridColumns / 2, GridRows / 2, -10); //Center the camera around the grid
    }

    private void GameTick()
    {
        for (int y = 0; y < GridRows; y++)
        {
            for (int x = 0; x < GridColumns; x++)
            {
                gridReference.CalculateNeighbours(1, x, y);
                gridReference.SetCellStatus(x, y);
            }
        }
    }
}
