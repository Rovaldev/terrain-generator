using UnityEngine;

public class Room
{
    public Vector2Int bottomLeftCorner;
    public Vector2Int topRightCorner;

    public Room(Vector2Int bottomLeftCorner, Vector2Int topRightCorner)
    {
        this.bottomLeftCorner = bottomLeftCorner;
        this.topRightCorner = topRightCorner;
    }

    //Propiedad para conocer la anchura
    public int Width
    {
        get { return topRightCorner.x - bottomLeftCorner.x; }
    }

    //Propiedad para conocer la longitud
    public int Length
    {
        get { return topRightCorner.y - bottomLeftCorner.y; }
    }
}
