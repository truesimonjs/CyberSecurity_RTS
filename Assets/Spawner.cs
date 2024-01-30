using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnTime;
    //private Vector3 attackPos;
    public Transform attackPos;
    public TeamManager team;
    private void Start()
    {
        StartCoroutine(Spawn());
    }
    public IEnumerator Spawn()
    {
        while (true)
        {
            UnitScript spawnee = Instantiate(prefab).GetComponent<UnitScript>();
            spawnee.team = team;
            spawnee.AddState(new UnitOrder(typeof(MoveState), attackPos.position));
            yield return new WaitForSeconds(spawnTime);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1);
        Gizmos.DrawWireSphere(attackPos.position, 1);
    }
}
