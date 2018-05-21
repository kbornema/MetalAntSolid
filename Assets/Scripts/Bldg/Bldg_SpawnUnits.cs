using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bldg
{
    public class Bldg_SpawnUnits : Bldg_Bhvr
    {
        [SerializeField]
        private int _min;
        [SerializeField]
        private int _max;
        [SerializeField]
        private int _modifier;

        [SerializeField]
        private FloatTimer _minSpawnTimer;
        [SerializeField]
        private FloatTimer _spawnTimer;

        [SerializeField, Header("Show all entities - DebugOnly")]
        private List<OrbitingAnt> _ants;

        [SerializeField]
        private float _IDLEradius;

        [SerializeField]
        private GameObject spawnPosition;

        [SerializeField]
        private GameObject orbitCenter;

        void Start()
        {
            orbitCenter = transform.parent.gameObject;
        }

        public override void Init(Bldg_Building controller)
        {
            _controller = controller;

            int value = Random.Range(0, _modifier);
            _min += value;
            _max += value;
        }
        public override void OnUpdate()
        {
            var count = _ants.Count;

            if(count < _min)
                UpdateTimer(_minSpawnTimer);
            else
            {
                if (count >= _max)
                    return;

                UpdateTimer(_spawnTimer);

            }
        }

        private void UpdateTimer(FloatTimer timer)
        {
            timer.Update();
            if (timer.IsReached)
            {

                PooledObject ant = MatchManager.Instance().PoolManager.GetObjFromPool(ObjectPoolManager.PoolTyp.OrbitingAnt).GetComponent<AntPool>().GetObject();
                OrbitingAnt orbAnt = ant.GetComponentInChildren<OrbitingAnt>(true);
                orbAnt.gameObject.SetActive(true);
                orbAnt.HomeZone = orbitCenter;

                _ants.Add(orbAnt);

                ant.gameObject.transform.position = this.transform.position;

                timer.Reset();
            }
        }

        public override void OnEnd()
        {

        }

        public override void OnReset()
        {

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(this.transform.position, _IDLEradius);
        }
    }
}
