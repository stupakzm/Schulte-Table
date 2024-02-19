using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statistics {

    public static void SetBestTime(Mode mode, float time) {
        if (PlayerPrefs.GetFloat(mode.ToString() + Const.BEST_TIME, float.MaxValue) > time) {
            PlayerPrefs.SetFloat(mode.ToString() + Const.BEST_TIME, time);
        }
    }

    public static float GetBestTime(Mode mode) {
        return PlayerPrefs.GetFloat(mode.ToString() + Const.BEST_TIME);
    }

    public static void SetAttemptCount(Mode mode) {
        PlayerPrefs.SetInt(mode.ToString() + Const.ATTEMPTS, PlayerPrefs.GetInt(mode.ToString() + Const.ATTEMPTS) +1);
    }

    public static int GetAttemptCount(Mode mode) {
        return PlayerPrefs.GetInt(mode.ToString() + Const.ATTEMPTS);
    }
}
