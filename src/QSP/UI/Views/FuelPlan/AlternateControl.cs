﻿using QSP.UI.Models;
using QSP.UI.Presenters.FuelPlan;
using QSP.UI.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QSP.UI.Views.FuelPlan
{
    public partial class AlternateControl : UserControl, IAlternateView
    {
        private AlternatePresenter presenter;

        public IEnumerable<IAlternateRowView> Views =>
            // Skip the last row, which is a layout panel with buttons.
            Enumerable.Range(0, layoutPanel.RowCount - 1)  
                      .Select(i => layoutPanel.GetControlFromPosition(0, i))
                      .Cast<IAlternateRowView>();

        public AlternateControl()
        {
            InitializeComponent();
        }

        public void Init(AlternatePresenter presenter)
        {
            this.presenter = presenter;

            removeAltnBtn.Enabled = false;
            SetBtnColorStyles(ButtonColorStyle.Default);
            presenter.AddRow();
        }

        /// <summary>
        /// Add a row with its presenter and initialize view of the row. 
        /// Returns the view of the created row.
        /// </summary>
        public IAlternateRowView AddRow()
        {
            SuspendLayout();

            var v = new AlternateRowControl();
            var p = presenter.GetRowPresenter(v);
            v.Init(p, ParentForm);
            v.AddToLayoutPanel(layoutPanel);

            ResumeLayout();
            return v;
        }

        private void SetBtnColorStyles(ControlDisableStyleController.ColorStyle style)
        {
            var removeBtnStyle = new ControlDisableStyleController(removeAltnBtn, style);
            removeBtnStyle.Activate();
        }

        private void addAltnBtn_Click(object sender, EventArgs e)
        {
            presenter.AddRow();
        }

        /// <exception cref="InvalidOperationException"></exception>
        private void removeAltnBtn_Click(object sender, EventArgs e)
        {
            presenter.RemoveLastRow();
            layoutPanel.RowCount--;
            removeAltnBtn.Enabled = presenter.RowCount > 1;
        }
    }
}
