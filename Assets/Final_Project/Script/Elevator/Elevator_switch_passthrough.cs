using Meta.XR.MRUtilityKit;
using Meta.XR.MRUtilityKitSamples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_switch_passthrough : MonoBehaviour
{
    /// <summary>
    /// modify version of KeyboardManager.cs
    /// </summary>

    #region Singleton 
    public static Elevator_switch_passthrough instance;
    public Animator floor_animator;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Elevator_switch_passthrough found!");
            // Destroy(gameObject);
            return;
        }
        else
        {
            // DontDestroyOnLoad(gameObject);
            instance = this;
        }
        switch_to_realworld();
    }

    #endregion
    [SerializeField]
    GameObject _prefab;

    [SerializeField]
    OVRPassthroughLayer _passthroughLayer;

    public void OnTrackableAdded(MRUKTrackable trackable)
    {
        Debug.Log($"Detected new {trackable.TrackableType} with {trackable.name}");

        if (trackable.TrackableType != OVRAnchor.TrackableType.Keyboard)
        {
            // We only care about keyboards
            return;
        }

        // Instantiate the prefab
        var newGameObject = Instantiate(_prefab, trackable.transform);

        // Hook everything up
        var boundaryVisualizer = newGameObject.GetComponentInChildren<Bounded3DVisualizer>();
        if (boundaryVisualizer)
        {
            boundaryVisualizer.Initialize(_passthroughLayer, trackable);
        }
    }

    public void OnTrackableRemoved(MRUKTrackable trackable)
    {
        Debug.Log($"Removing GameObject '{trackable.name}'");
        Destroy(trackable.gameObject);
    }

    void Update()
    {
        if (_passthroughLayer && OVRInput.GetDown(OVRInput.RawButton.A)) // OVRInput.GetDown(OVRInput.RawButton.A)
        {
            _passthroughLayer.enabled = false;

            switch (_passthroughLayer.projectionSurfaceType)
            {
                case OVRPassthroughLayer.ProjectionSurfaceType.Reconstructed:
                    {
                        _passthroughLayer.projectionSurfaceType = OVRPassthroughLayer.ProjectionSurfaceType.UserDefined;
                        _passthroughLayer.overlayType = OVROverlay.OverlayType.Overlay;
                        Camera.main.clearFlags = CameraClearFlags.Skybox;
                        break;
                    }
                case OVRPassthroughLayer.ProjectionSurfaceType.UserDefined:
                    {
                        _passthroughLayer.projectionSurfaceType = OVRPassthroughLayer.ProjectionSurfaceType.Reconstructed;
                        _passthroughLayer.overlayType = OVROverlay.OverlayType.Underlay;
                        Camera.main.clearFlags = CameraClearFlags.SolidColor;
                        break;
                    }
            }

            _passthroughLayer.enabled = true;
        }
    }

    public void switch_to_realworld()
    {
        // Toggle between full passthrough and surface-projected passthrough
        _passthroughLayer.projectionSurfaceType = OVRPassthroughLayer.ProjectionSurfaceType.Reconstructed;
        _passthroughLayer.overlayType = OVROverlay.OverlayType.Underlay;
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        floor_animator.SetInteger("floor", -1);
    }

    public void switch_to_virtualworld(int button_id)
    {
        // Toggle between full passthrough and surface-projected passthrough
        _passthroughLayer.projectionSurfaceType = OVRPassthroughLayer.ProjectionSurfaceType.UserDefined;
        _passthroughLayer.overlayType = OVROverlay.OverlayType.Overlay;
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        floor_animator.SetInteger("floor", button_id);
    }
}
