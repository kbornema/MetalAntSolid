using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bldg
{

    public class Bldg_Bhvr_Fire : Bldg_Bhvr
    {

        [SerializeField]
        private GameObject _bulletPrefab;
        [SerializeField]
        private Transform _spawnPosition;
        [SerializeField]
        private FloatTimer _cooldown;

        private bool _cooldownRdy;

        private GameObject _bulletInstance;

        public override void Init(Bldg_Building controller)
        {
            _controller = controller;
            _cooldownRdy = true;
        }

        public override void OnEnd()
        {

        }

        public override void OnReset()
        {
            
        }

        public override void OnUpdate()
        {
            if (!_cooldownRdy)
            {
                _cooldown.Update();
                if (_cooldown.IsReached)
                    _cooldownRdy = true;
            }
   
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            //ToDo: filter enemy
            if (!_cooldownRdy)
                return;
            var dir = collision.gameObject.transform.position - this.transform.position;
            dir = dir.normalized;
            SpawnBullet(dir);


        }
        private void SpawnBullet(Vector2 dir)
        {
            _bulletInstance = Instantiate(_bulletPrefab, _spawnPosition.position, Quaternion.identity, null);
            _bulletInstance.GetComponent<BaseBullet>().InitBullet(dir);

            _cooldownRdy = false;
            _cooldown.Reset();
        }
    }

}
