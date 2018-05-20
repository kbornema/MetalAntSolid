using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bldg
{
    public class Bldg_Bhvr_LifeSpan : Bldg_Bhvr
    {

        //Set neutral after x seconds
        [SerializeField]
        FloatTimer _floatTimer;

        public override void Init(Bldg_Building controller)
        {
            _controller = controller;
        }
        public override void OnUpdate()
        {
            _floatTimer.Update();
            if (_floatTimer.IsReached)
                SetNeutral();
        }
        public override void OnEnd()
        {
            SetNeutral();
        }

        private void SetNeutral()
        {
            _controller.OwnerTyp = MatchManager.PlayerTyp.None;
        }

        public override void OnReset()
        {

        }
    }

}

