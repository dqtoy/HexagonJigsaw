using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lib
{
    public class BindingProperty : MonoBehaviour
    {

        public static List<object> checks = new List<object>() { };

        private void Awake()
        {
            if (component != null && property != "" && content != "")
            {
                binding = new Binding(component, checks, property, content);
            }
        }

        private void OnDestroy()
        {
            if (binding != null)
            {
                binding.dispose();
            }
        }

        private Binding binding;

        public Component component;
        public string property = "";
        public string content = "";
    }

}