using UnityEngine;

namespace XLand
{
    public class DontDimScreen : MonoBehaviour
    {
        private void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}