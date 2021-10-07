using UnityEngine;

namespace Helpers
{
    public static class MathHelper
    {
        public static float GetVectorAngle(Vector3 vector)
        {
            return Mathf.Atan2(vector.x, vector.z) * Mathf.Rad2Deg;
        }

        public static Vector3 GetAngleVector(float angle)
        {
            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));
        }
    }
}