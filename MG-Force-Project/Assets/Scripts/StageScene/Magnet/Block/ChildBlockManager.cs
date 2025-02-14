using UnityEngine;

namespace Game.StageScene
{
    public class ChildBlockManager : MonoBehaviour
    {
        private void Update()
        {
            if (transform.parent == null) return; // 親がいない場合は何もしない

            // 親オブジェクトの BlockObjectManager を取得
            BlockObjectManager manager = transform.parent.GetComponent<BlockObjectManager>();
            if (manager == null || manager.currentOriginObject == null) return;

            // currentOriginObject から MeshFilter と MeshRenderer を取得
            MeshFilter originMeshFilter = manager.currentOriginObject.GetComponent<MeshFilter>();
            MeshRenderer originMeshRenderer = manager.currentOriginObject.GetComponent<MeshRenderer>();

            // 自身の MeshFilter と MeshRenderer を取得（なければ追加）
            MeshFilter childMeshFilter = GetComponent<MeshFilter>();
            if (childMeshFilter == null) childMeshFilter = gameObject.AddComponent<MeshFilter>();

            MeshRenderer childMeshRenderer = GetComponent<MeshRenderer>();
            if (childMeshRenderer == null) childMeshRenderer = gameObject.AddComponent<MeshRenderer>();

            // メッシュとマテリアルをコピー
            if (originMeshFilter != null) childMeshFilter.sharedMesh = originMeshFilter.sharedMesh;
            if (originMeshRenderer != null) childMeshRenderer.sharedMaterials = originMeshRenderer.sharedMaterials;
        }
    }
}
