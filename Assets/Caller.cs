using UnityEngine;

namespace CallerStuff {

    public class Caller {

        AudioClip m_SoundClip;
        string m_ImageID;

        public Caller(AudioClip p_Clip, string p_ImageID) {
            m_SoundClip = p_Clip;
            m_ImageID = p_ImageID;
        }

        public AudioClip GetSound() {
            return m_SoundClip;
        }

        public string GetImageID() {
            return m_ImageID;
        }

        public override string ToString() {
            return "[Clip: " + m_SoundClip.name + " ImageID: " + m_ImageID +  "]";
        }
    }

}

