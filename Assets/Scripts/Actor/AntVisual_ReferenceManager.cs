using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntVisual_ReferenceManager : MonoBehaviour {

    public GameObject antWeapon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [ContextMenu("assignReferences")]
    public void assignReferences()
    {
        AntVisual antVisual = this.GetComponent<AntVisual>();
        if(antVisual == null)
        {
            Debug.Log("No Antvisual Script on this Object. Something is wrong!!!");
        }
        if(this.GetComponentInParent<Hero_Movement>() != null)
        {
            Hero_Movement heroMovement = this.GetComponentInParent<Hero_Movement>();
            heroMovement.antVisual = antVisual;
            Hero_Wpn_Controller weapon = heroMovement.GetComponentInChildren<Hero_Wpn_Controller>();
            weapon.Weapon = antWeapon;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(heroMovement.gameObject);
#endif
        }
        else if (this.GetComponentInParent<AntMovement>() != null)
        {
            AntMovement antMovement = this.GetComponentInParent<AntMovement>();
            antMovement.antVisual = antVisual;
            Standard_Weapon_Controller weapon = antMovement.GetComponentInChildren<Standard_Weapon_Controller>();
            weapon.Weapon = antWeapon;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(antMovement.gameObject);
#endif
        }
        else
        {
            Debug.Log("The Parent of this Object should not have an AntVisual Component");
        }
    }
}
