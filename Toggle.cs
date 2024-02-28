using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    [SerializeField] private bool isOn;
    [SerializeField] private GameObject objectToSetActive;
    [SerializeField] private GameObject checkmark;

    private void Start() {
        checkmark.SetActive(isOn);
        objectToSetActive?.SetActive(isOn);
    }

    public void OnValueChanged() {
        isOn = !isOn;
        checkmark.SetActive(isOn);
        objectToSetActive?.SetActive(isOn);
    }
}
