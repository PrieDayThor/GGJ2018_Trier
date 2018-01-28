using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class Manager_Game : MonoBehaviour {

    //Resources.Load("Sound/Level1")

    AudioClip[] allClips;
    Sprite[] allImages;

    GameObject[] potentialConnections;
    public GameObject callerPic = null;
    Sprite personCallingIdentity;

    AudioClip sound;

    Dictionary<string, int> dict = new Dictionary<string, int>();

    public GameObject soundPlayerObject;

    // Use this for initialization
    void Start () {
        allClips = Resources.LoadAll<AudioClip>("Sounds");
        allImages = Resources.LoadAll<Sprite>("Images/Characters");

        initDictionary(allClips, allImages);
        initCharacters();
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonDown(0))
        {
            playOneSoundClip();
        }

    }

    void playOneSoundClip()
    {
        Debug.Log("Filename: " + allClips[0].name);
        sound = allClips[0];
        
        soundPlayerObject.GetComponent<SoundPlayer>().playSound(sound);
    }

    void initDictionary(AudioClip[] sound, Sprite[] image)
    {
        for(int i = 0; i < sound.Length-1; ++i)
        {
            dict.Add(sound[i].name, i);
        }
        for (int i = 0; i < image.Length - 1; ++i)
        {
            dict.Add(image[i].name, i);
        }
    }

    void initCharacters()
    {
        int charCounter = 1;

        potentialConnections = GameObject.FindGameObjectsWithTag("CharacterSlot");
        callerPic.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/silhouette");

        List<int> used = new List<int>();

        int randomNumber = (int)(Random.Range(0, (allImages.Length)));
        used.Add(randomNumber);

        while(charCounter < 8)
        {
            randomNumber = (int)(Random.Range(0, (allImages.Length)));

            if(!(used.Contains(randomNumber)))
            {
                used.Add(randomNumber);
                charCounter++;
            }
        }

        Debug.Log(potentialConnections.Length);
        Debug.Log(allImages.Length);
        Debug.Log(used.Count);

        for (int i = 1; i < 8; ++i)
        {
            potentialConnections[i].GetComponent<Image>().sprite = allImages[used[i]];
        }

        while(used.Contains(randomNumber))
        {
            randomNumber = (int)(Random.Range(0, (allImages.Length)));
        }

        personCallingIdentity = allImages[randomNumber];
    }

    public void updateCallerPic()
    {
        callerPic.GetComponent<Image>().sprite = personCallingIdentity;
    }

    public void checkConnection()
    {
        //gibt den namen des aktivierten buttons zurück
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);


        

    }



}
