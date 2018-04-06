﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    // 発生元エレメント
    public Element Parent
    {
        set;
        get;
    }

    public int _index;
    public int Index
    {
        set
        {
            _index = value;
        }
 
    }

    //// エフェクトの存在時間
    //private float _time;
    //public float Time
    //{
    //    set { _time = value; }
    //    get { return _time; }
    //}


    // 破壊コンポーネント
    [SerializeField]
    private AutoDestroyer _ad;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(Parent.GetEnergy());

        if (Parent != null)
        {
            if (Parent.GetEnergy() <= 0.0)
            {
                _ad.LifeTime = _index * 0.05f;
                _ad.enabled = true;
            }
        }
    }
}