using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEditor;

namespace Tiled2Unity
{
    interface ICustomTiledImporter
    {
        // A game object within the prefab has some custom properites assigned through Tiled that are not consumed by Tiled2Unity
        // This callback gives customized importers a chance to react to such properites.
        void HandleCustomProperties(GameObject gameObject, IDictionary<string, string> customProperties);

        // Called just before the prefab is saved to the asset database
        // A last chance opporunity to modify it through script
        void CustomizePrefab(GameObject prefab);
    }
}

// Examples

[Tiled2Unity.CustomTiledImporter]
class CustomImporterAddComponent : Tiled2Unity.ICustomTiledImporter
{
    public void HandleCustomProperties(UnityEngine.GameObject gameObject,
        IDictionary<string, string> props)
    {
		Debug.Log (gameObject);
        // Simply add a component to our GameObject
        if (props.ContainsKey("AddComp"))
        {	
			// Load the prefab assest and Instantiate it
			string prefabPath = "Assets/Prefabs/" + props["AddComp"] + ".prefab";
			UnityEngine.Object spawn =
				AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
			if (spawn != null)
			{
				GameObject spawnInstance =
					(GameObject)GameObject.Instantiate(spawn);
				spawnInstance.name = spawn.name;
				
				// Use the position of the game object we're attached to
				spawnInstance.transform.parent = gameObject.transform;
				spawnInstance.transform.localPosition = new Vector3(16, 16, 0);
			}
		}
	}


    public void CustomizePrefab(GameObject prefab)
    {
        // Do nothing
    }
}
