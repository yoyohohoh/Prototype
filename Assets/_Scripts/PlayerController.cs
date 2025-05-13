using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movements")]
    [SerializeField] public float _speed = 5f;
    [SerializeField] private Vector2 _move;
    [SerializeField] private CharacterController _controller;

    [Header("Joystick")]
    [SerializeField] private Joystick _joystick;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("CharacterController component is missing from the GameObject.");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player Position: (" + this.transform.position.x + "," + this.transform.position.x + ")");
        _move = _joystick.Direction;
        Vector3 movement = new Vector3(_move.x * _speed * Time.fixedDeltaTime, 0.0f, _move.y * _speed * Time.fixedDeltaTime);
        _controller.Move(movement);
    }
}
