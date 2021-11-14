using UnityEngine;

namespace Helpers
{
    public static class MathHelper
    {
        public static float Tau = Mathf.PI * 2;
        
        public static float DirectionToAngle(Vector3 vector)
        {
            return Mathf.Atan2(vector.x, vector.z);
        }

        public static Vector3 AngleToDirection(float angle)
        {
            return new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle));
        }
    }
}