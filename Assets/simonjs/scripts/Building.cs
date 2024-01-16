using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject visualObject;
    private Material[] finishMaterials;
    private MeshRenderer[] meshes;
    private UnitScript unitScript;
    
   
    public void Begin()
    {
        unitScript = GetComponent<UnitScript>();
        unitScript.enabled = false;
        unitScript.gameObject.GetComponent<Collider>().enabled = false;
        meshes = GetComponentsInChildren<MeshRenderer>();
        finishMaterials = new Material[meshes.Length];
        for (int i = 0; i < meshes.Length; i++)
        {
            finishMaterials[i] = meshes[i].material;
            meshes[i].material = TowerDraw.instance.canBuild;
        }

    }
    public void finishBuild()
    {
        unitScript.enabled = true;
        unitScript.gameObject.GetComponent<Collider>().enabled = true;
        for (int i = 0; i < meshes.Length; i++)
        {

            meshes[i].material = finishMaterials[i];
        }
    }
}
