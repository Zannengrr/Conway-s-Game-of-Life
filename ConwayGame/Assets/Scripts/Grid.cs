using UnityEngine;

public class Grid : MonoBehaviour
{
    private Cell[,] gridData;
    public Cell CellPrefab = null;

    public int AliveTreshold = 75;

    public void CreateGrid(int gridRows, int gridColumns)
    {
        gridData = new Cell[gridColumns, gridRows];
        for (int y = 0; y < gridRows; y++)
        {
            for (int x = 0; x < gridColumns; x++)
            {
                Cell cell= Instantiate(CellPrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
                cell.name = $"X:{x} - Y:{y}";
                cell.SetAlive(RandomCellAlive());
                gridData[x, y] = cell;
            }
        }
    }

    private bool RandomCellAlive()
    {
        int randomNumber = Random.Range(0, 100);

        return randomNumber > AliveTreshold;
    }

    public void CalculateNeighbours(int distance, int x, int y)
    {
        gridData[x, y].Neighbours = 0;
        int gridRowLimit = gridData.GetLength(1);
        int gridColumnLimit = gridData.GetLength(0);
        for (int i = Mathf.Max(0, y - distance); i <= Mathf.Min(y + distance, gridRowLimit - 1); i++)
        {
            for (int j = Mathf.Max(0, x - distance); j <= Mathf.Min(x + distance, gridColumnLimit - 1); j++)
            {
                if ((j != x || i != y) && gridData[j, i].isAlive) gridData[x, y].Neighbours++;
            }
        }
    }

    public void SetCellStatus(int x, int y)
    {
        Cell cell = gridData[x, y];
        if (cell.isAlive)
        {
            if (cell.Neighbours != 2 && cell.Neighbours != 3) cell.SetAlive(false);
        }
        else
        {
            if (cell.Neighbours == 3) cell.SetAlive(true);
        }
    }
}
