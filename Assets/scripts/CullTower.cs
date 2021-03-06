﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CullTower : MonoBehaviour
{

    public Material blankMaterial;

    private GameObject playerCollider;
    //private GameObject towerExterior;
    //private GameObject island_rock;
    private string[] objectsToHide = { "towerExterior", "island_rock" };
    private System.Collections.Generic.List<GameObject> gameObjectsToHide;
    //private System.Collections.Generic.List<Material> gameObjectMaterials;

    // Use this for initialization
    void Start()
    {
        gameObjectsToHide = new System.Collections.Generic.List<GameObject>();

        foreach (var objectName in objectsToHide)
        {
            gameObjectsToHide.Add(GameObject.Find(objectName));
        }
        //towerExterior = GameObject.Find("towerExterior");
        //island_rock = GameObject.Find("island_rock");
        playerCollider = GameObject.Find("playerCollider");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void removeAllTextures()
    {
        var sceneObjects = new List<GameObject>(Resources.FindObjectsOfTypeAll<GameObject>());
        //sceneObjects.Add(GameObject.Find("woodDesk"));
        //string output = "scene materials: ";
        foreach (var item in sceneObjects)
        {
            var renderer = item.GetComponent<MeshRenderer>();
            if (renderer)
            {
                var materials = renderer.sharedMaterials;
                if ( materials.Length > 1 )
                {
                    for ( int i = 0; i < materials.Length; ++i )
                    {
                        materials[i] = blankMaterial;
                    }
                    renderer.materials = materials;
                }
                else
                {
                    renderer.material = blankMaterial;
                }
            }
        }
        //Debug.Log(output);
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    public void OnTriggerEnter(Collider other)
    {
        triggerRenderer(other, false);
        //removeAllTextures();
    }

    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    public void OnTriggerExit(Collider other)
    {
        triggerRenderer(other, true);
    }

    private void triggerRenderer(Collider other, bool enabled)
    {
        if (playerCollider && (other == playerCollider.GetComponent<Collider>()))
        {
            Debug.Log(other.gameObject + "entered tower");
            foreach (GameObject gameObject in gameObjectsToHide)
            {
                gameObject.GetComponent<Renderer>().enabled = enabled;
            }
            //Debug.Log(towerExterior.GetComponent<Mesh>().GetTriangles(0));
        }
    }
}
