using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Color pressedFrameColor;
    public Color pressedImageColor;
    public float rotationSpeed = 300f;
    public bool isPressed = false;

    private Image _frame;
    private Image _image;
    private GameObject _ring;
    private Color _originalFrameColor;
    private Color _originalImageColor;


    void Start()
    {
        _frame = transform.Find("Frame").GetComponent<Image>();
        _image = transform.Find("Image").GetComponent<Image>();
        if(transform.Find("Ring") != null)
        {
            _ring = transform.Find("Ring").gameObject;
        }
            
        if (_frame != null)
        {
            _originalFrameColor = _frame.color;
        }
        if (_image != null)
        {
            _originalImageColor = _image.color;
        }
    }

    void Update()
    {
        if (_ring != null)
        {
            if (isPressed)
            {
                _ring.GetComponent<RectTransform>().Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
            }

            _ring.GetComponent<Image>().enabled = isPressed;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_ring != null)
        {
            isPressed = true;
        }
        if (_frame != null)
        {
            _frame.color = pressedFrameColor;
        }
        if (_image != null)
        {
            _image.color = pressedImageColor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_ring != null)
        {
            isPressed = false;
            _ring.GetComponent<RectTransform>().eulerAngles = Vector3.zero;
        }
        if (_frame != null)
        {
            _frame.color = _originalFrameColor;
        }
        if (_image != null)
        {
            _image.color = _originalImageColor;
        }
    }
}
