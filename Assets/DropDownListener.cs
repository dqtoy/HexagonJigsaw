using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropDownListener : MonoBehaviour {

    public void OnChange()
    {
        Dropdown dp = gameObject.GetComponent<Dropdown>();
        object obj = dp.options[dp.value].image;
        EditorVO.Instance.dispatcher.DispatchWith("UI" + gameObject.name + "Handle",obj);
    }
}
