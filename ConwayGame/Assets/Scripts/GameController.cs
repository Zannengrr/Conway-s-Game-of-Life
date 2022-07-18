using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int GridColumns = 25;
    public int GridRows = 25;
    public Grid gridReference;
    public Camera Camera;

    private float time;
    public float gameTickPeriod = 0.05f;

    public Button ClearGridButton;
    public Button RestartSimulationButton;
    public Button PauseButton;

    private bool simulationRunning = false;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= gameTickPeriod && simulationRunning)
        {
            time = 0.0f;
            GameTick();
        }
    }

    private void Awake()
    {
        Init();
        gridReference.CreateGrid(GridRows, GridColumns);
        Camera.transform.position = new Vector3(GridColumns / 2, GridRows / 2, -10); //Center the camera around the grid
        StartSimulation();
    }

    private void Init()
    {
        ClearGridButton.onClick.AddListener(StopSimulation);
        RestartSimulationButton.onClick.AddListener(StartSimulation);
        PauseButton.onClick.AddListener(ToggleSimulationRunning);
    }

    private void GameTick()
    {
        for (int y = 0; y < GridRows; y++)
        {
            for (int x = 0; x < GridColumns; x++)
            {
                gridReference.CalculateNeighbours(1, x, y);
            }
        }

        for (int y = 0; y < GridRows; y++)
        {
            for (int x = 0; x < GridColumns; x++)
            {
                gridReference.SetCellStatus(x, y);
            }
        }
    }

    private void StartSimulation()
    {
        gridReference.RandomizeGrid();
        simulationRunning = true;
    }

    private void ToggleSimulationRunning()
    {
        simulationRunning = !simulationRunning;
    }

    private void StopSimulation()
    {
        gridReference.KillAllCells();
        simulationRunning = false;
    }
}
