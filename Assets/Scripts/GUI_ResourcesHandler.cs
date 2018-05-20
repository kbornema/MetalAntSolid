using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class GUI_ResourcesHandler : MonoBehaviour {

        private MatchManager.PlayerTyp _playerTyp;
        [SerializeField]
        private Text _resourceText;


        public void Init(MatchManager.PlayerTyp playerTyp)
        {
            _playerTyp = playerTyp;
            MatchManager.Instance().AddOnResourceListener(Updatetext);
        }

        private void Updatetext(MatchManager.PlayerTyp playertyp, int value)
        {
            if(playertyp == _playerTyp)
                _resourceText.text = value.ToString();
        }
    }

}
