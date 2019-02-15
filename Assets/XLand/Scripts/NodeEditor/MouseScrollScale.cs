using UnityEngine;

namespace XLand
{
    public class MouseScrollScale : MonoBehaviour
    {
        public float sensitivity = 0.1f;

        public RectTransform boundingRect;

        private void Update()
        {
            var delta = Input.mouseScrollDelta;
            if (delta.y == 0) return;
            if (!boundingRect.Contains(Input.mousePosition)) return;
            var mousePos = Input.mousePosition;
            var mouseLocalPos = transform.InverseTransformPoint(mousePos);

            var oldLocalScale = transform.localScale;
            var oldLocalScaleValue = (oldLocalScale.x + oldLocalScale.y + oldLocalScale.z) / 3.0f;
            var scaleValue = oldLocalScaleValue + delta.y * sensitivity;
            if (scaleValue < 1.0f)
            {
                scaleValue = Mathf.Exp(scaleValue - 1.0f);
            }

            transform.localScale = Vector3.one * scaleValue;

            var mouseNewPos = transform.TransformPoint(mouseLocalPos);
            transform.position += mousePos - mouseNewPos;
        }
    }
}