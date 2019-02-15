using UnityEngine;

namespace XLand
{
    public class ResetTransform : MonoBehaviour
    {
        public void ResetLocalPosition()
        {
            transform.localPosition = Vector3.zero;
        }

        public void ResetLocalScale()
        {
            transform.localScale = Vector3.one;
        }
    }
}