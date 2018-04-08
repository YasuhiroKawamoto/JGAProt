using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAutoDestroyer : MonoBehaviour {

    private Animator _anm;

	// Use this for initialization
	void Start () {
        _anm = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
        if(_anm.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            Destroy(this.gameObject);
        }
	}
}
