using UnityEngine;

namespace XLand
{
    public class PinchScale : MonoBehaviour
    {
        public float sensitivity = 0.1f;
        public RectTransform boundingRect;

        void Update()
        {
            // If there are two touches on the device...
            if (Input.touchCount == 2)
            {
                // Store both touches.
                var touchZero = Input.GetTouch(0);
                var touchOne = Input.GetTouch(1);

                if (!boundingRect.Contains(touchOne.position) || !boundingRect.Contains(touchZero.position)) return;

                // Find the position in the previous frame of each touch.
                var touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                var prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                var touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                var deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;

                Vector3 centerPrevPos = (touchZeroPrevPos + touchOnePrevPos) / 2.0f;
                var centerPrevLocalPos = transform.InverseTransformPoint(centerPrevPos);

                var oldScale = transform.localScale;
                var scaleValue = (oldScale.x + oldScale.y + oldScale.z) / 3.0f;
                transform.localScale = Vector3.one * Mathf.Max(scaleValue + deltaMagnitudeDiff * sensitivity, 0.1f);

                var desiredCenterPos = transform.TransformPoint(centerPrevLocalPos);
                Vector3 centerCurrentPos = (touchZero.position + touchOne.position) / 2.0f;
                // move center back
                transform.position += centerCurrentPos - desiredCenterPos;
            }
        }
    }
}