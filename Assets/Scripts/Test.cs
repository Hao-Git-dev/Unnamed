using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour, IPointerClickHandler
{
    public Image image;

    public void OnPointerClick(PointerEventData eventData)
    {
        TestManager.instance.test1 = this;
    }

    private void Start() {
        image = GetComponent<Image>();
    }
}
