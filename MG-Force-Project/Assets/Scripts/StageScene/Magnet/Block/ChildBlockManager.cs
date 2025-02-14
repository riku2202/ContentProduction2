using UnityEngine;

namespace Game.StageScene
{
    public class ChildBlockManager : MonoBehaviour
    {
        private void Update()
        {
            if (transform.parent == null) return; // �e�����Ȃ��ꍇ�͉������Ȃ�

            // �e�I�u�W�F�N�g�� BlockObjectManager ���擾
            BlockObjectManager manager = transform.parent.GetComponent<BlockObjectManager>();
            if (manager == null || manager.currentOriginObject == null) return;

            // currentOriginObject ���� MeshFilter �� MeshRenderer ���擾
            MeshFilter originMeshFilter = manager.currentOriginObject.GetComponent<MeshFilter>();
            MeshRenderer originMeshRenderer = manager.currentOriginObject.GetComponent<MeshRenderer>();

            // ���g�� MeshFilter �� MeshRenderer ���擾�i�Ȃ���Βǉ��j
            MeshFilter childMeshFilter = GetComponent<MeshFilter>();
            if (childMeshFilter == null) childMeshFilter = gameObject.AddComponent<MeshFilter>();

            MeshRenderer childMeshRenderer = GetComponent<MeshRenderer>();
            if (childMeshRenderer == null) childMeshRenderer = gameObject.AddComponent<MeshRenderer>();

            // ���b�V���ƃ}�e���A�����R�s�[
            if (originMeshFilter != null) childMeshFilter.sharedMesh = originMeshFilter.sharedMesh;
            if (originMeshRenderer != null) childMeshRenderer.sharedMaterials = originMeshRenderer.sharedMaterials;
        }
    }
}
