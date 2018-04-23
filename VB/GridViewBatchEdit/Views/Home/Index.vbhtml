        <script type="text/javascript">
        function Grid_BatchEditStartEditing(s, e) {
            var templateColumn = s.GetColumnByField("C1");
            if (!e.rowValues.hasOwnProperty(templateColumn.index))
                return;
            var cellInfo = e.rowValues[templateColumn.index];
            C1spinEdit.SetValue(cellInfo.value);
            if (e.focusedColumn === templateColumn)
                C1spinEdit.SetFocus();
        }
        function Grid_BatchEditEndEditing(s, e) {
            var templateColumn = s.GetColumnByField("C1");
            if (!e.rowValues.hasOwnProperty(templateColumn.index))
                return;
            var cellInfo = e.rowValues[templateColumn.index];
            cellInfo.value = C1spinEdit.GetValue();
            cellInfo.text = C1spinEdit.GetText();
            C1spinEdit.SetValue(null);
        }
        function Grid_BatchEditRowValidating(s, e) {
            var templateColumn = s.GetColumnByField("C1");
            var cellValidationInfo = e.validationInfo[templateColumn.index];
            if (!cellValidationInfo) return;
            var value = cellValidationInfo.value;
            if (!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) === "") {
                cellValidationInfo.isValid = false;
                cellValidationInfo.errorText = "C1 is required";
            }
        }

        var preventEndEditOnLostFocus = false;
        function C1spinEdit_KeyDown(s, e) {
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode === ASPx.Key.Esc) {
                var cellInfo = grid.batchEditApi.GetEditCellInfo();
                window.setTimeout(function () {
                    grid.SetFocusedCell(cellInfo.rowVisibleIndex, cellInfo.column.index);
                }, 0);
                s.GetInputElement().blur();
                return;
            }
            if (keyCode !== ASPx.Key.Tab && keyCode !== ASPx.Key.Enter) return;
            var moveActionName = e.htmlEvent.shiftKey ? "MoveFocusBackward" : "MoveFocusForward";
            if (grid.batchEditApi[moveActionName]()) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
                preventEndEditOnLostFocus = true;
            }
        }
        function C1spinEdit_LostFocus(s, e) {
            if (!preventEndEditOnLostFocus)
                grid.batchEditApi.EndEdit();
            preventEndEditOnLostFocus = false;
        }
    </script>

@Html.Action("GridViewPartial")