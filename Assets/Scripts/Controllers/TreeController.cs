using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using TMPro;

public class TreeController : MonoBehaviour
{
    public int Health;
    public Tile Tile;
    public TextMeshPro TextPrefab;
    public TextMeshPro DamageUI;

    void OnEnable() {
        Health = 3;
        TextPrefab = GameObject.Find("Damage Text").GetComponent<TextMeshPro>();
        DamageUI = Instantiate(TextPrefab, transform).GetComponent<TextMeshPro>();
        DamageUI.text = Health.ToString();
    }

    public void Hit() {
        Health--;
        DamageUI.text = Health.ToString();
        AnimateDamage();
    }

    private IEnumerator AnimateDamage2() {
        return null;
    }

    private void AnimateDamage() {
        float StartTime = Time.time;
        float CurrentTime = Time.time;
        float StartPos = DamageUI.transform.position.y;
        float EndPos = StartPos + 1f;
        while (CurrentTime < StartTime + 10) {
            print("animate");
            float yPos = Mathf.Lerp(StartPos, EndPos, (StartTime + 10) - CurrentTime / StartTime);
            DamageUI.transform.position = new Vector3(DamageUI.transform.position.x, yPos, DamageUI.transform.position.z);
            CurrentTime += Time.deltaTime;
        }
    }

    public bool Dead() {
        if (Health == 0) return true;
        else return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroy() {
        Destroy(gameObject);
    }
}
