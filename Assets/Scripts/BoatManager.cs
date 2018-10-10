using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoatManager : MonoBehaviour {
    
    public Slider damageSlider;
    public int maxDamage;
    public GameObject[] leaks;
    public float minTimeLeaks, maxTimeLeaks;
    public Row[] rowers;
    public float extraForce, maxSpeed;
    public AudioClip gameOverClip;

    private Rigidbody2D rb;
    private Trigger _currentTrigger;
    private float _damage;
    private float _counter,_nextLeakTime;
    private int _nextLeak;
    private Obstacle _currentObstacle;
    private int oldRowCounter = 1;
    private GameManager gameManager;
    private bool endPlayed;

	// Use this for initialization
	void Start () {
        nextLeak();
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.instance;
	}

    void Update()
    {
        _counter += Time.deltaTime;
        if(_counter>=_nextLeakTime)
        {
            if(!leaks[_nextLeak].activeSelf)
            {
                leaks[_nextLeak].SetActive(true);
            }
            nextLeak();
        }

        checkRowers();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag=="Obstacle")
        {
            _currentObstacle = other.gameObject.GetComponent<Obstacle>();
            damage += _currentObstacle.damage;
            _currentObstacle.hit();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag=="Wave")
        {
            other.gameObject.GetComponent<Obstacle>().hit();
            damage += other.gameObject.GetComponent<Wave>().checkDamage(); 
        }

        if (other.tag == "HiddenRocks")
        {
            other.gameObject.GetComponent<HiddenRocks>().gotHit = true;
        }

        if(other.tag=="Trigger")
        {
            _currentTrigger = other.GetComponent<Trigger>();
            _currentTrigger.activateObject();
        }
    }

    public void nextLeak()
    {
        _counter = 0;
        _nextLeakTime = Random.Range(minTimeLeaks, maxTimeLeaks);
        int trycounter = 0;
        do
        {
            trycounter++;
            _nextLeak = Random.Range(0, leaks.Length);
        } while (trycounter < 3 && leaks[_nextLeak].activeSelf);
        
    }

    public void checkRowers()
    {
        bool sameRowCounter = true;
        int couner = rowers[0].rowCounter;
        for(int i=0;i<rowers.Length;i++)
        {
            if(rowers[i].rowCounter!=couner)
            {
                sameRowCounter = false;
            }
        }
        if(sameRowCounter&&couner!=oldRowCounter&&rb.velocity.y<maxSpeed)
        {
            Debug.Log("booster");
            rb.AddForce(new Vector2(0, extraForce * Time.deltaTime));
            oldRowCounter = couner;
        }
    }

    public float damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
            damageSlider.value = 1 - (_damage / (float)maxDamage);
            if(damageSlider.value==0 && !endPlayed)
            {
                StartCoroutine("endGame");
                //SceneManager.LoadScene(3);  //Game over bad
            }
        }
    }

    public IEnumerator endGame()
    {
        endPlayed = true;
        gameManager.audioSources[0].clip = gameOverClip;
        gameManager.audioSources[1].Stop();
        gameManager.audioSources[0].Play();
        yield return new WaitForSeconds(gameOverClip.length);
        SceneManager.LoadScene(3);
    }
}
