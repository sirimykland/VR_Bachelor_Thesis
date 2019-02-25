﻿using System.Collections;
using UnityEngine;



public class Card : MonoBehaviour {

    public static bool DO_NOT_TURN = false;

    [SerializeField]
    private int _state;
    [SerializeField]
    private int _cardValue;
    [SerializeField]
    private int _cardType;
    [SerializeField]
    private bool _initialized = false;

    private float _speed = 10F;

    void Start(){
        _state = 0;
    }

    IEnumerator rotateStuff()
    {
        float t = 0;
        float root = transform.eulerAngles.y;

        float target = transform.eulerAngles.y+ 180;

        if (root > target)
        {
            root= target;
            target = 0;
        }

        while (root < target)
        {
            t += Time.deltaTime;
            root = Mathf.Lerp(root, target, t * 0.5f);
            transform.rotation = Quaternion.Euler(0, root, 0);
            yield return null;
        }
    }
    

    void OnMouseUpAsButton() { 
            //Debug.Log("Pressed card.");
            FlipCard();
    }
    public void SetupGraphics(Material backside) {
        this.gameObject.GetComponentInChildren<Renderer>().material = new Material(backside);
    }

    public void FlipCard() {
        if (_state == 0){
            _state = 1;
        }else if (_state == 1){
            _state = 0;
        }

        if (_state == 0 && !DO_NOT_TURN){
            StartCoroutine(rotateStuff());        
        }
        else if (_state == 1 && !DO_NOT_TURN){
            StartCoroutine(rotateStuff());
        }

        //Debug.Log("Flipped card"+ _state);
    }

    public int CardValue{
        get { return _cardValue; }
        set { _cardValue = value; }
    }

    public int CardType{
        get { return _cardType; }
        set { _cardType = value; }
    }

    public int State {
        get { return _state; }
        set { _state = value; }
    }

    public bool Initialized{
        get { return _initialized; }
        set { _initialized = value; }
    }

    public void falseCheck() {
        StartCoroutine(pause());
    }

    IEnumerator pause() {
        yield return new WaitForSeconds(1);
        if (_state == 0){
            StartCoroutine(rotateStuff());
            
        } else if (_state == 1) {
           StartCoroutine(rotateStuff());
        }
        DO_NOT_TURN = false;
    }

}
