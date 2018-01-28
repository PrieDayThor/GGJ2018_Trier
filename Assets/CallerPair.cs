using UnityEngine;

namespace CallerStuff{
    public class CallerPair {

        Caller m_Caller1;
        Caller m_Caller2;

        Caller m_ActiveCaller;
        Caller m_PassiveCaller;

        public CallerPair(Caller p_Caller1, Caller p_Caller2) {
            m_Caller1 = p_Caller1;
            m_Caller2 = p_Caller2;
            ShuffleActivePassive();
        }

        public void ShuffleActivePassive() {
            if (Random.Range(0, 100) > 50) {
                m_ActiveCaller = m_Caller1;
                m_PassiveCaller = m_Caller2;
            } else {
                m_ActiveCaller = m_Caller2;
                m_PassiveCaller = m_Caller1;
            }

        }

        public Caller GetCaller1() {
            return m_Caller1;
        }

        public Caller GetCaller2() {
            return m_Caller2;
        }

        public Caller GetActiveCaller() {
            return m_ActiveCaller;
        }

        public Caller GetPassiveCaller() {
            return m_PassiveCaller;
        }


    }
}

