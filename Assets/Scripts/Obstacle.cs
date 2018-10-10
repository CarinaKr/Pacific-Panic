using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public float damage;
    public int btnNum,arduinoNum;
    public AudioSource audioSource;
    public AudioClip hitBoatClip;
    public AudioClip getHitClip;
    public bool destroyOnHit;

    protected Button button;
    protected Transform boat;
    protected GameManager gameManager;
    protected bool gettingDestroyed;

    // Use this for initialization
    protected void Start () {
        boat = GameObject.Find("Boat").GetComponent<Transform>();
        button = Button.instance;
        gameManager = GameManager.instance;
    }

    public virtual void hit()
    {
        StartCoroutine("playSound", hitBoatClip);
    }

    protected IEnumerator playSound(AudioClip clip)
    {
        audioSource.panStereo = transform.position.x <= boat.position.x ? -1 : 1;
        audioSource.clip = clip;
        if(!audioSource.isPlaying)
        { audioSource.Play(); }
        yield return new WaitForSeconds(clip.length);
        if(destroyOnHit)
        {
            Destroy(gameObject);
        }
    }
}
