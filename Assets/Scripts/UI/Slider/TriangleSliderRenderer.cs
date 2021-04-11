using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TriangleSliderRenderer : MonoBehaviour {

  [SerializeField]
  private int numberOfColumns = 0;

  [SerializeField]
  private int currentValue = 8;

  [SerializeField]
  private RectTransform rectTransform;

  [SerializeField]
  private HorizontalLayoutGroup layoutGroup;

  [SerializeField]
  private RectTransform selectedColumn;

  [SerializeField]
  private Color activeColumnColor = Color.white;

  [SerializeField]
  private Color inactiveColumnColor = Color.gray;

  private int cachedNumberOfColumns;
  private RectTransform[] cachedColumns;

  public int CurrentValue {
    get => currentValue;
    set {
      currentValue = Mathf.Clamp(value, 1, numberOfColumns);
      UpdateColumnsColors();
      UpdateSelectedColumn();
    }
  }

  private RectTransform[] CachedColumns {
    get {
      if (cachedColumns == null) {
        cachedColumns = GetComponentsInChildren<Image>().Select(i => i.GetComponent<RectTransform>()).ToArray();
      }
      return cachedColumns;
    }
  }

  private void Update() {
    UpdateColumns();
  }

  private void OnEnable() {
    cachedColumns = GetComponentsInChildren<Image>().Select(i => i.GetComponent<RectTransform>()).ToArray();
    UpdateColumnsColors();
    UpdateSelectedColumn();
  }

  internal void HideColumnSelector() {
    selectedColumn.gameObject.SetActive(false);
  }

  internal float GetPercentageValue() {
    return currentValue / (float)numberOfColumns;
  }

  internal void SetPercentage(float sliderPercentage) {
    float newValue = sliderPercentage * numberOfColumns;
    CurrentValue = (int)newValue;
  }

  internal void ShowColumnSelector() {
    selectedColumn.gameObject.SetActive(true);
  }

  private void UpdateColumnsColors() {
    var columnImages = CachedColumns.Select(c => c.GetComponent<Image>()).ToArray();
    for (int i = 0; i < columnImages.Length; i++) {
      var columnImage = columnImages[i];
      if (i +  1 > currentValue) {
        columnImage.color = inactiveColumnColor;
      } else {
        columnImage.color = activeColumnColor;
      }
    }
  }

  private void UpdateSelectedColumn() {
    var topSelectedColumn = CachedColumns[currentValue - 1];
    selectedColumn.transform.position = new Vector3(topSelectedColumn.position.x, selectedColumn.transform.position.y);
  }

  #region EDITOR

  private void UpdateColumns() {
    if (rectTransform == null || layoutGroup == null) {
      return;
    }
    RectTransform[] columns = GetColumnRectTransforms();
    float sliderHeight = rectTransform.rect.height;
    float sliderWidth = rectTransform.rect.width;
    float spacing = layoutGroup.spacing;
    float columnWidth = sliderWidth / numberOfColumns - (Mathf.Max(numberOfColumns - 1, 0) * spacing);
    for (int i = 0; i < columns.Length; i++) {
      float columnHeight = sliderHeight * ((i + 1) / (float)numberOfColumns);
      columns[i].sizeDelta = new Vector2(columnWidth, columnHeight);
    }
    if (selectedColumn) {
      selectedColumn.sizeDelta = new Vector2(columnWidth, selectedColumn.sizeDelta.y);
    }
    cachedNumberOfColumns = numberOfColumns;
    cachedColumns = columns;
  }

  private RectTransform[] GetColumnRectTransforms() {
    if (numberOfColumns == cachedNumberOfColumns && cachedColumns != null) {
      return cachedColumns;
    }
    IEnumerable<RectTransform> columnsEnumerable = GetComponentsInChildren<Image>().Select(i => i.GetComponent<RectTransform>());
    int columnsLength = columnsEnumerable.Count();
    if (columnsLength > numberOfColumns) {
      columnsEnumerable = DeleteColumnsToFit(columnsEnumerable, numberOfColumns);
    } else if (columnsLength < numberOfColumns) {
      columnsEnumerable = AddColumnsToFit(columnsEnumerable, columnsLength, numberOfColumns);
    }
    return columnsEnumerable.ToArray();
  }

  private IEnumerable<RectTransform> DeleteColumnsToFit(IEnumerable<RectTransform> columns, int numberOfColumns) {
    var columnsToDelete = columns.Skip(numberOfColumns);
    foreach (var column in columnsToDelete) {
      DestroyImmediate(column.gameObject);
    }
    return columns.Take(numberOfColumns);
  }

  private IEnumerable<RectTransform> AddColumnsToFit(IEnumerable<RectTransform> columns, int currentNumberOfColumns, int numberOfColumns) {
    int numberOfColumnsToAdd = numberOfColumns - currentNumberOfColumns;
    return columns.Concat(CreateColumns(currentNumberOfColumns, numberOfColumnsToAdd));
  }

  private IEnumerable<RectTransform> CreateColumns(int currentNumberOfColumns, int numberOfColumnsToAdd) {
    RectTransform[] columns = new RectTransform[numberOfColumnsToAdd];
    for (int i = 0; i < numberOfColumnsToAdd; i++) {
      GameObject newColumn = new GameObject($"Column {currentNumberOfColumns + i}", typeof(Image));
      columns[i] = newColumn.GetComponent<RectTransform>();
      newColumn.transform.SetParent(transform);
    }

    return columns;
  }

  #endregion
}
