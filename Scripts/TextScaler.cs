using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScaler : MonoBehaviour {

    [Range(1f, 100f)] public int screenHieghtDevider;

    private void Start() {
        float screenHeight = Screen.height;
        gameObject.GetComponent<TextMeshProUGUI>().fontSize = screenHeight/ screenHieghtDevider;
    }
}
