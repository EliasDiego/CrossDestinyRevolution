using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    Transform[] _Transforms;
    MeshRenderer _MeshRenderer;
    Material _Material;

    void Start()
    {
        _MeshRenderer = GetComponent<MeshRenderer>();

        _Material = _MeshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        _Material.SetVectorArray("_Positions", _Transforms.Select(t => new Vector4(t.position.x, t.position.y, t.position.z)).ToArray());
    }
}
