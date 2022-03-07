using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeAppearance : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField]
    private TextMeshProUGUI _progressName;

    [SerializeField]
    private Image _progressBar;

    [SerializeField]
    private GameObject _content;

    [SerializeField]
    private GameObject _currentModel;

    private string _currentName = string.Empty;

    public event ChangedModel OnChangedModel;
    public delegate void ChangedModel(Transform model);


    public void UpdateProgressBar(float weight)
    {
        var fillAmount = weight / 100;

        _progressBar.fillAmount = fillAmount;
    }


    public void SetStartParameters(AppearanceParam param)
    {
        SetParameters(param);
    }


    public void SetAppearance(AppearanceParam param)
    {
        if (_currentName.Equals(param.Name)) return;

        SetParameters(param);
    }


    private void ChangeProgressName(string name)
    {
        _progressName.text = name;
        _currentName = name;
    }


    private void ChangeProgressBarColor(Color barColor)
    {
        _progressBar.color = barColor;
    }


    public void ActivateOutline()
    {
        var outline = _currentModel.transform.parent.gameObject.GetComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineVisible;
    }


    public void TurnOnContent()
    {
        _content.SetActive(true);
    }


    public void TurnOffContent()
    {
        _content.SetActive(false);
    }


    private void ChangeModel(GameObject model)
    {
        if (model != null)
        {
            var previousModel = _currentModel;

            _currentModel = model;

            OnChangedModel?.Invoke(model.transform);

            StartCoroutine(AddOutline(model.transform));

            model.SetActive(true);

            if (previousModel != null)
                previousModel.SetActive(false);
        }
    }


    private IEnumerator AddOutline(Transform model)
    {
        var outline = model.parent.gameObject.GetComponent<Outline>();

        Destroy(outline);

        yield return new WaitForEndOfFrame();

        var width = 2.5f;
        var color = new Color(40, 40, 40, 255); // almost black

        var outlineNew = model.parent.gameObject.AddComponent<Outline>();

        outlineNew.OutlineMode = Outline.Mode.OutlineVisible;
        outlineNew.OutlineWidth = width;
        outlineNew.OutlineColor = color;
    }


    private void SetParameters(AppearanceParam param)
    {
        ChangeProgressName(param.Name);
        ChangeProgressBarColor(param.BarColor);
        ChangeModel(param.Model);
    }
}