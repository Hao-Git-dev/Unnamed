using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour, IPointerClickHandler
{
    public Image image;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (TestManager.instance.start == null)
        {
            image.color = Color.gray;
            TestManager.instance.start = this;
        }
        else if(TestManager.instance.end == null)
        {
            TestManager.instance.end = this;
            image.color = Color.gray;

            TestManager.instance.Test();
            TestManager.instance.start = null;
            TestManager.instance.end = null;
        }    
        
    }

    private void Start() {
        image = GetComponent<Image>();
    }
}
