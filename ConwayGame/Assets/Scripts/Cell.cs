using UnityEngine;

public class Cell: MonoBehaviour
{
    public int Neighbours = 0;
    public bool isAlive = false;
    public Vector2Int Position;
    public SpriteRenderer Sprite;

    public void SetAlive(bool alive)
    {
        isAlive = alive;
        Sprite.enabled = alive;
    }
}
