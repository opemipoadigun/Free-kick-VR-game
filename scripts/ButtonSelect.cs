using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string sceneName;

    public string menu;

    private EventSystem eventSystem;

    private void Start()
    {
        eventSystem = EventSystem.current;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventSystem.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventSystem.SetSelectedGameObject(null);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Load the scene
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        // Check if the Oculus button is clicked
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            // Get the currently selected GameObject
            GameObject selectedObject = eventSystem.currentSelectedGameObject;

            // If a GameObject is selected, simulate a click event on it
            if (selectedObject != null)
            {
                // Create a pointer click event data
                PointerEventData pointerEventData = new PointerEventData(eventSystem);
                pointerEventData.button = PointerEventData.InputButton.Left; // Simulate left mouse button click

                // Execute the pointer click event on the selected GameObject
                ExecuteEvents.Execute(selectedObject, pointerEventData, ExecuteEvents.pointerClickHandler);
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            // Load the menu scene
            SceneManager.LoadScene(menu);
        }
    }
}

