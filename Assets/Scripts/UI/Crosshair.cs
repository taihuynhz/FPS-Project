using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace FPSFramework
{
    [AddComponentMenu("UI/Crosshair")]
    public class Crosshair : MonoBehaviour
    {
        public float deltaSizeMultipler = 1;
        public float smoothness = 5;
        public float fadeSpeed = 10;
        public RectTransform holder;
        public CanvasGroup lines;
        public CanvasGroup dot;

        public FloatingRect floatingRect { get; private set; }

        private void Start()
        {
            floatingRect = transform.GetComponent<FloatingRect>();
        }

        private void Update()
        {
            ReturnSize();
        }

        public void Show()
        {
            lines.alpha = Mathf.Lerp(lines.alpha, 1, Time.deltaTime * fadeSpeed);
            dot.alpha = Mathf.Lerp(dot.alpha, 1, Time.deltaTime * fadeSpeed);
        }

        public void HideAll()
        {
            lines.alpha = Mathf.Lerp(lines.alpha, 0, Time.deltaTime * fadeSpeed * 2);
            dot.alpha = Mathf.Lerp(dot.alpha, 0, Time.deltaTime * fadeSpeed * 2);

            holder.sizeDelta = Vector2.Lerp(holder.sizeDelta, Vector2.one * 0, Time.deltaTime * smoothness / 2);
        }

        public void HideLines()
        {
            lines.alpha = Mathf.Lerp(lines.alpha, 0, Time.deltaTime * fadeSpeed);
        }

        public void UpdateSize(float currentSpray)
        {
            holder.sizeDelta = Vector2.one * currentSpray * deltaSizeMultipler;
        }

        public void ReturnSize()
        {
            holder.sizeDelta = Vector2.Lerp(holder.sizeDelta, new Vector2(250, 250), Time.deltaTime * smoothness / 2);
        }
    }
}