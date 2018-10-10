using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : Obstacle {

    public float speed;
    public int[] btnPlayersLeft;
    public int[] btnPlayersRight;

    
    public float startDistance;

    private int _direction;
    private Vector2 _goal;
    private float _damage;

    new void Start()
    {
        base.Start();
        //transform.position = new Vector2(transform.position.x * startDistance, boat.position.y);
        _direction = transform.position.x < 0 ? 0 : 1;
        _goal = new Vector2(transform.position.x * (-1),transform.position.y);
    }
    
    void Update()
    {
        if(boat.position.y>_goal.y)
        {
            _goal.y = boat.position.y;
        }
        transform.position = Vector3.MoveTowards(transform.position,_goal, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position,_goal)<0.25f)
        {
            Destroy(gameObject);
        }
    }

    public float checkDamage()
    {
        _damage = damage;
        if(_direction==0)    //wave from the left
        {
            for(int i=0;i<btnPlayersLeft.Length;i++)
            {
                if (/*Input.GetAxisRaw("Player" + i)==-1 ||*/ button.button(arduinoNum, btnPlayersLeft[i]))  
                {
                    if(_damage==damage)     //first player leaning over
                    {
                        _damage -= damage / 2;  //deducts half the damage
                    }
                    else if(_damage<=(damage/2))    //other two players
                    {
                        _damage -= damage / 4;      //deduct a quater each
                    }
                }
            } 
        }
        else if(_direction==1)
        {
            for (int i = 0; i < btnPlayersRight.Length; i++)
            {
                if (/*Input.GetAxisRaw("Player" + i) == 1 ||*/ button.button(arduinoNum, btnPlayersRight[i]))  
                {
                    //_damage -= damage / btnPlayersRight.Length;
                    if (_damage == damage)     //first player leaning over
                    {
                        _damage -= damage / 2;  //deducts half the damage
                    }
                    else if (_damage <= (damage / 2))    //other two players
                    {
                        _damage -= damage / 4;      //deduct a quater each
                    }
                }
            }
        }
        Debug.Log("Damage by wave:" + _damage);
        return _damage;
    }
    
}
