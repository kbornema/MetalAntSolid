using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUI
{
    public class GUI_SelectMenu : MonoBehaviour
    {
        [SerializeField]
        private List<GUI_SelectIcon> _selectList;

        Rewired.Player playerInput;

        CanvasGroup canvasGroup;

        bool enabled;

        public void Start()
        {
            playerInput = Rewired.ReInput.players.GetPlayer(this.transform.GetComponentInParent<Actor>().PlayerID);
            canvasGroup = this.GetComponent<CanvasGroup>();
            enabled = false;
        }

        public void CreateSelectMenu(List<Action> del)
        {
            canvasGroup.alpha = 1;

            //ToDo: Stop movement from player
            if (del.Count == 0)
            {
                foreach (var item in _selectList)
                    item.FadeOut();

                return;
            }

            for (int i = 0; i < _selectList.Count; i++)
            {
                if (del.Count <= i)
                    _selectList[i].FadeOut();
                else
                    _selectList[i].InitSelectIcon(del[i]);
            }

            enabled = true;
        }

        private void Update()
        {
            if (enabled)
            {
                //ToDo: input for controller
                Vector2 inputVector = new Vector2(playerInput.GetAxis("LookX"), playerInput.GetAxis("LookY"));

                if (Mathf.Abs(inputVector.magnitude) > 0.5f)
                {
                    if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
                    {
                        inputVector = new Vector2(inputVector.x, 0).normalized;
                    }
                    else
                    {
                        inputVector = new Vector2(0, inputVector.y).normalized;
                    }

                    if (Input.GetKeyDown(KeyCode.W) || inputVector.y > 0)
                        ButtonPressed(0);

                    else if (Input.GetKeyDown(KeyCode.D) || inputVector.x > 0)
                        ButtonPressed(1);

                    else if (Input.GetKeyDown(KeyCode.S) || inputVector.x < 0)
                        ButtonPressed(2);

                    else if (Input.GetKeyDown(KeyCode.A) || inputVector.y < 0)
                        ButtonPressed(3);

                    if (Input.GetKeyDown(KeyCode.Escape) || playerInput.GetButton("ButtonB"))
                        DisableMenu();
                }
            }
        }

        private void LateUpdate()
        {
            this.transform.rotation = Quaternion.identity;
        }

        private void ButtonPressed(int id)
        {
            if (_selectList[id].Trigger())
                DisableMenu();
        }

        private void DisableMenu()
        {
            foreach (var item in _selectList)
            {
                item.Reset();
            }
            canvasGroup.alpha = 0;
            enabled = false;
        }
    }

}
