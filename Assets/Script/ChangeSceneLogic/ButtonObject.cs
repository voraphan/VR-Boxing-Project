using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonObject : MonoBehaviour
{
    [SerializeField] private Vector3 moveOffset = new Vector3(0, -0.1f, 0); 
    [SerializeField] private float moveTime = 0.1f; 

    private Vector3 _originalPosition;
    [SerializeField] private string sceneName;
    [SerializeField] private TMPro.TextMeshPro _textMeshPro;
    private void Start()
    {
        _originalPosition = transform.localPosition;
        _textMeshPro.text = sceneName;
    }


    public void PushButton()
    {
        StartCoroutine(MoveButton());
    }

    private IEnumerator MoveButton()
    {
        Vector3 targetPosition = _originalPosition + moveOffset;

        yield return MoveOverTime(targetPosition);

        yield return MoveOverTime(_originalPosition);

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator MoveOverTime(Vector3 target)
    {
        Vector3 start = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            transform.localPosition = Vector3.Lerp(start, target, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        PushButton();
    }
}
