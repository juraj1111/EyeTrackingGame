using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using ViveSR.anipal.Eye;

public class EyeTracking: MonoBehaviour
{
    private FocusInfo focusInfo;
    private static EyeData eyeData = new EyeData();
    private GazeIndex[] GazePriority = new GazeIndex[] { GazeIndex.COMBINE, GazeIndex.LEFT, GazeIndex.RIGHT };
    [SerializeField] private float rayDistance = 20f;
    private bool eye_callback_registered = false;
    private GazeInteractable currentGazeInteractable = null;

    private void Start()
    {
        if (!SRanipal_Eye_Framework.Instance.EnableEye)
        {
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
            SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT) return;

        if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == true && eye_callback_registered == false)
        {
            SRanipal_Eye.WrapperRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
            eye_callback_registered = true;
        }
        else if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == false && eye_callback_registered == true)
        {
            SRanipal_Eye.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
            eye_callback_registered = false;
        }

        foreach (GazeIndex index in GazePriority)
        {
            Ray GazeRay;
            bool eye_focus;
            if (eye_callback_registered)
                eye_focus = RayCast(index, out GazeRay, out focusInfo, 0, rayDistance, eyeData);
            else
                eye_focus = RayCast(index, out GazeRay, out focusInfo, 0, rayDistance);

            if (eye_focus)
            {
                GazeInteractable gazeInteractable = focusInfo.transform.GetComponent<GazeInteractable>();
                //Debug.Log("Gaze Interactable: " + gazeInteractable);
                //GameObject hitObject = focusInfo.transform.gameObject;
                //Debug.Log("Hit Object Name: " + hitObject.name);

                if (gazeInteractable != null)
                {
                    //if (gazeInteractable.hasGazeInteraction())
                    //{
                        if (currentGazeInteractable == gazeInteractable)
                        {
                            gazeInteractable.gazeInteract(focusInfo.distance);
                        }
                        else
                        {
                            if (currentGazeInteractable != null) currentGazeInteractable.gazeInteractEnd();
                            currentGazeInteractable = gazeInteractable;
                            gazeInteractable.onFirstLook(focusInfo.distance);
                        }
                    //}
                }
                else
                {
                    if (currentGazeInteractable != null) currentGazeInteractable.gazeInteractEnd();
                    currentGazeInteractable = null;
                }
                break;
            }
            else
            {
                if (currentGazeInteractable != null) currentGazeInteractable.gazeInteractEnd();
                currentGazeInteractable = null;
            }
        }
    }

    private bool RayCast(GazeIndex index, out Ray ray, out FocusInfo focusInfo, float radius, float maxDistance, EyeData eye_data)
    {
        bool valid = SRanipal_Eye.GetGazeRay(index, out ray, eye_data);
        if (valid)
        {
            Ray rayGlobal = new Ray(Camera.main.transform.position, Camera.main.transform.TransformDirection(ray.direction));
            RaycastHit hit;
            valid = Physics.Raycast(rayGlobal, out hit, maxDistance);
            focusInfo = new FocusInfo
            {
                point = hit.point,
                normal = hit.normal,
                distance = hit.distance,
                collider = hit.collider,
                rigidbody = hit.rigidbody,
                transform = hit.transform
            };
        }
        else
        {
            focusInfo = new FocusInfo();
        }
        return valid;
    }

    private bool RayCast(GazeIndex index, out Ray ray, out FocusInfo focusInfo, float radius, float maxDistance)
    {
        SRanipal_Eye.UpdateData();
        EyeData EyeData_ = SRanipal_Eye.getEyeData();
        return RayCast(index, out ray, out focusInfo, radius, maxDistance, EyeData_);
    }

    private void Release()
    {
        if (eye_callback_registered == true)
        {
            SRanipal_Eye.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
            eye_callback_registered = false;
        }
    }
    private static void EyeCallback(ref EyeData eye_data)
    {
        eyeData = eye_data;
    }
}
