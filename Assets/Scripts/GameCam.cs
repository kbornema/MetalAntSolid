using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCam : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private List<CamTarget> _targets;

    [Header("Movement")]
    [SerializeField]
    private float _moveSpeed = 5.0f;

    [SerializeField]
    private float _treshold;

    [Header("Zoom")]
    [SerializeField]
    private float _minSize = 5.0f;
    [SerializeField]
    private float _maxSize = 20.0f;
    [SerializeField]
    private float _perspectiveFactor = -2.0f;

    // Update is called once per frame
    private void Update ()
    {
        Vector2 finalDir = Vector2.zero;

        float totalWeight = 0.0f;

        float maxDist = float.NegativeInfinity;

        for (int i = _targets.Count - 1; i >= 0; i--)
        {   
            if(_targets[i] == null)
            {
                _targets.RemoveAt(i);
            }

            if (_targets[i].ValidTarget)
            {
                Vector2 dir = _targets[i].transform.position - transform.position;
                float dist = dir.magnitude;

                if (dist > maxDist)
                    maxDist = dist;

                dir.Normalize();
                float weight = _targets[i].Weight;

                totalWeight += weight;
                finalDir += dir * weight;
            }
        }

      

        Vector3 dir3 = finalDir;

        if(dir3.sqrMagnitude > _treshold * _treshold)
        {
            dir3 = dir3 / totalWeight;
            transform.position += dir3 * Time.deltaTime * _moveSpeed;

        }


        if(_camera.orthographic)
        {
            _camera.orthographicSize = Mathf.Clamp(maxDist, _minSize, _maxSize);
        }
        else
        {
            var pos = _camera.transform.position;
            pos.z = Mathf.Clamp(maxDist, _minSize, _maxSize) * _perspectiveFactor;
            _camera.transform.position = pos;
        }

	}

    public void AddTarget(CamTarget t)
    {
        _targets.Add(t);
    }

    public void RemoveTarget(CamTarget t)
    {
        _targets.Remove(t);
    }
}
