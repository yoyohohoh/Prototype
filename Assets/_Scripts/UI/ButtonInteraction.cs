using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using Unity.VisualScripting;

public class ButtonInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed = false;

    [Header("Color")]
    private Color _originalFrameColor;
    private Color _originalImageColor;
    public Color pressedFrameColor;
    public Color pressedImageColor;

    [Header("Components")]
    private Image _frame;
    private Image _image;
    private GameObject _ring;
    private GameObject _progress;
    private GameObject _skillRing;
    private GameObject[] progressParts;
    public bool isRing;
    public bool isProgress;

    [Header("Variables")]
    public float rotationSpeed = 300f;
    public float progressTime = 1.0f;
    public bool isProgressCompleted;

    public int count;

    void Start()
    {
        if (transform.Find("Frame"))
        {
            _frame = transform.Find("Frame").GetComponent<Image>();
            _originalFrameColor = _frame.color;
        }
        if (transform.Find("Image"))
        {
            _image = transform.Find("Image").GetComponent<Image>();
            _originalImageColor = _image.color;
        }

        if (transform.Find("Ring"))
        { 
            _ring = transform.Find("Ring").gameObject;
            _ring.SetActive(isRing);
        }
        if(transform.Find("Progress"))
        { 
            _progress = transform.Find("Progress").gameObject;
            _progress.SetActive(isProgress);
            _skillRing = transform.Find("SkillRing").gameObject;
            _skillRing.SetActive(isProgress);

            Transform parent = _progress.transform;
            progressParts = new GameObject[parent.childCount];
            for (int i = 0; i < parent.childCount; i++)
            {
                progressParts[i] = parent.GetChild(i).gameObject;
            }

            StartCoroutine(ProgressControl(progressTime));
        }
    }

    void Update()
    {

        if (isRing)
        {
            ButtonRotation(_ring, isPressed);
        }

        if (isProgress)
        {
            ButtonRotation(_skillRing, isPressed && isProgressCompleted);
        }

    }

    IEnumerator ProgressControl(float waitingTime)
    {
        if (isProgress)
        {
            
            foreach (GameObject part in progressParts)
            {
                part.SetActive(false);
            }

            foreach (GameObject part in progressParts)
            {
                part.SetActive(true);
                yield return new WaitForSeconds(waitingTime);
            }
            isProgressCompleted = true;
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isProgress)
        {
            StopCoroutine(ProgressControl(0f));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isProgress)
        {
            StartCoroutine(ProgressControl(progressTime));
        }
    }

    void ButtonRotation(GameObject button, bool isActive)
    {
        button.GetComponent<Image>().enabled = isActive;
        if(isActive)
        { button.GetComponent<RectTransform>().Rotate(0f, 0f, -rotationSpeed * Time.deltaTime); }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
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
        isPressed = false;
        if (_frame != null)
        {
            _frame.color = _originalFrameColor;
        }
        if (_image != null)
        {
            _image.color = _originalImageColor;
        }
        if (isRing)
        {
            _ring.GetComponent<RectTransform>().eulerAngles = Vector3.zero;
        }
    }
}
