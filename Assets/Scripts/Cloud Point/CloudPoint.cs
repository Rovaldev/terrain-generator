using System.Drawing;
using UnityEngine;

public class CloudPoint
{
    //Atributos
    private int numberOfAtoms;
    private CList<Vector3> elements = new CList<Vector3>();
    private Vector3 boundingBoxMinPoint;
    private Vector3 boundingBoxMaxPoint;

    //Metodos get/set para numberOfAtoms
    public int GetNumberOfAtoms()
    {
        return numberOfAtoms;
    }

    public void SetNumberOfAtoms(int numberOfAtoms)
    {
        this.numberOfAtoms = numberOfAtoms;
    }

    //Metodos get/set para elements;
    public CList<Vector3> GetElements()
    {
        return elements;
    }

    public void SetElements(CList<Vector3> elements)
    {
        this.elements = elements;
    }

    //Metodo get/Set para boundingBoxMinPoint
    public Vector3 GetBoundingBoxMinPoint()
    {
        return boundingBoxMinPoint;
    }

    public void SetBoundingBoxMinPoint(Vector3 boundingBoxMinPoint)
    {
        this.boundingBoxMinPoint = boundingBoxMinPoint;
    }

    //Metodo get/Set para boundingBoxMaxPoint
    public Vector3 GetBoundingBoxMaxPoint()
    {
        return boundingBoxMaxPoint;
    }

    public void SetBoundingBoxMaxPoint(Vector3 boundingBoxMaxPoint)
    {
        this.boundingBoxMaxPoint = boundingBoxMaxPoint;
    }

    //Metodo para añadir un nuevo elemento
    public void AddPoint(Vector3 element)
    {
        elements.Add(element);
    }
}
