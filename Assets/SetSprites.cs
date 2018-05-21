using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSprites : MonoBehaviour {

    [SerializeField]
    private AntVisual _antVisual;

    private void OnValidate()
    {
        var a = _antVisual.BodyParts;

        foreach (var item in a)
        {
            var asd = item.GetComponent<SpriteRenderer>();
            if(asd != null)
            {
                asd.sortingOrder = 100000;
            }
        }
    }
}
