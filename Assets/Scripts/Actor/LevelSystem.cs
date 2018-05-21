using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GUI;

[RequireComponent(typeof(EatingBhvr))]
public class LevelSystem : MonoBehaviour {

    #region Events
    [HideInInspector]
    public class LevelUpEvent : UnityEvent { }
    [HideInInspector]
    private LevelUpEvent _levelUpEvent = new LevelUpEvent();

    public void InvokeLevelUpEvent()
    {
        _levelUpEvent.Invoke();
    }

    public void AddOnLevelUpEventListener(UnityAction listener)
    {
        _levelUpEvent.AddListener(listener);
    }

    #endregion

    [SerializeField]
    private EatingFloatingText _eatingFloating;
    [SerializeField]
    private EatingBhvr _eatingBhvr;
    [SerializeField]
    private float _levelUpExp;

    private GUI_SelectMenu selectMenu;

    private float _curEp;

    #region Powerups

    public List<GameObject> towerList;
    public List<Hero_Wpn_Info> weaponList;

    SpawnFollower antSpawner;
    AntVisual antVisual;
    AntUpgrader antUpgrader;

    void InitPowerUps()
    {
        ObjectPoolManager objectPoolManager = MatchManager.Instance().PoolManager;
        antSpawner = this.transform.parent.GetComponent<SpawnFollower>();
        antVisual = this.transform.parent.GetComponentInChildren<AntVisual>();
        antUpgrader = this.transform.parent.GetComponent<AntUpgrader>();
    }

    void SpawnAnt()
    {
        Debug.Log("Spawning Ant");
        antSpawner.SpawnAntGroup();
    }

    void SpawnTower()
    {
        Debug.Log("Spawning Tower");
        GameObject randomTower = towerList[Random.Range(0, towerList.Count)];
        Standard_Weapon_Controller tower = Instantiate(randomTower).GetComponentInChildren<Standard_Weapon_Controller>();
        tower.transform.parent.position = this.transform.position;
        tower.GetComponentInParent<TeamAssignment>().Team = this.transform.GetComponentInParent<TeamAssignment>().Team;
    }

    void UpgradeAnts()
    {
        Debug.Log("Upgrading Ant");
        antUpgrader.UpgradeHero();

    }

    void UpgradeWeapon()
    {
        Debug.Log("Upgrading Weapon");
        // Destroy(this.transform.parent.GetComponentInChildren<Hero_Wpn_Controller>());
        Hero_Wpn_Controller weapon = this.transform.parent.GetComponentInChildren<Hero_Wpn_Controller>();
        if (weapon.weaponInfos.Count > 3)
            return;
        Hero_Wpn_Info newWeaponInfo = Instantiate(weaponList[weapon.weaponInfos.Count], weapon.transform);
        weapon.weaponInfos.Add(newWeaponInfo);
        weapon.switchToWeapon(newWeaponInfo);
        // Hero_Wpn_Controller weapon = Instantiate(weaponList[Random.Range(0, weaponList.Count)], this.transform.parent);
        // weapon.switchToWeapon(weapon.GetComponentInChildren<Hero_Wpn_Info>());
    }

    #endregion


    private void OnValidate()
    {
        if (_eatingBhvr != null)
            _eatingBhvr.GetComponent<EatingBhvr>();
    }

    private void Awake()
    {
        _curEp = 0;

        if (_eatingBhvr != null)
            _eatingBhvr.GetComponent<EatingBhvr>();

        _eatingBhvr.AddOnEatingEventListener(SetEP);
        selectMenu = this.transform.parent.GetComponentInChildren<GUI_SelectMenu>();

    }

    private void Start()
    {
        InitPowerUps();
    }
    
    public void SetEP(int value)
    {
        _curEp += value;
        if (_curEp >= _levelUpExp)
        {
            if(_eatingFloating != null)
                _eatingFloating.StopFloatingText();

            ActivateUpgrade();
            InvokeLevelUpEvent();
            FloatingText.CreateFloatingText("LEVEL UP", this.transform.position + new Vector3(0, 1, 0), null, Color.red);
            _curEp = 0;
        }
    }

    public void ActivateUpgrade()
    {
        List<System.Action> actions = new List<System.Action>() { SpawnAnt, SpawnTower, UpgradeAnts, UpgradeWeapon };
        selectMenu.CreateSelectMenu(actions);
        
    }



}
