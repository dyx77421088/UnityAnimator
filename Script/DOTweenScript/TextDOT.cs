using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class TextDOT : MonoBehaviour
{
    private Text text;
    private Tweener tw;
    async void Start()
    {
        text = GetComponent<Text>();
        //DOTween.Sequence()
        //    .Append(text.DOText("���ǹ�����", 2))
        //    .Append(text.DOText("����ﰥ��", 3).SetRelative(true));

        tw = text.DOText("������ְ�������ְ�������ְ�������ְ�������ְ�������ְ�", 3).OnComplete(()=>Debug.Log("text�ɹ�������"))
            .SetLoops(3, LoopType.Yoyo);
        //await Task.Delay(6000);
        //text.DOPlayBackwards();
        //await Task.Delay(3000);
        //text.DOPlayForward();
        //transform.DOPath();
        StartCoroutine(T() );   
        
    }

    private IEnumerator T()
    {
        yield return tw.WaitForPosition(4);
        Debug.Log("11111111111");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(tw.ElapsedPercentage());
    }
}
