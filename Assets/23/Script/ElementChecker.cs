using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementChecker : MonoBehaviour {


    [SerializeField]
    GameObject FirstElement;

    [SerializeField]
    GameObject SecondElement;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!FirstElement)
        {
            FirstElement = other.gameObject;
        }
        else if (other.gameObject != FirstElement)
        {
            SecondElement = other.gameObject;
        }
    }

    public GameObject GetFirstElement()
    {
        return FirstElement;
    }

    public GameObject GetSecondElement()
    {
        return SecondElement;
    }

    public void Reset()
    {
        FirstElement = null;
       
        SecondElement = null;
    }

    public void ToNext()
    {
        FirstElement = null;
        FirstElement = SecondElement;
        SecondElement = null;
    }

}
