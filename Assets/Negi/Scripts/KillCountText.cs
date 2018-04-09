using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCountText : MonoBehaviour {

    [SerializeField]
    private Text _text = null;

    private void Update()
    {
        _text.text = EnemyManager.Instance.GetKills().ToString();
    }
}
