using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    //Atributos
    public DynamicGridController gridController;
    public int minWidth;
    public int minLength;
    public int maxIterations;
    public int roomHeight;
    public int hallWidth = 2;
    public int roomLayer = 1;
    public Node root;

    //Metodo para comenzar a construir los cuartos
    public void StartRoomGeneration()
    {
        //Por defecto el tamaño del cuarto es del tamaño del mapa
        root = new Node(new Vector2Int(0, 0), new Vector2Int(gridController.width, gridController.length));

        //Genera el arbol con los cuartos
        root = GenerateTreeRoomNodes(root);

        //Crea lo cuartos
        CreateRooms(root);
    }

    //Metodo para construir los cuartos
    private void CreateRooms(Node root)
    {
        //Si root es nulo
        if(root == null)
        {
            return;
        }

        //Si es un nodo hoja
        if(root.left == null && root.right == null)
        {
            //Elimina los bloques que estan dentro de su volumen
            for(int x=root.room.bottomLeftCorner.x + 1; x<root.room.topRightCorner.x - 1; x++)
            {
                for(int y=roomLayer; y<roomHeight + roomLayer; y++)
                {
                    for (int z = root.room.bottomLeftCorner.y + 1; z < root.room.topRightCorner.y - 1; z++)
                    {
                        if (gridController.Grid.GetDataFromGridCoordinates(x, y, z) != null)
                        {
                            Destroy(gridController.Grid.GetDataFromGridCoordinates(x, y, z));
                        }
                    }
                }
            }
        }
        else
        {
            //Continua con su nodo izquierdo y despues el derecho
            CreateRooms(root.left);
            CreateRooms(root.right);

            //Crea los pasillos
            CreateHalls(root);
        }
    }

    //Metodo para construir los pasillos
    private void CreateHalls(Node root)
    {
        if (root.hall == null)
            return;

        //Debug.Log("From X: " + root.hall.bottomLeftCorner.x + " To X: " + root.hall.topRightCorner.x + " From Y: " + roomLayer + " To Y: " + (roomHeight + roomLayer) + " From Z: " + root.hall.bottomLeftCorner.y + " To Z: " + root.hall.topRightCorner.y);

        //Recorre todas sus coordenadas para eliminar los bloques
        for (int x = root.hall.bottomLeftCorner.x; x < root.hall.topRightCorner.x; x++)
        {
            for (int y = roomLayer; y < roomHeight + roomLayer; y++)
            {
                for (int z = root.hall.bottomLeftCorner.y; z < root.hall.topRightCorner.y; z++)
                {
                    //Obtiene el cubo que se encuentra en el pasillo
                    GameObject cube = gridController.Grid.GetDataFromGridCoordinates(x, y, z);

                    //Si no es nulo entonces lo elimina
                    if (cube)
                    {
                        Destroy(cube);
                        
                    }
                        
                }
            }
        }
    }

    //Metodo para generar los pasillos de un noto
    private Room GenerateHall(Node node)
    {
        //Si requiere un pasillo vertical
        if(node.left.room.bottomLeftCorner.x - node.right.room.bottomLeftCorner.x == 0)
        {
            //Genera una posicion aleatoria para el pasillo
            int hallPosition = Random.Range(hallWidth, node.room.Width - hallWidth);

            //Crea sus limites
            Vector2Int xLimits = new Vector2Int(node.left.room.bottomLeftCorner.x + hallPosition, node.left.room.bottomLeftCorner.x + hallPosition + hallWidth);
            Vector2Int zLimits = new Vector2Int(node.left.room.bottomLeftCorner.y + node.left.room.Length - hallWidth - 1, node.left.room.bottomLeftCorner.y + node.left.room.Length + hallWidth * 2 - 1);

            //APOYO VISUAL
            /*Vector3 bottomCorner = gridController.Grid.GetCellWorldPosition(xLimits.x, 20, zLimits.x);
            Vector3 topCorner = gridController.Grid.GetCellWorldPosition(xLimits.y, 20, zLimits.y);
            Debug.DrawLine(bottomCorner, topCorner, Color.red, 10);*/

            //Guarda el pasillo
            return new Room(new Vector2Int(xLimits.x, zLimits.x), new Vector2Int(xLimits.y, zLimits.y));
        }
        else
        {
            //Genera una posicion aleatoria para el pasillo
            int hallPosition = Random.Range(hallWidth, node.room.Length - hallWidth);

            //Crea sus limites
            Vector2Int xLimits = new Vector2Int(node.left.room.bottomLeftCorner.x + node.left.room.Width - hallWidth - 1, node.left.room.bottomLeftCorner.x + node.left.room.Width + hallWidth * 2 - 1);
            Vector2Int zLimits = new Vector2Int(node.left.room.bottomLeftCorner.y + hallPosition, node.left.room.bottomLeftCorner.y + hallPosition + hallWidth);

            //APOYO VISUAL
            /*Vector3 bottomCorner = gridController.Grid.GetCellWorldPosition(xLimits.x, 20, zLimits.x);
            Vector3 topCorner = gridController.Grid.GetCellWorldPosition(xLimits.y, 20, zLimits.y);
            Debug.DrawLine(bottomCorner, topCorner, Color.red, 10);*/

            //Guarda el pasillo
            return new Room(new Vector2Int(xLimits.x, zLimits.x), new Vector2Int(xLimits.y, zLimits.y));
        }
    }

    //Metodo para generar todos los cuartos del arbol
    private Node GenerateTreeRoomNodes(Node root)
    {
        //Va guardando los nodos en un Queue para iterarlos tambien, enlista al nodo root
        Queue<Node> pendingRooms = new Queue<Node>();
        pendingRooms.Enqueue(root);

        for (int i = 0; i < maxIterations; i++)
        {
            //Si aun hay nodos por procesar
            if(pendingRooms.Count > 0)
            {
                //Saca el nodo que sigue
                Node next = pendingRooms.Dequeue();

                //Comprueba si pudo dividir el cuarto
                if (SplitRoom(next))
                {
                    //Genera sus pasillos
                    next.hall = GenerateHall(next);

                    //Enlista a los nodos hijos
                    pendingRooms.Enqueue(next.left);
                    pendingRooms.Enqueue(next.right);
                }
            }
        }

        return root;
    }

    //Metodo para dividir un cuarto
    private bool SplitRoom(Node node)
    {
        //Determina la orientacion en la que se va a dividir (0: vertical, 1: horizontal)
        int orientation = Random.Range(0, 2);
        //int orientation = 0;

        //Si es horizontal y aun no llega al tamaño minimo vertical (*2 porque los cuartos resultantes tienen que respetar los limites)
        if(orientation == 0 && node.room.Width > minWidth * 2)
        {
            //Genera una division aleatoria
            int newWidth = Random.Range(minWidth, node.room.Width - minWidth);

            //Crea los nodos
            Node leftNode = new Node(node.room.bottomLeftCorner, new Vector2Int(node.room.bottomLeftCorner.x + newWidth, node.room.topRightCorner.y));
            Node rightNode = new Node(new Vector2Int(node.room.bottomLeftCorner.x + newWidth, node.room.bottomLeftCorner.y), node.room.topRightCorner);

            //Los guarda en sus nodos correspondientes
            node.left = leftNode;
            node.right = rightNode;

            return true;
        }

        //Si es vertical y aun no llega al tamaño minimo horizontal
        if(orientation == 1 && node.room.Length > minLength * 2)
        {
            //genera una division aleatoria
            int newLength = Random.Range(minLength, node.room.Length - minLength);

            //Crea los nodos
            Node leftNode = new Node(node.room.bottomLeftCorner, new Vector2Int(node.room.topRightCorner.x, node.room.bottomLeftCorner.y + newLength));
            Node rightNode = new Node(new Vector2Int(node.room.bottomLeftCorner.x, node.room.bottomLeftCorner.y + newLength), node.room.topRightCorner);

            //Los guarda en sus nodos correspondientes
            node.left = leftNode;
            node.right = rightNode;

            return true;
        }

        return false;
    }
}
