using UnityEngine;

namespace XLand
{
    public class SaveCurrentModel : MonoBehaviour
    {
        public void Save()
        {
            if (ModelManager.Instance.CurrentActiveModel == null) return;
            var xLander = ModelManager.Instance.CurrentActiveModel.GetComponent<XLander>();
            if (xLander == null) return;
            xLander.Save();
        }
    }
}