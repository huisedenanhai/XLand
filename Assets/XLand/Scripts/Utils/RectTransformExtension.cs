using UnityEngine;

namespace XLand
{
    public static class RectTransformExtension
    {
        public static bool Contains(this RectTransform rectTransform, Vector3 point)
        {
            return rectTransform.rect.Contains(rectTransform.InverseTransformPoint(point));
        }
    }
}