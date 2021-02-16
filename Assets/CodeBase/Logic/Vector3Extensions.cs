using UnityEngine;

namespace CodeBase.Logic
{
    public static class Vector3Extensions
    {
        public static Vector3 ToPositive(this Vector3 vector)
        {
            vector.x = GetPositive(vector.x);
            vector.y = GetPositive(vector.y);
            vector.z = GetPositive(vector.z);

            return vector;
        }

        private static float GetPositive(float number) =>
            number < 0 ? number * -1 : number;
    }
}