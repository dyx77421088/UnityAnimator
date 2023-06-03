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
        //    .Append(text.DOText("我是哈哈哈", 2))
        //    .Append(text.DOText("你干嘛哎呦", 3).SetRelative(true));

        tw = text.DOText("我是你爸爸我是你爸爸我是你爸爸我是你爸爸我是你爸爸我是你爸爸", 3).OnComplete(()=>Debug.Log("text成功播放了"))
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
