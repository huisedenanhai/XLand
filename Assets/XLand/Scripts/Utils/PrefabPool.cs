using System.Collections.Generic;
using UnityEngine;

namespace XLand
{
    public class PrefabPool : MonoBehaviour
    {
        public GameObject prefab;

        private readonly List<GameObject> m_Prefabs = new List<GameObject>();

        public GameObject Get()
        {
            if (m_Prefabs.Count == 0) return Instantiate(prefab);
            var p = m_Prefabs[0];
            m_Prefabs.RemoveAt(0);
            p.SetActive(true);
            return p;
        }

        public void Put(GameObject go)
        {
            m_Prefabs.Insert(0, go);
            go.transform.SetParent(transform, false);
            go.SetActive(false);
        }
    }
}