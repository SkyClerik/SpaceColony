using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay.Inventory
{
    public class ItemVisual : VisualElement
    {
        private PlayerInventoriesContainer _playerInventoriesContainer;
        private PlayerInventory _ownerInventory;
        private StoredItem _ownerStored;
        private Vector2 _originalPosition;
        private (int, int) _originalScale;
        private float _originalRotate;
        private bool _isDragging;
        private (bool canPlace, Vector2 position) _placementResults;
        private VisualElement _intermediate;
        private VisualElement _icon;

        private const string _intermediateName = "Intermediate";
        private const string _iconName = "Icon";
        private const string _visualIconContainerName = "visual-icon-container";
        private const string _visualIconName = "visual-icon";

        public ItemVisual(PlayerInventory ownerInventory, StoredItem ownerStored)
        {
            _ownerInventory = ownerInventory;
            _ownerStored = ownerStored;
            _playerInventoriesContainer = PlayerInventoriesContainer.Instance;

            name = _ownerStored.Details.FriendlyName;
            style.visibility = Visibility.Hidden;
            AddToClassList(_visualIconContainerName);
            SetSize();

            _intermediate = new VisualElement
            {
                style =
                {
                    width = _ownerStored.Details.SlotDimension.DefaultWidth * _ownerInventory.SlotDimension.Width,
                    height = _ownerStored.Details.SlotDimension.DefaultHeight * _ownerInventory.SlotDimension.Height,
                    rotate = new Rotate(_ownerStored.Details.SlotDimension.DefaulAngle),
                    paddingTop = 5,
                    paddingBottom = 5,
                    paddingLeft = 5,
                    paddingRight = 5,
                },
                name = _intermediateName
            };
            _ownerStored.Details.SlotDimension.CurrentAngle = _ownerStored.Details.SlotDimension.DefaulAngle;

            _icon = new VisualElement
            {
                style =
                {
                    backgroundImage = _ownerStored.Details.Icon.texture,
                    width = _ownerStored.Details.SlotDimension.DefaultWidth * _ownerInventory.SlotDimension.Width,
                    height = _ownerStored.Details.SlotDimension.DefaultHeight * _ownerInventory.SlotDimension.Height,
                },
                name = _iconName
            };

            _icon.AddToClassList(_visualIconName);

            Add(_intermediate);
            _intermediate.Add(_icon);

            RegisterCallback<MouseUpEvent>(OnMouseUp);
            RegisterCallback<MouseDownEvent>(OnMouseDown);
            RegisterCallback<MouseMoveEvent>(OnMouseMove);
        }

        ~ItemVisual()
        {
            UnregisterCallback<MouseUpEvent>(OnMouseUp);
            UnregisterCallback<MouseDownEvent>(OnMouseDown);
            UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        }

        public void SetPosition(Vector2 pos)
        {
            style.left = pos.x;
            style.top = pos.y;
        }

        public void Rotate()
        {
            SwapOwnerSize();
            SetSize();
            RotateIconRight();
        }

        private void SwapOwnerSize()
        {
            var details = _ownerStored.Details;
            (details.SlotDimension.CurrentWidth, details.SlotDimension.CurrentHeight) = (details.SlotDimension.CurrentHeight, details.SlotDimension.CurrentWidth);
        }

        public void SetSize()
        {
            style.height = _ownerStored.Details.SlotDimension.CurrentHeight * _ownerInventory.SlotDimension.Height;
            style.width = _ownerStored.Details.SlotDimension.CurrentWidth * _ownerInventory.SlotDimension.Width;
        }

        public void RotateIconRight()
        {
            var angle = _ownerStored.Details.SlotDimension.CurrentAngle + 90;

            if (angle >= 360)
                angle = 0;

            RotateIntermediate(angle);
            SaveCurrentAngle(angle);
        }

        private void RotateIntermediate(float angle) => _intermediate.style.rotate = new Rotate(angle);

        private void SaveCurrentAngle(float angle) => _ownerStored.Details.SlotDimension.CurrentAngle = angle;

        private void RestoreSizeAndRotate()
        {
            _ownerStored.Details.SlotDimension.CurrentWidth = _originalScale.Item1;
            _ownerStored.Details.SlotDimension.CurrentHeight = _originalScale.Item2;
            SetSize();
            RotateIntermediate(_originalRotate);
            SaveCurrentAngle(_originalRotate);
        }

        private void OnMouseUp(MouseUpEvent mouseEvent)
        {
            if (mouseEvent.button == 0)
            {
                if (!_isDragging)
                    return;

                _isDragging = false;
                style.opacity = 1f;
                PlayerInventory.CurrentDraggedItem = null;
                _ownerInventory.Document.sortingOrder = 0;

                foreach (var inventory in _playerInventoriesContainer.Inventories)
                {
                    _placementResults = inventory.ShowPlacementTarget(this, out StoredItem overlapItem);

                    if (_placementResults.canPlace)
                    {
                        inventory.StoredItems.Add(_ownerStored);
                        inventory.AddItemToInventoryGrid(this);
                        _ownerInventory = inventory;
                        SetPosition(_placementResults.position - parent.worldBound.position);

                        if (overlapItem != null)
                            overlapItem.RootVisual.PickUp();

                        return;
                    }
                }

                DropBack();
            }
        }

        private void DropBack()
        {
            _placementResults = _ownerInventory.ShowPlacementTarget(this, out StoredItem overlapItem);

            if (_placementResults.canPlace && overlapItem == null)
            {
                _ownerInventory.StoredItems.Add(_ownerStored);
                _ownerInventory.AddItemToInventoryGrid(this);
                SetPosition(_originalPosition);
                RestoreSizeAndRotate();
                return;
            }
            else
            {
                PickUp();
            }
        }

        private void OnMouseDown(MouseDownEvent mouseEvent)
        {
            if (mouseEvent.button == 0)
            {
                if (PlayerInventory.CurrentDraggedItem != _ownerStored)
                    PickUp();
            }
        }

        public void PickUp()
        {
            _isDragging = true;
            style.left = float.MinValue;
            style.opacity = 0.7f;

            _originalPosition = worldBound.position - parent.worldBound.position;
            _originalRotate = _ownerStored.Details.SlotDimension.CurrentAngle;
            _originalScale = (_ownerStored.Details.SlotDimension.CurrentWidth, _ownerStored.Details.SlotDimension.CurrentHeight);

            _ownerInventory.Root.Add(this);
            _ownerInventory.StoredItems.Remove(_ownerStored);
            _ownerInventory.Document.sortingOrder = 1;

            PlayerInventory.CurrentDraggedItem = _ownerStored;
            PlayerInventory.CurrentDraggedItem.OwnerInventory = _ownerInventory;
        }

        private void OnMouseMove(MouseMoveEvent evt)
        {
            if (!_isDragging)
                return;

            foreach (var inventory in _playerInventoriesContainer.Inventories)
                inventory.ShowPlacementTarget(this, out _);
        }
    }
}