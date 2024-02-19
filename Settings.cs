using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Settings : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private GameObject[] selectedLightMode;

    public void ShowSelectedLightMode() {
        if (PlayerPrefs.GetString(Const.LIGHT_SETTINGS) == Const.LIGHT_MODE) {
            selectedLightMode[0].SetActive(true);
            selectedLightMode[1].SetActive(false);
            selectedLightMode[2].SetActive(false);
            selectedLightMode[3].SetActive(true);
        }
        else {
            selectedLightMode[0].SetActive(false);
            selectedLightMode[1].SetActive(true);
            selectedLightMode[2].SetActive(true);
            selectedLightMode[3].SetActive(false);
        }
    }

    public void SetLightMode() {
        PlayerPrefs.SetString(Const.LIGHT_SETTINGS, Const.LIGHT_MODE);
    }

    public void SetDarkMode() {
        PlayerPrefs.SetString(Const.LIGHT_SETTINGS, Const.DARK_MODE);
    }

    public void UpdateStatsText() {
        statsText.text = string.Empty;
        for (int i = 0; i < 6; i++) {
            statsText.text += "Best time in " + (Mode)i + " mode is " + Statistics.GetBestTime((Mode)i) +
                ".\nAttempts - " + Statistics.GetAttemptCount((Mode)i) + "\n";
        }
    }
}
