using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : SingletonMonoBehaviour<PlayManager> {

    // UIRoot
    [SerializeField]
    private GameObject _uiRoot = null;
    public GameObject UIRoot
    {
        get { return _uiRoot; }
        private set { _uiRoot = value; }
    }

    // 2DObjectのRoot
    [SerializeField]
    private GameObject _2dObjectRoot = null;
    public GameObject ObjectRoot
    {
        get { return _2dObjectRoot; }
        private set { _2dObjectRoot = value; }
    }

}
