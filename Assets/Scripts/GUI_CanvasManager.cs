using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUI
{
    public class GUI_CanvasManager : MonoBehaviour
    {
        [SerializeField]
        private GUI_ResourcesHandler _resourceHandler;

        [SerializeField]
        private Transform _parent;

        private void Start()
        {

            var instance = Instantiate(_resourceHandler, _parent);
            instance.Init(MatchManager.PlayerTyp.Player1);

            var instance2 = Instantiate(_resourceHandler, _parent);
            instance2.Init(MatchManager.PlayerTyp.Player2);
        }
    }

}

