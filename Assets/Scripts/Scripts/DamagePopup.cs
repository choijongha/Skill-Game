using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    // Create Damage Popup
    public static DamagePopup Create(Vector3 position, float damageAmount, bool isCritical)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Transform damagePopupTransform = Instantiate(gameManager.damageText, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCritical);

        return damagePopup;
    }
    private static int sortingOrder;
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textMeshColor;
    private float MAX_DISSAPPEARTIMER = 1f;
    private Vector3 moveVector;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    private void Setup(float damageAmount, bool isCritical)
    {
        textMesh.SetText(damageAmount.ToString());
        if (!isCritical)
        {
            textMesh.fontSize = 3;
            textMeshColor = Color.yellow;
        }
        else
        {
            textMesh.fontSize = 5;
            textMeshColor.r = 1;
            textMeshColor.g = 0;
            textMeshColor.b = 0;
            textMeshColor.a = 1;
        }
        textMesh.color = textMeshColor;
        disappearTimer = MAX_DISSAPPEARTIMER;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        moveVector = new Vector3(.7f, 1);
    }
    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;
        
        if(disappearTimer > MAX_DISSAPPEARTIMER * 0.5)
        {
            // First half of the popup lifetime
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            // Second half of the popup lifetime
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textMeshColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textMeshColor;
            if(textMeshColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
