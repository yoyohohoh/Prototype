// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Subject
{
    [Header("Movements")]
    private CharacterController _controller;
    [SerializeField] private bool isGround = false;
    private Vector2 _direction;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] public float _speed;
    [SerializeField] public float _dashForce;
    [SerializeField] public float _jumpForce;
    [SerializeField] public float _damage;
    [SerializeField] public float _attackForce;

    [Header("Control")]
    public GameObject _controlPanel;
    private Joystick _joystick;
    private ButtonInteraction _attackButton;
    private ButtonInteraction _jumpButton;
    private ButtonInteraction _dashButton;
    private ButtonInteraction _skillButton;

    [Header("HUD")]
    [SerializeField] public PlayerData _playerData;
    [SerializeField] private float _currentSpeed;
    public float _currentDamage;
    [SerializeField] private Transform weaponHolder;
    bool isWeaponEquipped = false;
    [SerializeField] private Transform attackTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("CharacterController component is missing from the GameObject.");
        }

        if (_controlPanel)
        {
            _joystick = _controlPanel.transform.Find("Movement").GetComponent<Joystick>();
            _attackButton = _controlPanel.transform.Find("Attack").GetComponent<ButtonInteraction>();
            _jumpButton = _controlPanel.transform.Find("Jump").GetComponent<ButtonInteraction>();
            _dashButton = _controlPanel.transform.Find("Dash").GetComponent<ButtonInteraction>();
            _skillButton = _controlPanel.transform.Find("Skill").GetComponent<ButtonInteraction>();
            if (_joystick == null || _attackButton == null || _jumpButton == null || _dashButton == null || _skillButton == null)
            {
                Debug.LogError("One or more control buttons are missing from the control panel.");
            }
        }

        _playerData = GameSaveManager.Instance().LoadPlayerData();
        if (_playerData == null)
        {
            _playerData = new PlayerData();
        }

        UpdatePlayerData(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        _direction = _joystick.Direction;
        _currentSpeed = GetCurrentSpeed(_dashButton.isPressed);
        _velocity = new Vector3(_direction.x * _currentSpeed * Time.fixedDeltaTime, 0.0f, _direction.y * _currentSpeed * Time.fixedDeltaTime);
        Movement(_velocity);

        if (_jumpButton.isPressed)
        {
            Jump(isGround);
        }

        _currentDamage = GetAttackForce(_skillButton.isPressed);

        if (_attackButton.isPressed)
        {
            Attack(_currentDamage);
        }
        else if (_skillButton.isPressed && _skillButton.isProgressCompleted)
        {
            Skill(_currentDamage);
        }

        if(!_attackButton.isPressed)
        {
            ProjectilePoolManager.Instance.ResetAttack();
        }

    }
    float GetCurrentSpeed(bool isDashing)
    {
        return isDashing ? _dashForce * _speed : _speed;
    }
    void Movement(Vector3 velocity)
    {
        _controller.Move(velocity);
    }
    public void Jump(bool isGround)
    {
        Debug.Log("Jump");
    }

    public void PutWeapon(GameObject weapon)
    {
        weapon.transform.SetParent(weaponHolder);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        isWeaponEquipped = true;
    }

    float GetAttackForce(bool isSkilling)
    {
        return isSkilling ? _attackForce * _damage : _damage;
    }
    public void Attack(float damage)
    {
        if(isWeaponEquipped)
        { ProjectilePoolManager.Instance.Initiate(weaponHolder, attackTarget); }
    }

    public void Skill(float damage)
    {
        Debug.Log("Skill");
    }

    public void ResetSkill()
    {
        Debug.Log("SkillStop");
    }
    public void UpdatePlayerData(float hpAdded, float xpAdded)
    {
        _playerData.hp += hpAdded;
        _playerData.xp += xpAdded;
        _playerData.hp = UnityEngine.Mathf.Clamp(_playerData.hp, 0f, 100f);
        NotifyObservers(_playerData);
    }

    public void UpdatePlayerData(int level)
    {
        _playerData.level = level;

        NotifyObservers(_playerData);
    }
}
