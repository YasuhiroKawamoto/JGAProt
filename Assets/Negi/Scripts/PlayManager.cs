using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
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

        // IngameManager
        [SerializeField]
        private InGameManager _gameManager = null;
        public InGameManager GameManager
        {
            get { return _gameManager; }
            private set { _gameManager = value; }
        }

    }
}