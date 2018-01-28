using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CallerStuff;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour {

    // Dateien von Paaren sind im Ordner hintereinander.
    private AudioClip[] m_CallerAudio;
    private Sprite[] m_CallerSprites;
    private GameObject[] m_UI_Buttons;

    private List<CallerPair> m_CallerPairs = new List<CallerPair>();
    // Use this for initialization
    private int CallerIndex = 0;

    public List<GameObject> m_ButtonPrefabs;

    public GameObject m_CurrentCallerSprite;
    public Sprite m_Silloutte;
    public Text m_CountdownField;
    public Text m_PlayerPoints;
    public AudioSource m_AudioSource;
    public GameObject m_GameOverScreen;
    public Text m_FinalText;
    private float m_GameTime = 180.0f;
    private int m_PlayerScore = 0;
    private bool m_GameOver = false;
    private bool m_PlayerCanAct = true;


    private void Awake() {
        PrepareData();
        ShuffleCallerArray();
        //PicturesToUI();
        m_PlayerPoints.text = m_PlayerScore.ToString();
    }

    private void Update() {
        if (!m_GameOver) {
            m_GameTime -= Time.deltaTime;
            m_CountdownField.text = m_GameTime.ToString("##.##");
        } else {
            m_GameOverScreen.SetActive(true);
            m_FinalText.text = "Points: " + m_PlayerScore;
        }
        if (m_GameTime <= 0.0f) {
            m_GameOver = true;
        }
    }

    /// <summary>
    /// Random shuffle of all elements insider the array of callerpairs.
    /// </summary>
    private void ShuffleCallerArray() {
        System.Random m_Random = new System.Random();
        int n = m_CallerPairs.Count;
            while (n > 1) {
                int k = m_Random.Next(n--);
                CallerPair temp = m_CallerPairs[n];
                m_CallerPairs[n] = m_CallerPairs[k];
                m_CallerPairs[k] = temp;
            }
    }

    /// <summary>
    /// Loading of audio & animators from resource folder and saving them as "CallerPair"
    /// </summary>
    private void PrepareData() {
        // Get Sounds
        m_CallerAudio = Resources.LoadAll<AudioClip>("Sounds/");

        // Get Animators
        m_CallerSprites = Resources.LoadAll<Sprite>("Characters/");

        //for (int count = 0; count < m_CallerAudio.Length ; count += 2) {
        //    m_CallerPairs.Add(
        //        new CallerPair(
        //        new Caller(m_CallerAudio[count], m_Animators[count]), 
        //        new Caller(m_CallerAudio[count + 1], m_Animators[count + 1])));
        //}
    }

    /// <summary>
    /// Printing all loaded data to debug-console. For debugging purpose.
    /// </summary>
    private void PrintData() {
        foreach (CallerPair pair in m_CallerPairs) {
            Debug.Log("Caller1: " + pair.GetCaller1().ToString() + " Caller2: " + pair.GetCaller2().ToString() +
                      " Acitve: " + pair.GetActiveCaller().ToString() + " Passive: " + pair.GetPassiveCaller().ToString());
        }
    }

    /// <summary>
    /// Loading animators of all attainable characters into spriterenderer.
    /// </summary>
    private void PicturesToUI() {
        //m_UI_Buttons = GameObject.FindGameObjectsWithTag("PictureUI");
        //for(int count = 0; count < m_UI_Buttons.Length; count++) {
        //    m_UI_Buttons[count].GetComponent<Animator>().runtimeAnimatorController = m_CallerPairs[count].GetPassiveCaller().GetAnimator();
        //}
    }

    /// <summary>
    /// Checking whether the player found the right pair.
    /// </summary>
    /// <param name="p_Object"></param>
    public void CheckSolution(GameObject p_Object) {
        //if (m_PlayerCanAct) {
        //    if (Equals(p_Object.GetComponent<Animator>().runtimeAnimatorController
        //                                                 , m_CallerPairs[CallerIndex].GetPassiveCaller().GetAnimator())) {
        //        if (CallerIndex == m_CallerPairs.Count - 1) {
        //            ReshuffleAllPairs();
        //            ShuffleCallerArray();
        //            PicturesToUI();
        //            CallerIndex = 0;
        //            StartCoroutine(WaitForNewPicture(m_CallerPairs[m_CallerPairs.Count - 1].GetActiveCaller().GetAnimator()));
        //        } else {
        //            CallerIndex++;
        //            StartCoroutine(WaitForNewPicture(m_CallerPairs[CallerIndex - 1].GetActiveCaller().GetAnimator()));
        //        }
        //        m_PlayerScore += 100;
        //        m_PlayerPoints.text = m_PlayerScore.ToString();
        //    } else {
        //        m_GameTime -= 15.0f;
        //    }
        //}
    }

    /// <summary>
    /// Plays the one-liner of the current calling person.
    /// </summary>
    //public void PlayCurrentCallerSound() {
    //    if (!m_AudioSource.isPlaying && m_PlayerCanAct) {
    //        m_AudioSource.PlayOneShot(m_CallerPairs[CallerIndex].GetActiveCaller().GetSound());
    //    }
    //}

    private void ReshuffleAllPairs() {
        foreach(CallerPair pair in m_CallerPairs) {
            pair.ShuffleActivePassive();
        }
    }
    
    //private IEnumerator WaitForNewPicture(AnimatorController p_Controller) {
    //    m_PlayerCanAct = false;
    //    m_CurrentCallerSprite.GetComponent<Animator>().runtimeAnimatorController = p_Controller;
    //    yield return new WaitForSeconds(2.0f);
    //    m_CurrentCallerSprite.GetComponent<Animator>().runtimeAnimatorController = null;
    //    m_CurrentCallerSprite.GetComponent<Image>().sprite = m_Silloutte;
    //    m_PlayerCanAct = true;
    //}
}
