using System.Reflection;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace XLand
{
    public static class SliderExtension
    {
        static readonly MethodInfo toggleSetMethod;

        static SliderExtension()
        {
            var methods = typeof(Slider).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var t in methods)
            {
                if (t.Name == "Set" && t.GetParameters().Length == 2)
                {
                    toggleSetMethod = t;
                    break;
                }
            }
        }

        /// <summary>
        /// set slider value without triggering onValueChanged event
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="value"></param>
        /// <param name="sendCallback">do you what to trigger onValueChanged event?</param>
        public static void Set(this Slider instance, float value, bool sendCallback)
        {
            Assert.IsNotNull(toggleSetMethod);
            toggleSetMethod.Invoke(instance, new object[] {value, sendCallback});
        }
    }
}