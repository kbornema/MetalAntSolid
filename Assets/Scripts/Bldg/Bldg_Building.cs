using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bldg
{
    public class Bldg_Building : MonoBehaviour
    {
        [SerializeField]
        List<Bldg_Bhvr> _bhvrList;

        



        private MatchManager.PlayerTyp _ownerTyp = MatchManager.PlayerTyp.None;
        public MatchManager.PlayerTyp OwnerTyp { get { return _ownerTyp; } set { _ownerTyp = value; } }


        private void Awake()
        {
            foreach (var item in _bhvrList)
                item.Init(this);
        }

        private void Update()
        {
            foreach (var item in _bhvrList)
                item.OnUpdate();
        }
        public void ConquerBuilding(MatchManager.PlayerTyp playertyp)
        {
            _ownerTyp = playertyp;
            foreach (var item in _bhvrList)
                item.Init(this);
        }

        private void OnDestroy()
        {
            foreach (var item in _bhvrList)
                item.OnEnd();
        }



        #region Validate
        private void OnValidate()
        {
            FindAll();
        }
        private void FindAll()
        {
            var newbhvrs = GetComponentsInChildren<Bldg_Bhvr>();

            if (CheckDifference(newbhvrs, _bhvrList))
            {
                _bhvrList = new List<Bldg_Bhvr>(newbhvrs);
            }
        }
        private bool CheckDifference(Bldg_Bhvr[] arr, List<Bldg_Bhvr> list)
        {
            if (arr.Length != list.Count)
                return true;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != list[i])
                    return true;
            }
            return false;
        }
        #endregion

    }
}
