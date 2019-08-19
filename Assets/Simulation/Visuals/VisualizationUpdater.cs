using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationUpdater : MonoBehaviour
{
    Simulation _sim;

    Material _material;

    Texture _originalWaterSandRockSediment;
    Texture _originalVelocity;

    Material _originalMaterialCopy;

    void Start()
    {
        // Simulation
        _sim = GetComponentInParent<Simulation>();

        // Material
        var firstRenderer = GetComponentInChildren<Renderer>();
        _material = firstRenderer.sharedMaterial;
#if UNITY_EDITOR
        _originalMaterialCopy = new Material(_material);
#endif
    }

#if UNITY_EDITOR
    void OnDestroy()
    {
        _material.CopyPropertiesFromMaterial(_originalMaterialCopy);
    }
#endif

    void Update()
    {
        // Update visualization
        _material.SetTexture("_WaterSandRockSediment", _sim.CurrentWaterSandRockSediment);
        _material.SetTexture("_Flux", _sim.CurrentOutflowFluxRLBT);
        _material.SetTexture("_VelocityXY", _sim.CurrentVelocityXY);
        _material.SetFloat("_DT", _sim.UpdateInterval);
        _material.SetFloat("_L", _sim.PipeLength);

        // Debug only?
        _material.SetFloat("_Kc", _sim.SedimentCapacityConstant);
        _material.SetFloat("_ErosionMinimumAngleThresshold", _sim.ErosionMinimumAngleThresshold);
    }
}
