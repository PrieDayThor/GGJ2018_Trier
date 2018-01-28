using UnityEngine;

namespace CallerStuff {

    public class Caller {

        AudioClip m_SoundClip;
        Sprite m_Sprite;

        public Caller(AudioClip p_Clip, Sprite P_Sprite) {
            m_SoundClip = p_Clip;
            m_Sprite = P_Sprite;
        }

        public AudioClip GetSound() {
            return m_SoundClip;
        }

        public Sprite GetSprite() {
            return m_Sprite;
        }

        public override string ToString() {
            return "[Clip: " + m_SoundClip.name + " AnimatorController: " + m_Sprite.name +  "]";
        }
    }

}

