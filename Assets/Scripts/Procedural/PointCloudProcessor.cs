using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloudProcessor : MonoBehaviour
{
    //Atributos
    public GridController gridController;

    //Adapta la informacion de una nube de puntos a la malla
    public void FillGrid(CloudPoint cloudPoint)
    {
        //Calcula el tamaño de la caja de limites de la nube de puntos
        /*float cloudLength = (float)(cloudPoint.GetBoundingBoxMaxPoint().x - cloudPoint.GetBoundingBoxMinPoint().x);
        float cloudHeight = (float)(cloudPoint.GetBoundingBoxMaxPoint().y - cloudPoint.GetBoundingBoxMinPoint().y);
        float cloudWidth = (float)(cloudPoint.GetBoundingBoxMaxPoint().z - cloudPoint.GetBoundingBoxMinPoint().z);*/


    }
}
