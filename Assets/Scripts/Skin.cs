using UnityEngine;

[System.Serializable]
public class Skin
{
    public int id;
    public int cost;
    public Sprite sprite;

    public Skin(int id, int cost, Sprite sprite)
    {
        this.id = id;
        this.cost = cost;
        this.sprite = sprite;
    }
}
