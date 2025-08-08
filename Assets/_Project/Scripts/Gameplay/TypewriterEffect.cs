using UnityEngine;
using TMPro;
using DG.Tweening;

public class TypewriterAndFade : MonoBehaviour
{
    [TextArea(5, 10)]
    [SerializeField] private string _fullText = "Первоначальная модель головы подвергается серии ударов. Каждый удар вызывает видимые эффекты: появляются трещины и раны в виде декалей, а сама геометрия сетки (меша) деформируется в точке попадания, создавая вмятины. Повреждения накапливаются, приводя к значительному и реалистичному разрушению модели.";

    [Header("Настройки анимации")]
    [Tooltip("Длительность анимации печатания текста в секундах.")]
    [SerializeField] private float _typingDuration = 10f;

    [Tooltip("Пауза после окончания печатания перед исчезновением.")]
    [SerializeField] private float _delayBeforeFadeOut = 1.5f;

    [Tooltip("Длительность плавного исчезновения текста.")]
    [SerializeField] private float _fadeOutDuration = 2f;

    private TextMeshProUGUI _textMeshPro;

    void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        if (_textMeshPro == null)
        {
            Debug.LogError("На объекте отсутствует компонент TextMeshProUGUI!");
            return;
        }
    }

    void Start()
    {
        AnimateText();
    }

    public void AnimateText()
    {
        // В начале убедимся, что текст полностью непрозрачен и пуст
        _textMeshPro.color = new Color(_textMeshPro.color.r, _textMeshPro.color.g, _textMeshPro.color.b, 1f);
        _textMeshPro.text = "";

        // Создаем последовательность, которая будет управлять всеми нашими анимациями
        Sequence textSequence = DOTween.Sequence();

        // Печатание текста 
        int displayedCharCount = 0;
        Tween typingTween = DOTween.To(() => displayedCharCount, x => displayedCharCount = x, _fullText.Length, _typingDuration)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                _textMeshPro.text = _fullText.Substring(0, displayedCharCount);
            });

        // Добавляем анимацию печатания в нашу последовательность
        textSequence.Append(typingTween);
        
        // Добавляем в последовательность интервал ожидания
        textSequence.AppendInterval(_delayBeforeFadeOut);

        // Плавное исчезновение
        textSequence.Append(_textMeshPro.DOFade(0f, _fadeOutDuration).SetEase(Ease.InQuad));

        // Запускаем всю последовательность
        textSequence.Play();
    }
}