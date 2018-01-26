using UnityEngine;

public static class b_Settings_Manager {

    /// <summary> 
    /// Saving user audio 
    /// </summary>
    public static void SetMasterVolume(float p_Volume) {
        PlayerPrefs.SetFloat("MasterSound",p_Volume);
        PlayerPrefs.Save();
    }
}
