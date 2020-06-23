using System.Drawing;

public class CloudPoint
{
    //Atributos
    private int numberOfAtoms;
    private CList<DataObjects.Point> elements = new CList<DataObjects.Point>();
    private DataObjects.Point boundingBoxMinPoint;
    private DataObjects.Point boundingBoxMaxPoint;

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
    public CList<DataObjects.Point> GetElements()
    {
        return elements;
    }

    public void SetElements(CList<DataObjects.Point> elements)
    {
        this.elements = elements;
    }

    //Metodo get/Set para boundingBoxMinPoint
    public DataObjects.Point GetBoundingBoxMinPoint()
    {
        return boundingBoxMinPoint;
    }

    public void SetBoundingBoxMinPoint(DataObjects.Point boundingBoxMinPoint)
    {
        this.boundingBoxMinPoint = boundingBoxMinPoint;
    }

    //Metodo get/Set para boundingBoxMaxPoint
    public DataObjects.Point GetBoundingBoxMaxPoint()
    {
        return boundingBoxMaxPoint;
    }

    public void SetBoundingBoxMaxPoint(DataObjects.Point boundingBoxMaxPoint)
    {
        this.boundingBoxMaxPoint = boundingBoxMaxPoint;
    }

    //Metodo para añadir un nuevo elemento
    public void AddPoint(DataObjects.Point element)
    {
        elements.Add(element);
    }
}
