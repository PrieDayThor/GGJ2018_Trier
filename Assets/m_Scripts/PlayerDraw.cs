using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerDraw : NetworkBehaviour {

    public Color PlayerColor = Color.black;
    public int brushSize = 5;

    #region Initialization
    [Server]
    private void Start() {
        var data = DrawArea.GetAllTextureData();
        var zippeddata = data.Compress();

        RpcSendFullTexture(zippeddata);
    }

    [ClientRpc]
    private void RpcSendFullTexture(byte[] textureData) {
        DrawArea.SetAllTextureData(textureData.Decompress());
    }
    #endregion

    private void Update() {
        if (Input.GetMouseButton(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                var pallet = hit.collider.GetComponent<DrawArea>();
                if (pallet != null) {
                    Debug.Log(hit.textureCoord);
                    Debug.Log(hit.point);

                    Renderer rend = hit.transform.GetComponent<Renderer>();
                    MeshCollider meshCollider = hit.collider as MeshCollider;

                    if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
                        return;

                    Texture2D tex = rend.material.mainTexture as Texture2D;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;

                    CmdBrushAreaWithColorOnServer(pixelUV, PlayerColor, brushSize);
                    BrushAreaWithColor(pixelUV, PlayerColor, brushSize);
                }
            }
        }
    }

    [Command]
    private void CmdBrushAreaWithColorOnServer(Vector2 pixelUV, Color color, int size) {
        RpcBrushAreaWithColorOnClients(pixelUV, color, size);
        BrushAreaWithColor(pixelUV, color, size);
    }

    [ClientRpc]
    private void RpcBrushAreaWithColorOnClients(Vector2 pixelUV, Color color, int size) {
        BrushAreaWithColor(pixelUV, color, size);
    }

    private void BrushAreaWithColor(Vector2 pixelUV, Color color, int size) {
        for (int x = -size; x < size; x++) {
            for (int y = -size; y < size; y++) {
                DrawArea.Texture.SetPixel((int)pixelUV.x + x, (int)pixelUV.y + y, color);
            }
        }

        DrawArea.Texture.Apply();
    }
}
