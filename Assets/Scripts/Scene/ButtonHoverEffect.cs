using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject underline;
    
    void Start()
    {
        underline = transform.Find("Underline").gameObject;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        underline.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        underline.SetActive(false);
    }
}
