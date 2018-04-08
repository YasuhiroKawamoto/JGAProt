using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementChecker : MonoBehaviour {


    [SerializeField]
    public GameObject FirstElement { set; get; }

    [SerializeField]
    public GameObject SecondElement { set; get; }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (FirstElement == null)
        {
            FirstElement = other.gameObject;
        }
        else if (other.gameObject != FirstElement)
        {
            SecondElement = other.gameObject;
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (FirstElement == null)
        {
            FirstElement = other.gameObject;
        }
        
    }
}
