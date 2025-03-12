using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwapper : MonoBehaviour
{
    [SerializeField] private string sceneName; // The name of the scene to load
    [SerializeField] private GameObject confirmationPanel; // The UI panel for the confirmation
    [SerializeField] private Text confirmationText; // Optional: A text element for the prompt message

    private void Start()
    {
        if (confirmationPanel != null)
        {
            confirmationPanel.SetActive(false); // Ensure the confirmation panel starts hidden
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerManager player = other.gameObject.GetComponent<PlayerManager>();
        if (player)
        {
            Debug.Log("Player entered the trigger, showing confirmation panel.");
            ShowConfirmation();
            
            SceneManager.LoadScene(sceneName);
        }
    }

    public void ShowConfirmation()
    {
        if (confirmationPanel != null)
        {
            confirmationPanel.SetActive(true); // Show the confirmation panel
            if (confirmationText != null)
            {
                confirmationText.text = "Do you want to switch scenes to " + sceneName + "?";
            }
        }
        else
        {
            Debug.LogError("Confirmation panel is not assigned in the Inspector!");
        }
    }

    public void OnConfirmYes()
    {
        Debug.Log("Yes clicked: Loading scene " + sceneName);
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName); // Load the specified scene
        }
        else
        {
            Debug.LogError("Scene name is not set!");
        }
    }

    public void OnConfirmNo()
    {
        Debug.Log("No clicked: Hiding confirmation panel.");
        if (confirmationPanel != null)
        {
            confirmationPanel.SetActive(false); // Hide the confirmation panel
        }
    }
}