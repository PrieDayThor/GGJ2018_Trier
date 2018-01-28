using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using System.Linq;

public class Manager_Game : MonoBehaviour {

    public struct SoundPair {

        AudioClip m_Caller;
        AudioClip m_Dest;
        Sprite m_CallerIm;
        Sprite m_DestIm;

        public SoundPair(AudioClip p_Caller, AudioClip p_Dest, Sprite p_CallerIm, Sprite p_DestIm) {
            m_Caller = p_Caller;
            m_Dest = p_Dest;
            m_CallerIm = p_CallerIm;
            m_DestIm = p_DestIm;
        }

        public AudioClip GetCaller() {
            return m_Caller;
        }

        public AudioClip GetDest() {
            return m_Dest;
        }

        public Sprite GetCallerImg() {
            return m_CallerIm;
        }

        public Sprite GetDestImg() {
            return m_DestIm;
        }
    }

    //Resources.Load("Sound/Level1")

    AudioClip[] m_CallerClips;
    AudioClip[] m_DestClips;

    Sprite[] m_CallerPics;
    Sprite[] m_DestPics;

    GameObject[] m_CharacterSlots;
    public GameObject callerPic = null;
    Sprite personCallingIdentity;

    AudioClip sound;

    List<SoundPair> m_Data = new List<SoundPair>();

    public GameObject soundPlayerObject;

    private List<SoundPair> m_ListOfCallers = new List<SoundPair>();

    private float countDown;
    public Text countDownText;
    //Die Zeit die verloren geht, wenn ein falsches Paar gematcht wird
    public int penalty;

    // Use this for initialization
    void Start () {
        PrepareData();
        ChooseRandomNextCaller();
        initCharacters();

        countDown = 20;
    }

    private void PrepareData() {
        // Get Sounds
        m_CallerClips = Resources.LoadAll<AudioClip>("Sounds/Caller");
        m_DestClips = Resources.LoadAll<AudioClip>("Sounds/Destination");

        // Get Pictures
        m_CallerPics = Resources.LoadAll<Sprite>("Images/Characters/Caller");
        m_DestPics = Resources.LoadAll<Sprite>("Images/Characters/Destination");

        for (int count = 0; count < m_CallerClips.Length; count++) {
            m_Data.Add(new SoundPair(m_CallerClips[count], m_DestClips[count], m_CallerPics[count], m_DestPics[count]));
        }
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonDown(0))
        {
            playOneSoundClip();
        }
        if (countDown > 0) {
            updateCountDown();
        }
    }

    private void ChooseRandomNextCaller() {
        SoundPair tmp = m_Data[Random.Range(0, m_Data.Count)];
        while (m_ListOfCallers.Contains(tmp)) {
            if (m_ListOfCallers.Count == m_Data.Count) {
                break;
            }
            tmp = m_Data[Random.Range(0, m_Data.Count)];

        }
        m_ListOfCallers.Add(tmp);
    }

    void playOneSoundClip()
    {
        Debug.Log("Filename: " + m_CallerClips[0].name);
        sound = m_CallerClips[0];
        
        soundPlayerObject.GetComponent<SoundPlayer>().playSound(sound);
    }

    private void LoadPictures() {


    }

    void initCharacters()
    {
        int charCounter = 1;

        m_CharacterSlots = GameObject.FindGameObjectsWithTag("CharacterSlot");
        callerPic.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/silhouette");
        personCallingIdentity = m_ListOfCallers[0].GetCallerImg();

        int[] randomNumbers = new int[m_Data.Count];
        System.Random rnd = new System.Random();
        randomNumbers = Enumerable.Range(0,m_Data.Count).OrderBy(m => rnd.Next()).ToArray();

        foreach(int k in randomNumbers) {
            Debug.Log(k.ToString() + "");
        }

        for (int i = 0; i < m_Data.Count; ++i)
        {
            m_CharacterSlots[i].GetComponent<Image>().sprite = m_CallerPics[randomNumbers[i]];
        }
    }

    public void updateCaller()
    {
        //WIRD AUFGERUFEN WENN DAS RICHTIGE MATCH GEFUNDEN WIRD
        //callerPic.GetComponent<Image>().sprite = personCallingIdentity;

    }

    public void checkConnection()
    {
        //gibt den namen des aktivierten buttons zurück
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);


    }

    public void checkMatch() {
        print(EventSystem.current.currentSelectedGameObject.name);
        print(m_ListOfCallers[0].GetDest().name);
        if(EventSystem.current.currentSelectedGameObject.name == m_ListOfCallers[0].GetDest().name) {
            print("TRUE");
        }
    }

    void updateCountDown() {

        countDown -= Time.deltaTime;
        countDownText.text = ""+Mathf.Round(countDown);

    }

    void reduceCountDown() {
        countDown -= penalty;
        print(countDown);
    }



}
