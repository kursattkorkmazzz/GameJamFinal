using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    public Vector2 SpawnPointArea;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {

        Vector3 size = new Vector3(SpawnPointArea.x,SpawnPointArea.y,1);
        Gizmos.DrawCube(transform.position, size);
    }
}
