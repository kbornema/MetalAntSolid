using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bldg
{
    public class Bldg_Collector : Bldg_Bhvr
    {
        [SerializeField]
        private FloatTimer _timer;
        [SerializeField]
        private int _capacity;
        [SerializeField]
        private int _increaseValue;

        public override void Init(Bldg_Building controller)
        {
            _controller = controller;
        }

        public override void OnUpdate()
        {
            if (_controller.OwnerTyp == MatchManager.PlayerTyp.None)
                return;

            _timer.Update();
            if(_timer.IsReached)
            {
                int tmpValue = 0;
                if(_capacity - _increaseValue >= 0)
                {
                    tmpValue = _increaseValue;
                    _capacity -= _increaseValue;
                }
                else
                    tmpValue = _capacity;

                MatchManager.Instance().CalculateResources(_controller.OwnerTyp, tmpValue);
            }
        }

        public override void OnEnd()
        {

        }

        public override void OnReset()
        {

        }
    }
}