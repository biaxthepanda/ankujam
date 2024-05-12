using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ExpressionManager : MonoBehaviour
{
    #region Singleton
    public static ExpressionManager Instance { get; private set; }



    private void Awake()
    {
        // Bir örnek varsa ve ben deðilse, yoket.

        if (Instance != null && Instance != this)
        {
            return;
        }
        Instance = this;
    }

    #endregion

    public TextMeshProUGUI ExpressionText;
    public RectTransform ExpressionTextRect;

    private void Start()
    {
        
    }

    public void CreateExpression(string expressionString,Color expressionColor,float expressionDuration,float fontSize = 200) 
    {
        ExpressionText.text = expressionString;
        ExpressionText.color = expressionColor;
        ExpressionText.fontSize = fontSize;
        ExpressionTextRect.DOAnchorPos(new Vector2(0,-150),expressionDuration/2).SetEase(Ease.OutElastic).OnComplete(()=> ExpressionTextRect.DOAnchorPos(new Vector2(0,450),expressionDuration/4));
    
    }
}
