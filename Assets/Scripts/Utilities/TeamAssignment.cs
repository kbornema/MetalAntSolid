using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamAssignment : MonoBehaviour {

    [SerializeField]
    private int team;
    public int Team
    {
        get { return team; }
        set { team = value;}
    }
}
