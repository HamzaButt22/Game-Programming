using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    float fadeDuration = 1f;      // How long until alpha reaches 0
    float fadeTimer = 0f;
    public float floatSpeed = 30f;   // how fast text moves upward
    bool isFading = false;

    void Start()
    {
        // Start invisible
        Color c = textMesh.color;
        c.a = 0;
        textMesh.color = c;
    }

    public void ShowDamage(int dmg)
    {
        textMesh.text = dmg.ToString();

        // Reset fade cycle
        fadeTimer = 0f;
        isFading = true;

        // Set visible
        Color c = textMesh.color;
        c.a = 1f;
        textMesh.color = c;
    }

    void Update()
    {
        if (!isFading) return;

        // Move text upward slightly every frame
        transform.localPosition += new Vector3(0, floatSpeed * Time.deltaTime, 0);

        fadeTimer += Time.deltaTime;

        float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);

        Color c = textMesh.color;
        c.a = alpha;
        textMesh.color = c;

        if (alpha <= 0f)
        {
            isFading = false;

            // Reset position so next damage starts from same place
            transform.localPosition = Vector3.zero;
        }
    }
}