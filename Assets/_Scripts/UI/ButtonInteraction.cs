using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using Unity.VisualScripting;
using System.Linq;

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
    public int progressPartsCount;
    public bool isProgressCompleted;

    private Coroutine progressCoroutine;

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

            progressPartsCount = _progress.transform.childCount;
            progressParts = new GameObject[progressPartsCount];
            for (int i = 0; i < progressPartsCount; i++)
            {
                progressParts[i] = _progress.transform.GetChild(i).gameObject;
            }

            progressCoroutine = StartCoroutine(ProgressControl(progressTime));
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
            isProgressCompleted = progressParts.All(part => part.activeSelf);
        }
    }

    IEnumerator ProgressControl(float waitingTime)
    {
        if (isProgress && !isProgressCompleted)
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
        }
    }

    void ButtonRotation(GameObject button, bool isActive)
    {
        button.GetComponent<Image>().enabled = isActive;
        if(isActive)
        { 
            button.GetComponent<RectTransform>().Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);           
        }
    }
    void ResetFireRing()
    {
        if (isPressed)
        {
            StopFireRing();
        }
    }
    public void StopFireRing()
    {
        if (isProgress)
        {
            isProgressCompleted = false;
            if (progressCoroutine != null)
            {
                StopCoroutine(progressCoroutine);
            }
            foreach (GameObject part in progressParts)
            {
                part.SetActive(false);
            }
            if(!isPressed)
            {
                progressCoroutine = StartCoroutine(ProgressControl(progressTime));
            }
            
        }
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

        if (isProgress)
        {
            if(!isProgressCompleted)
            {
                StopFireRing();
            }
            else if (isProgressCompleted)
            {
                Invoke("ResetFireRing", progressTime * _progress.transform.childCount);
            }               
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

        if (isProgress)
        {
            StopFireRing();
        }
    }
}
