using System;
using UnityEngine;

namespace XLand
{
    public class SelectActiveChild : MonoBehaviour
    {
        public bool activeSelfWhenChildSelected = true;

        private void Start()
        {
            CurrentActiveChild = null;
        }

        public Transform CurrentActiveChild
        {
            set
            {
                var someChildrenActivated = false;
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(child == value);
                    if (child == value) someChildrenActivated = true;
                }

                if (activeSelfWhenChildSelected)
                {
                    gameObject.SetActive(someChildrenActivated);
                }
            }
            get
            {
                Transform value = null;
                foreach (Transform child in transform)
                {
                    if (!child.gameObject.activeSelf) continue;
                    value = child;
                    break;
                }

                return value;
            }
        }
    }
}