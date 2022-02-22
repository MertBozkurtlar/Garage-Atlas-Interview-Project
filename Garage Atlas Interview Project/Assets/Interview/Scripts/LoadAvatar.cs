using UnityEngine;
using Wolf3D.ReadyPlayerMe.AvatarSDK;
using Cinemachine;

public class LoadAvatar : MonoBehaviour
{
    [SerializeField] private string AvatarURL = "https://d1a370nemizbjq.cloudfront.net/209a1bc2-efed-46c5-9dfd-edc8a1d9cbe4.glb";
    [SerializeField] private Transform _cameraMainTransform;
    [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;
    [SerializeField] private Vector3 _initialPosition;

    private void Start()
    {
        if (_cameraMainTransform == null || _cinemachineFreeLook == null)
        {
            Debug.Log("Avatar Loader - A component is missing");
            return;
        }
        Debug.Log($"Started loading avatar. [{Time.timeSinceLevelLoad:F2}]");
        AvatarLoader avatarLoader = new AvatarLoader();
        avatarLoader.LoadAvatar(AvatarURL, OnAvatarImported, OnAvatarLoaded);
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
    }
}
