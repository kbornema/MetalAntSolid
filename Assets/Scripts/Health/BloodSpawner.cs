using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpawner : MonoBehaviour
{
    private AntVisual _antVisual;

    [SerializeField]
    private List<SpriteDecay> _spawnParticles;

    [SerializeField]
    private Vector3 _scaleMin = new Vector3(0.75f, 0.75f, 0.75f);
    [SerializeField]
    private Vector3 _scaleMax = new Vector3(1.1f, 1.1f, 1.1f);

    [SerializeField]
    private float _rangeMin = 0.25f;
    [SerializeField]
    private float _rangeMax = 1.0f;

    private void Start()
    {
        _antVisual = GetComponentInChildren<AntVisual>();
    }

    public void SpawnBlood(int num)
    {
        for (int i = 0; i < num; i++)
        {
            var prefab = _spawnParticles[Random.Range(0, _spawnParticles.Count)];

            Vector2 pos = transform.position;

            float pi = Random.Range(0.0f, Mathf.PI * 2.0f);
            Vector2 randDir = new Vector2(Mathf.Sin(pi), Mathf.Cos(pi)) * Random.Range(_rangeMin, _rangeMax);


            var instance = Instantiate(prefab, pos + randDir, Random.rotation);

            if(_antVisual)
            {
                instance.TheSpriteRenderer.color = _antVisual.ColorSetting.GetBloodColor(Random.value);
            }

            instance.transform.localScale = Vector3.Lerp(_scaleMin, _scaleMax, Random.value);
                
        }
    }


}
