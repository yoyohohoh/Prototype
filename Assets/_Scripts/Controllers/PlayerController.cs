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
    [SerializeField] public float _speed;
    [SerializeField] public float _dashForce;
    [SerializeField] public float _jumpForce;
    private Vector2 _direction;
    [SerializeField] private Vector3 _velocity;
    private CharacterController _controller;

    [Header("Control")]
    [SerializeField] GameObject _controlPanel;
    private Joystick _joystick;
    private ButtonInteraction _attackButton;
    private ButtonInteraction _jumpButton;
    private ButtonInteraction _dashButton;
    private ButtonInteraction _skillButton;

    [Header("HUD")]
    [SerializeField] public PlayerData _playerData;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private Transform weaponHolder;

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
        if(_playerData == null)
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
        
    }

    void Movement(Vector3 velocity)
    {
        _controller.Move(velocity);
    }

    float GetCurrentSpeed(bool isDashing)
    {
        return isDashing ? _dashForce * _speed : _speed;
    }

    public void PutWeapon(GameObject weapon)
    {
        weapon.transform.SetParent(weaponHolder);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
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
