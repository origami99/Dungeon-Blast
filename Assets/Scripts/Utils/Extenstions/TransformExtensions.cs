using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils.Extenstions
{
    public static class TransformExtensions
    {
        public static List<GameObject> FindObjectsWithTag(this Transform parent, string tag)
        {
            List<GameObject> taggedGameObjects = new List<GameObject>();

            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);

                if (child.tag == tag)
                {
                    taggedGameObjects.Add(child.gameObject);
                }
                if (child.childCount > 0)
                {
                    taggedGameObjects.AddRange(FindObjectsWithTag(child, tag));
                }
            }

            return taggedGameObjects;
        }
    }
}
