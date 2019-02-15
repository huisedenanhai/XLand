using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace XLand
{
    [RequireComponent(typeof(Dropdown))]
    public class EnumFieldInspector : FieldInspector
    {
        private Dropdown m_DropDown;

        private Dictionary<int, int> m_ValueToIndex;
        private List<int> m_ValueList;

        private void Awake()
        {
            m_DropDown = GetComponent<Dropdown>();
        }

        private void SetEnumFieldValue(int v)
        {
            FieldValue = Enum.ToObject(Field.FieldInfo.FieldType, v);
        }

        private void LoadValue(int v)
        {
            int index;
            if (m_ValueToIndex.TryGetValue(v, out index))
            {
                m_DropDown.value = index;
            }
            else if (m_ValueList.Count > 0)
            {
                m_DropDown.value = 0;
                SetEnumFieldValue(m_ValueList[0]);
            }
        }

        private void LoadValue()
        {
            var v = (int) FieldValue;
            LoadValue(v);
        }

        private void Start()
        {
            var fieldType = Field.FieldInfo.FieldType;
            // build value dict
            m_ValueList = new List<int>();
            m_ValueToIndex = new Dictionary<int, int>();
            Assert.IsTrue(fieldType.IsEnum);
            foreach (int v in Enum.GetValues(fieldType))
            {
                var index = m_ValueList.Count;
                m_ValueList.Add(v);
                m_ValueToIndex.Add(v, index);
            }

            // init dropdown
            m_DropDown.ClearOptions();
            var options = new List<Dropdown.OptionData>();
            foreach (var v in m_ValueList)
            {
                options.Add(new Dropdown.OptionData(Enum.ToObject(fieldType, v).ToString()));
            }

            m_DropDown.AddOptions(options);
            LoadValue();
            m_DropDown.RefreshShownValue();
            m_DropDown.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(int index)
        {
            if (index >= m_ValueList.Count) return;
            var v = m_ValueList[index];
            SetEnumFieldValue(v);
        }

        private void Update()
        {
            LoadValue();
        }
    }
}