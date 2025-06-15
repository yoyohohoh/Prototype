// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerController : Subject
{
    [Header("Movement")]
    private CharacterController _controller;
    private Vector2 _direction;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] public float _speed;
    [SerializeField] public float _dashForce;
    [SerializeField] public float _jumpForce;
    [SerializeField] public float _fallForce;
    [SerializeField] public float _damage;
    [SerializeField] public float _attackForce;

    [Header("Jump")]
    [SerializeField] bool _isGrounded;
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundMask;
    private float _groundRadius = 0.6f;
    private bool _isJumping = false;
    private Coroutine jumpCoroutine;

    [Header("Attack & Skill")]
    public bool isWeaponEquipped = false;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Transform attackTarget;
    [SerializeField] private Transform skillTarget;

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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerData = GameSaveManager.Instance().LoadPlayerData();

        if (_playerData == null)
        {
            _playerData = new PlayerData();
        }

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

        UpdatePlayerData(0f, 0f);


    }
    void FixedUpdate()
    {
        // movement
        _direction = _joystick.Direction;
        _currentSpeed = GetCurrentSpeed(_dashButton.isPressed);

        Movement();

        // jump
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundRadius, _groundMask);

        if (_jumpButton.isPressed && _isGrounded)
        {
            if (_direction == Vector2.zero)
            {
                if (jumpCoroutine == null)
                {
                    _velocity.y += 0.1f;
                    _controller.Move(_velocity);
                    _controller.height = 1.8f;
                    jumpCoroutine = StartCoroutine(JumpWithDelay(0.4f));
                }
                else
                {
                    return;
                }

            }
            else
            {
                Jump();
            }

        }
        _velocity.y += Physics.gravity.y * _fallForce * Time.fixedDeltaTime;
        _controller.Move(_velocity * Time.fixedDeltaTime);


    }

    // Update is called once per frame
    void Update()
    {
        // attack & skill
        _currentDamage = GetAttackForce(_skillButton.isPressed);
        if (_attackButton.isPressed)
        {
            Attack(_currentDamage, attackTarget);
        }
        else if (_skillButton.isPressed && _skillButton.isProgressCompleted)
        {
            Attack(_currentDamage, skillTarget);
        }

        if (!_attackButton.isPressed && !_skillButton.isPressed)
        {
            ProjectilePoolManager.Instance.ResetAttack();
        }
    }
    public float GetCurrentHealth()
    {
        return _playerData.hp;
    }
    public Vector3 GetCurrentPosition()
    {
        return _controller.transform.position;
    }
    float GetCurrentSpeed(bool isDashing)
    {
        return isDashing ? _dashForce * _speed : _speed;
    }
    void RotateHead()
    {
        Vector3 velocity = this.gameObject.GetComponent<CharacterController>().velocity;
        Vector3 horizontalVelocity = new Vector3(velocity.x, 0f, velocity.z);

        if (horizontalVelocity.sqrMagnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(horizontalVelocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void Movement()
    {
        Vector3 moveDirection = transform.right * _direction.x + transform.forward * _direction.y;
        _velocity = moveDirection.normalized * _currentSpeed;
        RotateHead();
    }
    void Jump()
    {
        _velocity.y = Mathf.Sqrt(_jumpForce * 10f * -2.0f * Physics.gravity.y);
        _controller.Move(_velocity * Time.fixedDeltaTime);
        _jumpButton.DiscreteModeButtonPress(false);
        _controller.height = 2.0f;
    }

    IEnumerator JumpWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Jump();
        jumpCoroutine = null;
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
    public void Attack(float damage, Transform target)
    {
        if (isWeaponEquipped)
        { 
            ProjectilePoolManager.Instance.Initiate(weaponHolder, target);
            this.GetComponent<AnimationController>().SetAnimationTrigger("Attack");
            _attackButton.DiscreteModeButtonPress(false);
        }
    }

    public void UpdatePlayerData(float hpAdded, float xpAdded)
    {
        _playerData.hp += hpAdded;
        _playerData.xp += xpAdded;
        _playerData.hp = UnityEngine.Mathf.Clamp(_playerData.hp, 0f, 100f);

        if (_playerData.xp >= _playerData.maxXp)
        {
            _playerData.xp = 0f;
            _playerData.level++;
        }
        NotifyObservers(_playerData);
    }

    public void UpdatePlayerData(int level)
    {
        _playerData.level = level;

        NotifyObservers(_playerData);

        this.GetComponent<AnimationController>().SetAnimationTrigger("levelUp");
    }

    public void UpdatePlayerData(float gold)
    {
        _playerData.gold += gold;

        NotifyObservers(_playerData);
    }
}
