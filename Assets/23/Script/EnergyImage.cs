using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Play.Element
{
    public class EnergyImage : MonoBehaviour
    {

        SpriteRenderer _spriteRenderer;

        // Use this for initialization
        void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {

            double size =  gameObject.GetComponentInParent<Element>().GetEnergy()/gameObject.GetComponentInParent<Element>().GetCapacity();

            _spriteRenderer.GetComponent<SpriteRenderer>().transform.localScale = new Vector3((float)size,(float)size,2);
        }
    }
}