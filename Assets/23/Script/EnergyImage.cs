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

            if (size > 0)
            {
                _spriteRenderer.GetComponent<SpriteRenderer>().transform.localScale = new Vector3((float)size, (float)size, 1);
            }
            else
            {
                _spriteRenderer.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(0.0f, 0.0f, 1);
            }
        }
    }
}