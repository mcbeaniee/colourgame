using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TexturePaint))]
public class TexturePaintEditor : Editor {

private bool fl = false;
private bool fe = false;

private Texture2D tt;

    void OnSceneGUI()
    {
        Event e = Event.current;
        
        int controlID = GUIUtility.GetControlID(FocusType.Passive);
        switch (Event.current.GetTypeForControl(controlID))
        {
                case EventType.MouseMove:
                    if(fl || fe)
                    {
                    Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                    RaycastHit hit;
        
                    if ( Physics.Raycast( ray, out hit, Mathf.Infinity))
                        {
                            Renderer rend = hit.transform.GetComponent<Renderer>();
                            MeshCollider meshCollider = hit.collider as MeshCollider;

                            if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
                                return;

                            Texture2D tex = rend.material.GetTexture("_M2") as Texture2D;
                            Vector2 pixelUV = hit.textureCoord2;
                            pixelUV.x *= tex.width;
                            pixelUV.y *= tex.height;

                            if(fl)
                                tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black); // draw
                            else if(fe)
                                tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.clear); // erase

                            tex.Apply();
                            tt = tex;
                        }
                    }
                break;
        case EventType.KeyDown:
        if(e.character == 'u') // switch to draw
        {
        fe = false;
        fl = !fl;
        Debug.Log(fl);
        if(!fl)
            SaveTexture(tt);
        }
        else if(e.character == 'y') // switch to erase
        {
        fl = false;
        fe = !fe;
        Debug.Log(fe);
        if(!fe)
            SaveTexture(tt);
        }
                break;
        }
    }

    protected void SaveTexture(Texture2D texture)
    {
     var path = AssetDatabase.GetAssetPath(texture);
     System.IO.File.WriteAllBytes(path, texture.EncodeToPNG());
     Debug.Log(path);
    }
}