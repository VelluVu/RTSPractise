using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragUi : EventTrigger {

    public float currentX;
    RectTransform button;
    public float offset;
    public VerticalLayoutGroup parent;
    public Transform parentObject;
    public bool dragging = false;

    // Use this for initialization
    void Start () {

        currentX = gameObject.GetComponent<RectTransform>().position.x;
        button = gameObject.GetComponent<RectTransform>();
        offset = 175f;
        parent = gameObject.GetComponentInParent<VerticalLayoutGroup>();
        parentObject = gameObject.transform.parent;

	}
	
	// Update is called once per frame
	void Update () {

        if (dragging == true)
        {

            GameObject gameObject = parentObject.gameObject;
            if(button.GetSiblingIndex() != 3 && Input.mousePosition.y < parentObject.GetChild(button.GetSiblingIndex()+1).GetComponent<RectTransform>().position.y)
            {
                Debug.Log("Siirretään alaspäin");
                button.SetSiblingIndex(button.GetSiblingIndex() + 1);

            }

            if(button.GetSiblingIndex() != 0 && Input.mousePosition.y > parentObject.GetChild(button.GetSiblingIndex() -1).position.y)
            {
                Debug.Log("Siirretään ylös");
                button.SetSiblingIndex(button.GetSiblingIndex() - 1);
            }

        }

	}

    public override void OnDrag(PointerEventData eventData)
    {

        dragging = true;
        button.position = new Vector2(currentX, Input.mousePosition.y);
        
    }

    public override void OnEndDrag(PointerEventData eventData)
    {

        dragging = false;
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentObject.gameObject.GetComponent<RectTransform>());
        GameObject.Find("CharacterFormation").GetComponent<CharacterPosition>().UpdatePlayerArray(parentObject);
    }

}
