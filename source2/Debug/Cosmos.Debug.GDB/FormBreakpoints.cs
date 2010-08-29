﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cosmos.Debug.GDB {
    public partial class FormBreakpoints : Form {
        protected Dictionary<int, BreakpointUC> mBreakpoints = new Dictionary<int, BreakpointUC>();

        public FormBreakpoints() {
            InitializeComponent();
        }

        public void LoadSession() {
            foreach (SettingsDS.BreakpointRow xBP in Settings.DS.Breakpoint.Rows) {
                AddBreakpoint(xBP.Label);
            }
        }

        public bool AddBreakpoint(string aLabel) {
            string s = aLabel.Trim();
            if (s.Length > 0) {
                Global.GDB.SendCmd("break " + s);
            }
            return false;
        }

        public void OnDelete(GDB.Response aResponse) {
            var xSplit = aResponse.Reply.Split(' ');
            int xID = int.Parse(xSplit[1]);
            var xUC = mBreakpoints[xID];

            // Delete UC
            Controls.Remove(xUC);
            xUC.Dispose();
        }

        public void SaveSettings() {
            foreach (var xUC in mBreakpoints.Values) {
                string xLabel = xUC.lablName.Text;
                // We dont add address types, as most of them change between compiles.
                if (!xLabel.StartsWith("*")) {
                    var xBP = Settings.DS.Breakpoint.NewBreakpointRow();
                    xBP.Label = xLabel;
                    Settings.DS.Breakpoint.AddBreakpointRow(xBP);
                }
            }
        }

        public void OnBreak(GDB.Response aResponse) {
            var xCmdParts = aResponse.Command.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string xLabel = xCmdParts[1];

            var xSplit = aResponse.Text[0].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (xSplit[0].ToLower() == "breakpoint") {
                // http://stackoverflow.com/questions/27674/dynamic-top-down-list-of-controls-in-windowsforms-and-c
                var xUC = new BreakpointUC();
                mBreakpoints.Add(int.Parse(xSplit[1]), xUC);

                xUC.Dock = DockStyle.Top;
                xUC.cboxEnabled.Checked = true;
                xUC.lablNum.Text = xSplit[1];
                xUC.lablName.Text = xLabel;
                xUC.Delete += new Action<int>(xUC_Delete);
                panl.Controls.Add(xUC);
            }
        }

        void xUC_Delete(int aID) {
            Global.GDB.SendCmd("delete " + aID);
        }

        private void butnBreakpointAdd_Click(object sender, EventArgs e) {
            string xLabel = textBreakpoint.Text.Trim();
            AddBreakpoint(xLabel);
            textBreakpoint.Clear();
        }

        private void textBreakpoint_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') {
                butnBreakpointAdd.PerformClick();
            }
        }

        private void FormBreakpoints_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            Hide();
        }

    }
}
