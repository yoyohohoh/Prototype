using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonImageColorChange : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Color pressedFrameColor;
    public Color pressedImageColor;

    private Image _frame;
    private Image _image;
    private Color _originalFrameColor;
    private Color _originalImageColor;

    void Start()
    {
        _frame = transform.Find("Frame").GetComponent<Image>();
        _image = transform.Find("Image").GetComponent<Image>();
        if (_frame != null)
        {
            _originalFrameColor = _frame.color;
        }
        if (_image != null)
        {
            _originalImageColor = _image.color;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
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
