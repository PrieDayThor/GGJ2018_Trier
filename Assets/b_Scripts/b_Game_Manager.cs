using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class b_Game_Manager : NetworkBehaviour {

    private List<b_Player> m_Players;
    private Dictionary<int, int> m_PlayerScores;
    private int m_IDStart = 0;

    public void InitGame(int p_AmountOfPlayers) {
        if (isServer) {
            m_Players = new List<b_Player>();
            m_PlayerScores = new Dictionary<int, int>();
            for(int count = 0; count < p_AmountOfPlayers; count ++) {
                m_PlayerScores.Add(count, 0);
            }
        }
    }

    ///<summary>
    /// Login a playerscript at server.
    /// </summary>
    public void LoginPlayer(b_Player p_Player) {
        if(isServer) {
            m_Players.Add(p_Player);
        }
    }
}
