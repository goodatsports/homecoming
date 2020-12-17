using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using TMPro;

public class TreeController : Interactable
{
    public int Health;
    public Tile Tile;
    public MapController Map;
    public PlayerController Player;
    public GameObject DamageTextPrefab;
    public BoxCollider2D Collider;

    void OnEnable() {
        Name = "Tree";
        Verb = "Chop down";

        Health = 3;
        Map = GameObject.Find("Map").GetComponentInChildren(typeof(MapController)) as MapController;
        Player = GameObject.Find("Player").GetComponentInChildren(typeof(PlayerController)) as PlayerController;
        //DamageUI = Instantiate(TextPrefab, transform).GetComponent<TextMeshPro>();
        //DamageUI.text = Health.ToString();
    }

    public override void Interact() {
        if (Player.HasAxe()) {
            Hit();
        }
    }

    public void Hit() {
        Health--;
        Instantiate(DamageTextPrefab, transform);
        if (Health == 0) {
            StartCoroutine(Dead());
        }
    }

    public IEnumerator Dead() {
        Vector3Int address = Map.WorldToCell(transform.position);
        Map.ChopTree(address);
        
        // Delete collider immediately, wait for damage animation to finish then destroy self
        Destroy(Collider);
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update() {

    }
}