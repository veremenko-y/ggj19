using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Editor
{
    class EditorHelpers
    {
        [MenuItem("GGJ/Face Camera")]
        static void FaceCamera()
        {
            GameObject selection = Selection.activeGameObject;
            if(selection != null)
            {
                selection.transform.LookAt(Camera.main.transform.position, Vector3.up);
            }
        }

        [MenuItem("GGJ/Snap To NavMesh")]
        static void SnapToNavMesh()
        {
            GameObject selection = Selection.activeGameObject;
            NavMeshHit hit;
            if(selection != null && NavMesh.SamplePosition(selection.transform.position, out hit, 100f, -1))
            {
                selection.transform.position = hit.position;
            }
            else
            {
                Debug.LogError("Can't hit nav mesh for selected object");
            }
        }
    }
}
