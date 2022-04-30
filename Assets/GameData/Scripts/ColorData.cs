using System;
using System.Collections.Generic;
using UnityEngine;

namespace Testovoe
{
    [CreateAssetMenu(fileName = "ColorData", menuName = "Data/ColorData", order = 1)]
    public class ColorData : ScriptableObject
    {
        public List<ColorDataItem> colors;
    }
    [Serializable]
    public class ColorDataItem 
    {
        [SerializeField] public string viewName;
        [SerializeField] public Color color;
    }

}