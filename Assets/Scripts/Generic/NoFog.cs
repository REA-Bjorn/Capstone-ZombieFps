using UnityEngine;
using UnityEngine.Rendering;

public class NoFog : MonoBehaviour
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
            RenderSettings.fog = false;
        }
    }

    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext arg1, Camera arg2)
    {
        if (thisCam == arg2)
        {
            RenderSettings.fog = true;
        }
    }
}
