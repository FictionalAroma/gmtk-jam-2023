using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.TerrainTools;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UIElements;



[CanEditMultipleObjects]
public class ChangeTagOrLayer : EditorWindow
{
    // Start is called before the first frame update
    string oldTagOrLayer;
    string newTagOrLayer;
    public string[] options = new string[] { "Tag", "Layer" };
    public int index = 0;
    [MenuItem("Tools/Change Tag Or Layer")]
    public static void OpenEditorWindow()
    {
        GetWindow(typeof(ChangeTagOrLayer));
    }
    public void OnGUI()
    {
        GUILayout.Label("Old Tag Or Layer");
        oldTagOrLayer = GUILayout.TextField(oldTagOrLayer);
        GUILayout.Label("New Tag Or Layer");
        newTagOrLayer = GUILayout.TextField(newTagOrLayer);
        index = EditorGUILayout.Popup(index, options);

        if (GUILayout.Button("Change"))
        {
            ChangePropety(oldTagOrLayer, newTagOrLayer, index);
        }




    }
    void ChangePropety(string oldTagOrLayer, string newTagOrLayer, int choice)
    {

        var tags = new List<GameObject>();
        var layers = new List<GameObject>();
        var objectstoAdd = FindObjectsOfType<GameObject>();
        switch (index)
        {
             
            case 0:

                foreach (GameObject gameObject in objectstoAdd)
                {
                    if (gameObject.layer == LayerMask.NameToLayer(oldTagOrLayer))
                    {
                        layers.Add(gameObject);
                    }
                }
                foreach (GameObject gameObject in layers)
                {
                    gameObject.layer = LayerMask.NameToLayer(newTagOrLayer);
                }
                break;

            case 1:
               
                foreach (GameObject gameObject in objectstoAdd)
                {

                    if (gameObject.CompareTag(oldTagOrLayer))
                    {
                        tags.Add(gameObject);
                    }
                }

                foreach (GameObject gameObject in tags)
                {
                    gameObject.tag = newTagOrLayer;
                }
                break;

        }
    }
}





       

