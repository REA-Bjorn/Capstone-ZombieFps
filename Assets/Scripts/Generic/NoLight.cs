using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NoLight : MonoBehaviour
{
    public Camera thisCam;

    private void Start()
    {
        RenderPipelineManager.beginCameraRendering += RenderPipelineManager_beginCameraRendering;
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }

    private void RenderPipelineManager_beginCameraRendering(ScriptableRenderContext arg1, Camera arg2)
    {
        if (thisCam == arg2)
        {
            RenderSettings.ambientIntensity = 0;
        }
    }

    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext arg1, Camera arg2)
    {
        if (thisCam == arg2)
        {
            RenderSettings.ambientIntensity = 1;
        }
    }
}
