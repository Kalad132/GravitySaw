using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class Sparks : MonoBehaviour
{
    [SerializeField] private List<PhysicsMaterial2D> _materials;
    [SerializeField] private ParticleSystem _template;

    void OnCollisionEnter2D(Collision2D collision)
    {
        PhysicsMaterial2D material = null;
        if (collision.gameObject.TryGetComponent(out Rigidbody2D body))
            material = body.sharedMaterial;
        else if (collision.gameObject.TryGetComponent(out BoxCollider2D collider))
            material = collider.sharedMaterial;
        else if (collision.gameObject.TryGetComponent(out CircleCollider2D circle))
            material = circle.sharedMaterial;
        else if (collision.gameObject.TryGetComponent(out TilemapCollider2D map))
            material = map.sharedMaterial;
        if (_materials.Contains(material))
            Instantiate(_template, collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).point, transform.position));
    }

}
