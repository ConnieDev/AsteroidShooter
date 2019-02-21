using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour {
	private static int defaultPoolSize = 20;

	public static bool willGrow = true;

	public static Dictionary<string, List<GameObject>> objectPools = new Dictionary<string, List<GameObject>>();

	public static GameObject GetPooledObject(GameObject prefab){
		if (!objectPools.ContainsKey (prefab.name)) {
			CreateObjectPool (prefab, defaultPoolSize);
			return GetPooledObject (prefab);
		}

		var pool = objectPools [prefab.name];

		for (int i = 0; i < pool.Count; i++) {
			if (!pool [i].activeInHierarchy) {
				return pool [i];
			}
		}
		if (willGrow) {
			GameObject shotInstance = Instantiate(prefab) as GameObject;
			pool.Add (shotInstance);
			return shotInstance;
		}

		return null;
	}

	public static void CreateObjectPool(GameObject prefab, int count){

		List<GameObject> objects = new List<GameObject> ();

		for(int i = 0; i < count; i++){
			GameObject instance = Instantiate<GameObject> (prefab);

			objects.Add (instance);

			instance.SetActive (false);
		}

		objectPools.Add (prefab.name, objects);
	}

}
