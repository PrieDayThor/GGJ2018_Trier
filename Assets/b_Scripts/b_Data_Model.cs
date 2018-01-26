using System.Collections.Generic;
using UnityEngine;

public class b_Data_Model : MonoBehaviour{

    /// <summary> 
    /// Return a dictionary with pairs of audioclips.
    /// The audioclips are located in the folder "Resources/Sounds".
    /// <para>Pairs should be saved with names like 0_C and 0_D, with C => caller and D => destination.</para>
    /// </summary>
    public Dictionary<AudioClip,AudioClip> LoadAllSounds() {
        Dictionary<AudioClip, AudioClip> tmp_Sounds = new Dictionary<AudioClip, AudioClip>();
        AudioClip[] tmp_AllSounds = Resources.LoadAll<AudioClip>("Sounds/");
        int tmp_Soundfiles = tmp_AllSounds.Length / 2;
        int index = 0;
        for (int count = 0; count < tmp_Soundfiles; count++) {
            tmp_Sounds.Add(tmp_AllSounds[index], tmp_AllSounds[index + 1]);
            index += 2;
        }
        return tmp_Sounds;
    }

    /// <summary> 
    /// Return an array of sprites.
    /// The sprites are located in the folder "Resources/Portraits".
    /// </summary>
    public Sprite[] LoadImages() {
        return Resources.LoadAll<Sprite>("Portraits/");
    }
}
