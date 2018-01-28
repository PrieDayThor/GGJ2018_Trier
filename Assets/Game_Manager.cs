using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CallerStuff;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {

    // Dateien von Paaren sind im Ordner hintereinander.
    private AudioClip[] m_CallerAudio;
    private Sprite[] m_CallerSprites;
    private GameObject[] m_UI_Buttons;
    private List<GameObject> m_SpawnedButtons = new List<GameObject>();

    private List<CallerPair> m_CallerPairs = new List<CallerPair>();
    // Use this for initialization
    private int CallerIndex = 0;

    public List<GameObject> m_ButtonPrefabs;
    public List<GameObject> m_PossiblePositions;

    public GameObject m_ButtonParent;

    public GameObject m_CurrentCallerSprite;
    public Sprite m_Silloutte;
    public Text m_CountdownField;
    public Text m_PlayerPoints;
    public AudioSource m_AudioSource;
    public GameObject m_GameOverScreen;
    public Text m_FinalText;
    private float m_GameTime = 300.0f;
    private int m_PlayerScore = 0;
    private bool m_GameOver = false;
    private bool m_PlayerCanAct = true;
    private bool m_TookPhonecall = false;
    public AudioClip m_Ringtone;


    private void Awake() {
        PrepareData();
        ShuffleCallerArray();
        PicturesToUI();
        m_PlayerPoints.text = m_PlayerScore.ToString();
    }

    private void Update() {
        if (!m_GameOver) {
            m_GameTime -= Time.deltaTime;
            m_CountdownField.text = m_GameTime.ToString("##.##");
            if (!m_TookPhonecall)
                PlayerRingtone();
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

        // Get Sprites
        m_CallerSprites = Resources.LoadAll<Sprite>("Images/Characters/");

        for (int count = 0; count < m_CallerAudio.Length; count += 2) {
            m_CallerPairs.Add(
                new CallerPair(
                new Caller(m_CallerAudio[count], m_CallerSprites[ 0 + count * 12].name.Substring(0,2)),
                new Caller(m_CallerAudio[count + 1], m_CallerSprites[ 12 + count * 12].name.Substring(0, 2))));
        }
    }

    /// <summary>
    /// Printing all loaded data to debug-console. For debugging purpose.
    /// </summary>
    private void PrintData() {
        foreach (CallerPair pair in m_CallerPairs) {
            Debug.Log("Caller1: " + pair.GetCaller1().ToString() + " Caller2: " + pair.GetCaller2().ToString() +
                      " Active: " + pair.GetActiveCaller().ToString() + " Passive: " + pair.GetPassiveCaller().ToString());
        }
    }

    /// <summary>
    /// Loading animators of all attainable characters into spriterenderer.
    /// </summary>
    private void PicturesToUI() {
        GameObject obj2;
        int index = m_PossiblePositions.Count -1;
        foreach (CallerPair pair in m_CallerPairs) {
            GameObject prefab = FindPrefabFromPrefix(pair.GetPassiveCaller().GetImageID().Substring(0,2));
            prefab = Instantiate(prefab, m_ButtonParent.transform, false);
            m_SpawnedButtons.Add(prefab);
            obj2 = m_PossiblePositions[index];
            index--;
            prefab.GetComponent<RectTransform>().position = obj2.GetComponent<RectTransform>().position;
            prefab.GetComponent<Button>().onClick.AddListener(() => { CheckSolution(prefab); });
        }
    }

    /// <summary>
    /// Checking whether the player found the right pair.
    /// </summary>
    /// <param name="p_Object"></param>
    public void CheckSolution(GameObject p_Object) {
        if (m_PlayerCanAct) {
            if (Equals(p_Object.GetComponent<Image>().name.Substring(0,2)
                       , m_CallerPairs[CallerIndex].GetPassiveCaller().GetImageID())) {
                if (CallerIndex == m_CallerPairs.Count - 1) {
                    ReshuffleAllPairs();
                    DestroySpawnedPrefabs();
                    ShuffleCallerArray();
                    PicturesToUI();
                    CallerIndex = 0;
                    m_AudioSource.PlayOneShot(m_CallerPairs[m_CallerPairs.Count - 1].GetPassiveCaller().GetSound());
                    StartCoroutine(WaitForNewPicture(ReturnSpriteFromPrefix(m_CallerPairs[m_CallerPairs.Count - 1].GetActiveCaller().GetImageID())));
                } else {
                    CallerIndex++;
                    m_AudioSource.PlayOneShot(m_CallerPairs[CallerIndex - 1].GetPassiveCaller().GetSound());
                    StartCoroutine(WaitForNewPicture(ReturnSpriteFromPrefix(m_CallerPairs[CallerIndex - 1].GetActiveCaller().GetImageID())));
                }
                m_PlayerScore += 100;
                m_PlayerPoints.text = m_PlayerScore.ToString();
            } else {
                m_GameTime -= 15.0f;
            }
        }
    }

    /// <summary>
    /// Plays the one-liner of the current calling person.
    /// </summary>
    public void PlayCurrentCallerSound() {
        if (!m_AudioSource.isPlaying && m_PlayerCanAct) {
            m_AudioSource.PlayOneShot(m_CallerPairs[CallerIndex].GetActiveCaller().GetSound());

        } else {
            if (m_AudioSource.clip == m_Ringtone) {
                m_AudioSource.Stop();
                m_AudioSource.clip = null;
                m_TookPhonecall = true;
                m_AudioSource.PlayOneShot(m_CallerPairs[CallerIndex].GetActiveCaller().GetSound());
            }
        }
    }
    public void PlayerRingtone() {
        if (!m_AudioSource.isPlaying) {
            if (m_AudioSource.clip == null)
                m_AudioSource.clip = m_Ringtone;
            m_AudioSource.PlayOneShot(m_Ringtone);
        }
    }

    private void ReshuffleAllPairs() {
        foreach(CallerPair pair in m_CallerPairs) {
            pair.ShuffleActivePassive();
        }
    }

    private IEnumerator WaitForNewPicture(Sprite p_Sprite) {
        m_PlayerCanAct = false;
        m_CurrentCallerSprite.GetComponent<Image>().sprite = p_Sprite;
        yield return new WaitForSeconds(2.0f);
        m_CurrentCallerSprite.GetComponent<Animator>().runtimeAnimatorController = null;
        m_CurrentCallerSprite.GetComponent<Image>().sprite = m_Silloutte;
        m_TookPhonecall = false;
        m_PlayerCanAct = true;
    }

    private GameObject FindPrefabFromPrefix(string p_Prefix) {
        foreach(GameObject obj in m_ButtonPrefabs) {
            if (Equals(obj.name.Substring(0, 2), p_Prefix))
                return obj;
        }
        Debug.Log("No prefab found");
        return null;
    }

    private Sprite ReturnSpriteFromPrefix(string p_Prefix) {
        for(int count = 0; count < m_CallerSprites.Length / 12; count++) {
            if(Equals(m_CallerSprites[0 + count * 12].name.Substring(0, 2), p_Prefix)) {
                return m_CallerSprites[0 + count * 12];
            }
        }
        return null;
    }

    private void DestroySpawnedPrefabs() {
        for(int count = m_SpawnedButtons.Count - 1; count > 0; count--) {
            Destroy(m_SpawnedButtons[count]);
        }
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
