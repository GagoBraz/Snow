using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(TMP_Text))]
public class ColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField]
    Color32 HighlightedColor = new Color32(0, 0, 0, 1);
    [SerializeField]
    Color32 PressedColor = new Color32(0, 0, 0, 1);
    [SerializeField]
    Color32 NormalColor = new Color32(0, 0, 0, 1);


    private Button _button;
    private TMP_Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _button = this.GetComponent<Button>();
        _text = this.GetComponent<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.color = HighlightedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.color = NormalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _text.color = PressedColor;
    }
    
}
