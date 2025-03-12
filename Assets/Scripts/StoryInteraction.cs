using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
public class StoryInteraction : MonoBehaviour
{
    
        public GameObject noteUIPanel;    // The note UI (image with close button)
        public GameObject promptText;          // The UI Text for "Press E to interact"
        public float interactionRange = 3f; // Interaction range
        public Transform player;
   
    private bool isPlayerNearby = false; // Tracks if the player is within range

    void Update()
    {
        // Show note when player presses E
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ShowNote();
        }

        // Close note with Escape
        if (noteUIPanel.activeSelf && Input.GetKeyDown(KeyCode.T))
        {
            CloseNote();
        }
        
        if (promptText != null && isPlayerNearby)
        {
            promptText.transform.position = transform.position + new Vector3(0, .3f, 0);
            promptText.transform.LookAt(Camera.main.transform);
            promptText.transform.Rotate(0, 180, 0);
        }
        }

        private void ShowNote()
        {
            if (noteUIPanel != null)
            {
                noteUIPanel.SetActive(true);
                Time.timeScale = 0f;

        }

            if (promptText != null)
            {
                promptText.gameObject.SetActive(false); // Hide prompt when showing note
            }
        }

        public void CloseNote()
        {
            if (noteUIPanel != null)
            {
                noteUIPanel.SetActive(false);

                
                Time.timeScale = 1f;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) // Check if the Player enters range
            {
                isPlayerNearby = true;

                // Show the prompt
                if (promptText != null)
                {
                    promptText.SetActive(true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) // Check if the Player exits range
            {
                isPlayerNearby = false;

                // Hide the prompt
                if (promptText != null)
                {
                    promptText.SetActive(false);
                }
            }
        }
    }

