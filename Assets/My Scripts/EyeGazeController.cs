using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Profiling;
using ViveSR.anipal.Eye;

public class EyeGazeController : MonoBehaviour
{
    private RaycastHit hit;
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
        Profiler.BeginSample("EyeGazeController.Update");

        if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
            SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT)
        {
            Profiler.EndSample();
            return;
        }

        Profiler.BeginSample("Update Eye Data Callback");
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
        Profiler.EndSample(); // End "Update Eye Data Callback"

        foreach (GazeIndex index in GazePriority)
        {
            Profiler.BeginSample("RayCast and Gaze Interaction");
            Ray GazeRay;
            bool eye_focus;
            if (eye_callback_registered)
                eye_focus = RayCast(index, out GazeRay, out hit, 0, rayDistance, eyeData);
            else
                eye_focus = RayCast(index, out GazeRay, out hit, 0, rayDistance);

            if (eye_focus)
            {
                GazeInteractable gazeInteractable = hit.transform.GetComponent<GazeInteractable>();

                if (gazeInteractable != null)
                {
                    if (currentGazeInteractable == gazeInteractable)
                    {
                        gazeInteractable.gazeInteract(hit.distance);
                    }
                    else
                    {
                        if (currentGazeInteractable != null) currentGazeInteractable.gazeInteractEnd();
                        currentGazeInteractable = gazeInteractable;
                        gazeInteractable.onFirstLook(hit.distance);
                    }
                }
                else
                {
                    if (currentGazeInteractable != null) currentGazeInteractable.gazeInteractEnd();
                    currentGazeInteractable = null;
                }
                Profiler.EndSample(); // End "RayCast and Gaze Interaction"
                break;
            }
            else
            {
                if (currentGazeInteractable != null) currentGazeInteractable.gazeInteractEnd();
                currentGazeInteractable = null;
            }
            Profiler.EndSample(); // End "RayCast and Gaze Interaction"
        }

        Profiler.EndSample(); // End "EyeGazeController.Update"
    }

    private bool RayCast(GazeIndex index, out Ray ray, out RaycastHit hit, float radius, float maxDistance, EyeData eye_data)
    {
        Profiler.BeginSample("RayCast (with EyeData)");
        bool valid = SRanipal_Eye.GetGazeRay(index, out ray, eye_data);
        if (valid)
        {
            Ray rayGlobal = new Ray(Camera.main.transform.position, Camera.main.transform.TransformDirection(ray.direction));
            valid = Physics.Raycast(rayGlobal, out hit, maxDistance);
        }
        else
        {
            hit = new RaycastHit();
        }
        Profiler.EndSample(); // End "RayCast (with EyeData)"
        return valid;
    }

    private bool RayCast(GazeIndex index, out Ray ray, out RaycastHit hit, float radius, float maxDistance)
    {
        Profiler.BeginSample("RayCast (without EyeData)");
        SRanipal_Eye.UpdateData();
        EyeData EyeData_ = SRanipal_Eye.getEyeData();
        bool result = RayCast(index, out ray, out hit, radius, maxDistance, EyeData_);
        Profiler.EndSample(); // End "RayCast (without EyeData)"
        return result;
    }

    private static void EyeCallback(ref EyeData eye_data)
    {
        Profiler.BeginSample("EyeCallback");
        eyeData = eye_data;
        Profiler.EndSample(); // End "EyeCallback"
    }
}
