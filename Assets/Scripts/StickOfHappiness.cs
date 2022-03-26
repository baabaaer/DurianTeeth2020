using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StickOfHappiness : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("Images")]
    public Image background;
    public Image handle;
    
    [Header("Values")]
    public float handleRange;
    public bool choice = true;

    private Vector3 inputVector;
    
    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background.rectTransform,
            ped.position,
            ped.pressEventCamera,
            out pos))
            {
                pos.x = (pos.x / background.rectTransform.sizeDelta.x);
                pos.y = (pos.y / background.rectTransform.sizeDelta.y);

                inputVector = new Vector3(pos.x, 0, pos.y);
                inputVector = (inputVector.magnitude > 0.5f)?inputVector.normalized:inputVector;
                

            //Debug.Log(pos);
            }

        // Move joystick handle
        handle.rectTransform.anchoredPosition = new Vector3(inputVector.x * (background.rectTransform.sizeDelta.x / 3f)
                                             ,inputVector.z * (background.rectTransform.sizeDelta.y / 3f)); // <- Wait, but why the because?

        // Because the UI has X and Z coordinates if vector3. Y coordinate means up or normal from UI, which will look like static because UI has no thickness dimension
    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    
    public void OnPointerUp(PointerEventData ped)
    {
        // Snaps back to zero once not used!
        inputVector = Vector3.zero;
        handle.rectTransform.anchoredPosition = Vector3.zero;
    }

    // Allows option for using keyboard or joystick, and separated by horizontal and vertical axis
    public float Horizontal()
    {   
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }
     
}
