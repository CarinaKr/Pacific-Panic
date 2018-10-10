using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public Transform boat;
    public Transform island;
    public Slider distanceSlider;
    public GameObject sharkRange,tentakleRange;
    public AudioClip startGame, loopGame, reachEnd;
    public AudioSource[] audioSources;


    private float islandPositon;
    private bool songChanged;
    private bool endPlayed;

    void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }

        //islandPositon = island.position.y - (island.lossyScale.y / 2);
        islandPositon = island.position.y;
        audioSources[0].clip = startGame;
        audioSources[0].Play();
    }
	
	// Update is called once per frame
	void Update () {
        distanceSlider.value =1- (Mathf.Abs(boat.position.y - islandPositon) / island.position.y);
        if(distanceSlider.value>0.75&&!songChanged)
        {
            songChanged = true;
            StartCoroutine("newSong");
        }
        if(distanceSlider.value>=0.98 && !endPlayed)
        {
            boat.position = new Vector2(boat.position.x,islandPositon);
            StartCoroutine("endReached");
            //SceneManager.LoadScene(2);      //Game over good
        }

        if(!audioSources[0].isPlaying)
        {
            audioSources[0].clip = loopGame;
            audioSources[0].loop = true;
            audioSources[0].Play();
        }
	}
    public void changeSong()
    {
        StartCoroutine("newSong");
    }

    IEnumerator newSong()
    {
        for(int i=0;i<100;i++)
        {
            audioSources[0].volume -= 0.1f;
            audioSources[1].volume += 0.1f;
        }
        yield return new WaitForSeconds(0.25f);
    }
    
    public IEnumerator endReached()
    {
        endPlayed = true;
        audioSources[0].volume = 1;
        audioSources[0].clip = reachEnd;
        audioSources[0].Play();
        yield return new WaitForSeconds(reachEnd.length);
        SceneManager.LoadScene(2);
    }
}
