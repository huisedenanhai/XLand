using UnityEngine;

namespace XLand
{
    public static class Vector3Extension
    {
        /// <summary>
        /// Relative angle in degree. result lies in range [-180, 180]
        /// </summary>
        /// <param name="angle">the angle</param>
        /// <param name="pivot">the pivot</param>
        /// <returns>diff of angle relative to the pivot, in range [-180, 180]</returns>
        public static Vector3 RelativeAngleTo(this Vector3 angle, Vector3 pivot)
        {
            var relative = angle - pivot;
            relative.x = Mathf.Repeat(relative.x + 180.0f, 360.0f) - 180.0f;
            relative.y = Mathf.Repeat(relative.y + 180.0f, 360.0f) - 180.0f;
            relative.z = Mathf.Repeat(relative.z + 180.0f, 360.0f) - 180.0f;
            return relative;
        }
    }
}