using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefab : MonoBehaviour
{
    public Item item;
    public Material[] materials;

    private MeshRenderer mr;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        SelectRandomMaterial();
    }

    private void SelectRandomMaterial()
    {
        mr.material = materials[Random.Range(0, materials.Length)];
    }
}
