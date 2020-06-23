using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataObjects : MonoBehaviour
{
    //Estructura punto para guarda
    /*public struct Point
    {
        public double x;
        public double y;
        public double z;

        public Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        //Implementa el operador != para poder comprobar no igualdades
        public static bool operator !=(Point a, Point b)
        {
            if(a.x==b.x && a.y==b.y && a.z == b.z)
            {
                return false;
            }

            return true;
        }

        //Como implementa != necesita comprobar igualdades tambien
        public static bool operator ==(Point a, Point b)
        {
            if (a.x == b.x && a.y == b.y && a.z == b.z)
            {
                return true;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        //Calcula la magnitud del punto
        public float magnitude
        {
            get
            {
                return (float)Math.Sqrt((this.x * this.x) + (this.y * this.y) + (this.z * this.z));
            }
        }
    }*/
}
