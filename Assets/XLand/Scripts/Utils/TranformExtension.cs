using UnityEngine;

namespace XLand
{
    public static class TranformExtension
    {
        public static void DestroyAllChildren(this Transform go)
        {
            foreach (Transform child in go)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}