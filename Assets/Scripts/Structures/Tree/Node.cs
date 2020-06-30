using UnityEngine;

public class Node
{
    public Node left;
    public Node right;
    public Room room;

    public Node(Vector2Int bottomLeftCorner, Vector2Int topRightCorner)
    {
        //Crea un nuevo cuarto
        room = new Room(bottomLeftCorner, topRightCorner);
    }
}
