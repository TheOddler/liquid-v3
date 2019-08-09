using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAdder : MonoBehaviour
{
    [SerializeField]
    Simulation _simulation;

    [SerializeField]
    [Tooltip("Size of the mesh of the simulation")]
    float _simulationSize = 10;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 castPosition = ray.origin + ray.direction * (ray.origin.y - transform.position.y) / -ray.direction.y;
        transform.position = castPosition;

        if (Input.GetMouseButton(0))
        {
            Vector2 posInSim = new Vector2(
                _simulationSize / 2f + (castPosition.x - _simulation.transform.position.x),
                _simulationSize / 2f - (castPosition.z - _simulation.transform.position.z)
                ) / _simulationSize;
            _simulation.AddWaterSandRockSediment(posInSim, 2f, new Vector4(1f * Time.deltaTime, 0, 0, 0));
        }
    }
}
