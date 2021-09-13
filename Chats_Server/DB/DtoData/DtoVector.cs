using System;

namespace ProjectRT.DataBase
{
    public class DtoVector
    {
        public float x;
        public float y;
        public float z;

        public static DtoVector operator-(DtoVector A, DtoVector B)
        {
            return new DtoVector()
            {
                x = A.x - B.x,
                y = A.y - B.y,
                z = A.z - B.z,
            };
        }

        public static double Distance(DtoVector A, DtoVector B)
        {
            return Math.Sqrt(Math.Pow(A.x - B.x, 2) + Math.Pow(A.y - B.y, 2) + Math.Pow(A.z - B.z, 2));
        }
    }



}