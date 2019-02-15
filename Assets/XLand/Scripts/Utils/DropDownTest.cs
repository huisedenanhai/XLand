using UnityEngine;
using UnityEngine.UI;

namespace XLand
{
    public class DropDownTest : MonoBehaviour
    {
        Dropdown m_Dropdown;
        public Text m_Text;

        void Start()
        {
            //Fetch the Dropdown GameObject
            m_Dropdown = GetComponent<Dropdown>();
            //Add listener for when the value of the Dropdown changes, to take action
            m_Dropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(m_Dropdown); });

            //Initialise the Text to say the first value of the Dropdown
            m_Text.text = "First Value : " + m_Dropdown.value;
        }

        //Ouput the new value of the Dropdown into Text
        void DropdownValueChanged(Dropdown change)
        {
            m_Text.text = "New Value : " + change.value;
        }
    }
}