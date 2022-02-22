using UnityEngine;
using Wolf3D.ReadyPlayerMe.AvatarSDK;
using Cinemachine;
using TMPro;

public class LoadAvatar : MonoBehaviour
{
    [SerializeField] private TMP_InputField _InputField;
    [SerializeField] private Transform _cameraMainTransform;
    [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;
    [SerializeField] private Vector3 _initialPosition;
    [SerializeField] private GameObject _UIAvatarSelectionCanvas;
    [SerializeField] private GameObject _UILoadingCanvas;

    private void Start()
    {
        if (_cameraMainTransform == null || _cinemachineFreeLook == null)
        {
            Debug.Log("Avatar Loader - A component is missing");
            return;
        }
    }
    public void LoadAvatarFromInput()
    {
        Debug.Log($"Started loading avatar. [{Time.timeSinceLevelLoad:F2}]");
        AvatarLoader avatarLoader = new AvatarLoader();
        avatarLoader.LoadAvatar(_InputField.text, OnAvatarImported, OnAvatarLoaded);
        if (AvatarLoaderBase.canLoad)
        {
            _UIAvatarSelectionCanvas.SetActive(false);
            _UILoadingCanvas.SetActive(true);
        }
        else
        {
            _InputField.text = "URL is invalid..";
            AvatarLoaderBase.canLoad = true;
        }

    }

    public void LoadAvatarFromURL(string avatarURL)
    {
        Debug.Log($"Started loading avatar. [{Time.timeSinceLevelLoad:F2}]");
        AvatarLoader avatarLoader = new AvatarLoader();
        avatarLoader.LoadAvatar(avatarURL, OnAvatarImported, OnAvatarLoaded);
        _UIAvatarSelectionCanvas.SetActive(false);
        _UILoadingCanvas.SetActive(true);
    }
    private void OnAvatarImported(GameObject avatar)
    {
        Debug.Log($"Avatar imported. [{Time.timeSinceLevelLoad:F2}]");
    }

    private void OnAvatarLoaded(GameObject avatar, AvatarMetaData metaData)
    {
        Debug.Log($"Avatar loaded. [{Time.timeSinceLevelLoad:F2}]\n\n{metaData}");
        Debug.Log("Game Object is: ");

        // Initiliaze the character object
        avatar.transform.position = _initialPosition;
        avatar.AddComponent<PlayerMovement>();
        avatar.GetComponent<PlayerMovement>().cameraMainTransform = _cameraMainTransform;
        _cinemachineFreeLook.Follow = avatar.transform;
        _cinemachineFreeLook.LookAt = avatar.transform;

        // Hide the UI
        _UILoadingCanvas.SetActive(false);
    }
}
