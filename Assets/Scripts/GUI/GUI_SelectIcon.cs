using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class GUI_SelectIcon : MonoBehaviour
    {
        [SerializeField]
        private Image _mainImage;
        [SerializeField]
        private Image _iconImage;

        private Action _del;

        public void InitSelectIcon(Sprite sprite, Action del)
        {
            _del = del;
            _iconImage.sprite = sprite;

        }

        public bool Trigger()
        {
            if (_del != null)
            {
                _del();
                return true;
            }
            else
                return false;

        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                Trigger();
        
        }

        public void FadeOut()
        {
            _mainImage.color = new Color(0,0,0);
            _iconImage.color = new Color(255,255,255, 0);
                 
        }

        internal void Reset()
        {
            _del = null;
        }
    }

}
