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
    public int roomGenerationLevel;
    public Node root;

    private void Start()
    {
        //Por defecto el tamaño del cuarto es del tamaño del mapa
        root = new Node(new Vector2Int(0,0), new Vector2Int(gridController.width, gridController.length));

        //Genera el arbol con los cuartos
        root = GenerateTreeRoomNodes(root);
    }

    //Metodo para comenzar a construir los cuartos
    public void StartRoomGeneration()
    {
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
                for(int y=roomGenerationLevel; y<roomHeight; y++)
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
        }
    }

    //Metodo para construir los pasillos
    private void CreateHalls(Node root)
    {
        //Comprueba si su nodo derecho e izquierdo son nodos hojas
        if(IsLeaf(root.left) && IsLeaf(root.right))
        {

        }

        //Comprueba si su nodo derecho es un nodo hoja
        if (IsLeaf(root.right))
        {

        }
    }

    //Metodo para comprobar si es un nodo hoja
    private bool IsLeaf(Node node)
    {
        return (node.left == null && node.right == null);
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
            //Lo divide por la mitad y crea dos nuevos cuartos
            int newWidth = node.room.Width / 2;
            Node leftNode = new Node(node.room.bottomLeftCorner, new Vector2Int(node.room.bottomLeftCorner.x + newWidth, node.room.topRightCorner.y));
            Node rightNode = new Node(new Vector2Int(node.room.bottomLeftCorner.x + newWidth, node.room.bottomLeftCorner.y), new Vector2Int(node.room.bottomLeftCorner.x + newWidth * 2, node.room.topRightCorner.y));

            //Los guarda en sus nodos correspondientes
            node.left = leftNode;
            node.right = rightNode;

            return true;
        }

        //Si es vertical y aun no llega al tamaño minimo horizontal
        if(orientation == 1 && node.room.Length > minLength * 2)
        {
            //Lo divide por la mitar u crea dos nuevos cuartos
            int newLength = node.room.Length / 2;
            Node leftNode = new Node(node.room.bottomLeftCorner, new Vector2Int(node.room.topRightCorner.x, node.room.bottomLeftCorner.y + newLength));
            Node rightNode = new Node(new Vector2Int(node.room.bottomLeftCorner.x, node.room.bottomLeftCorner.y + newLength), new Vector2Int(node.room.topRightCorner.x, node.room.bottomLeftCorner.y + newLength * 2));

            //Los guarda en sus nodos correspondientes
            node.left = leftNode;
            node.right = rightNode;

            return true;
        }

        return false;
    }
}
