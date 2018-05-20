using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bldg
{
    public abstract class Bldg_Bhvr : MonoBehaviour
    {
        protected Bldg_Building _controller;

        public abstract void Init(Bldg_Building controller);
        public abstract void OnUpdate();
        public abstract void OnEnd();
        public abstract void OnReset();

    }
}
