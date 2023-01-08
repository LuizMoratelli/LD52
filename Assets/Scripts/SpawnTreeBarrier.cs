using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTreeBarrier : MonoBehaviour
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public GameObject TreePrefab;
    public int QuantityOfTrees;
    public Vector2 OffsetBetweenTrees;
    public Vector3 ApplyRotation;

    [ContextMenu("Generate Trees")]
    void GenerateTrees()
    {
        for (int i = 0; i < QuantityOfTrees; i++)
        {
            var randomY = Random.Range(StartPosition.y, EndPosition.y);
            var randomX = Random.Range(StartPosition.x, EndPosition.x);

            var tree = Instantiate(TreePrefab, transform);
            tree.transform.position = new Vector3(randomX, randomY);
            tree.transform.rotation = Quaternion.Euler(tree.transform.rotation.eulerAngles + ApplyRotation);
            tree.GetComponent<SpriteRenderer>().sortingOrder = (int) Mathf.Round(-randomY);

        }
    }

    [ContextMenu("Clean Trees")]
    void CleanTrees()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
