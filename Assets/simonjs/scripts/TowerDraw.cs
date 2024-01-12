using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDraw : MonoBehaviour
{
    [HideInInspector]public GameObject phantomTower;
    private bool placing = false;
    private bool isBuildable;
    private bool wasBuildable;
    private LayerMask rayMask; // ray that projects the building to first thing it hits
    private LayerMask buildMask; //mask for the check of wether or not theres space
    [SerializeField] private Material canBuild;
    [SerializeField] private Material cantBuild;
    private void Awake()
    {
        rayMask = LayerMask.GetMask("Buildable");
        buildMask = LayerMask.GetMask("Buildings");
    }
    public void BeginPlacement(GameObject prefab)
    {
        placing = true;
       phantomTower=Instantiate(prefab.GetComponent<Building>().visualObject);
        updateBuildColor(true);

    }
    public void EndPlacement(bool destroyPhantom)
    {
        if (destroyPhantom && phantomTower!=null)
        {
            Destroy(phantomTower);
        }
        placing = false;

        
    }
    private void Update()
    {
        if (placing)
        {
            updatePosition();
            isBuildable = Physics.OverlapBox(phantomTower.transform.position, new Vector3(1, 1, 1), Quaternion.identity, buildMask).Length == 0;

            updateBuildColor(false);
        }

    }
    private void updatePosition()
    {
        Vector3 mousepos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousepos);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 100, rayMask);

        Vector3 targetPos = hit.point;
        targetPos = new Vector3(Mathf.Round(targetPos.x), targetPos.y, Mathf.Round(targetPos.z));
        phantomTower.transform.position = targetPos;
    }
    private void updateBuildColor(bool isNewBuilding) //isnewbuilding tells it wether or not wasbuildable is reliable as you cant rely on it the first frame that the building exists
    {
        if (isBuildable != wasBuildable || isNewBuilding)
        {
            MeshRenderer[] meshes = phantomTower.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mesh in meshes)
            {
                mesh.material = isBuildable ? canBuild : cantBuild;
            }
        }
        wasBuildable = isBuildable;
    }

}
