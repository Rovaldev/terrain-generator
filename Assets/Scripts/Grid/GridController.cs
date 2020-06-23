using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;

public class GridController : MonoBehaviour
{
    //Atributos
    public int length = 100;
    public int width = 100;
    public int height = 100;
    public float unitSize = 1;

    private GridObject<int> grid;

    public Transform test;

    private void Start()
    {
        //Inicializa la malla con los parametros dados
        grid = new GridObject<int>(length, width, height, unitSize, transform.position, -1);
    }

    private void Update()
    {
        //Actualiza el tamaño de la malla constantemente
        grid.Length = length;
        grid.Height = width;
        grid.Width = height;

        //Actualiza la unidad de tamaño de la malla constantemente
        grid.UnitSize = unitSize;

        //Actualiza el origen constantemente
        grid.Origin = transform.position;
    }

    //Metodo para dibujar la malla
    private void OnDrawGizmos()
    {
        //Dibuja el plano xy de la malla
        for (int x = 1; x <= length; x++)
        {
            float xScaled = x * unitSize;

            for (int y = 1; y <= height; y++)
            {
                float yScaled = y * unitSize;

                Debug.DrawLine(new Vector3(transform.position.x + (xScaled - unitSize), transform.position.y + (yScaled - unitSize),  transform.position.z), new Vector3(transform.position.x + xScaled, transform.position.y + (yScaled - unitSize), transform.position.z), Color.white);
                Debug.DrawLine(new Vector3(transform.position.x + (xScaled - unitSize), transform.position.y + (yScaled - unitSize), transform.position.z), new Vector3(transform.position.x + (xScaled - unitSize), transform.position.y + yScaled, transform.position.z), Color.white);
            }
        }

        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + (height * unitSize), transform.position.z), new Vector3(transform.position.x + (length * unitSize), transform.position.y + (height * unitSize), transform.position.z), Color.white);
        Debug.DrawLine(new Vector3(transform.position.x + (length * unitSize), transform.position.y, transform.position.z), new Vector3(transform.position.x + (length * unitSize), transform.position.y + (height * unitSize), transform.position.z), Color.white);

        //Dibuja el otro plano xy de la malla
        for (int x = 1; x <= length; x++)
        {
            float xScaled = x * unitSize;

            for (int y = 1; y <= height; y++)
            {
                float yScaled = y * unitSize;

                Debug.DrawLine(new Vector3(transform.position.x + (xScaled - unitSize), transform.position.y + (yScaled - unitSize), transform.position.z + (width * unitSize)), new Vector3(transform.position.x + xScaled, transform.position.y + (yScaled - unitSize), transform.position.z + (width * unitSize)), Color.white);
                Debug.DrawLine(new Vector3(transform.position.x + (xScaled - unitSize), transform.position.y + (yScaled - unitSize), transform.position.z + (width * unitSize)), new Vector3(transform.position.x + (xScaled - unitSize), transform.position.y + yScaled, transform.position.z + (width * unitSize)), Color.white);
            }
        }

        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + (height * unitSize), transform.position.z + (width * unitSize)), new Vector3(transform.position.x + (length * unitSize), transform.position.y + (height * unitSize), transform.position.z + (width * unitSize)), Color.white);
        Debug.DrawLine(new Vector3(transform.position.x + (length * unitSize), transform.position.y, transform.position.z + (width * unitSize)), new Vector3(transform.position.x + (length * unitSize), transform.position.y + (height * unitSize), transform.position.z + (width * unitSize)), Color.white);

        //Dibuja el plano yz de la malla
        for (int z = 1; z <= width; z++)
        {
            float zScaled = z * unitSize;

            for (int y = 1; y <= height; y++)
            {
                float yScaled = y * unitSize;

                Debug.DrawLine(new Vector3(transform.position.x + (length * unitSize), transform.position.y + (yScaled - unitSize), transform.position.z + (zScaled - unitSize)), new Vector3(transform.position.x + (length * unitSize), transform.position.y + (yScaled - unitSize), transform.position.z + zScaled), Color.white);
                Debug.DrawLine(new Vector3(transform.position.x + (length * unitSize), transform.position.y + (yScaled - unitSize), transform.position.z + (zScaled - unitSize)), new Vector3(transform.position.x + (length * unitSize), transform.position.y + yScaled, transform.position.z + (zScaled - unitSize)), Color.white);
            }
        }

        Debug.DrawLine(new Vector3(transform.position.x + (length * unitSize), transform.position.y + (height * unitSize), transform.position.z), new Vector3(transform.position.x + (length * unitSize), transform.position.y + (height * unitSize), transform.position.z + (width * unitSize)), Color.white);

        //Dibuja el otro plano yz de la malla
        for (int z = 1; z <= width; z++)
        {
            float zScaled = z * unitSize;

            for (int y = 1; y <= height; y++)
            {
                float yScaled = y * unitSize;

                Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + (yScaled - unitSize), transform.position.z + (zScaled - unitSize)), new Vector3(transform.position.x, transform.position.y + (yScaled - unitSize), transform.position.z + zScaled), Color.white);
                Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + (yScaled - unitSize), transform.position.z + (zScaled - unitSize)), new Vector3(transform.position.x, transform.position.y + yScaled, transform.position.z + (zScaled - unitSize)), Color.white);
            }
        }

        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + (height * unitSize), transform.position.z), new Vector3(transform.position.x, transform.position.y + (height * unitSize), transform.position.z + (width * unitSize)), Color.white);
    }

    //Metodo para obtener el grid
    public GridObject<int> Grid
    {
        get { return grid; }
    }
}
