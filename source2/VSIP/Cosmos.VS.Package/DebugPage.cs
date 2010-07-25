using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Cosmos.Build.Common;

namespace Cosmos.VS.Package {
	[Guid(Guids.DebugPage)]
    // Yes this class is actualy used. Its a container for sub pages. Currently we only have one
    // (DebugPageSub) that is used for QEMU and VMWare. Maybe we will have more in the future, or maybe
    // we will merge this into one class like BuildPage.
	public partial class DebugPage : ConfigurationBase {
		private SubPropertyPageBase pageSubPage;

		public DebugPage() : base() {
			InitializeComponent();

			BuildPage.BuildTargetChanged += new EventHandler(BuildPage_BuildTargetChanged);
		}

		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}

            BuildPage.BuildTargetChanged -= new EventHandler(BuildPage_BuildTargetChanged);
			base.Dispose(disposing);
		}

        void BuildPage_BuildTargetChanged(object sender, EventArgs e) {
            FillProperties(); 
        }

		private void ClearSubPage()
		{
			foreach (Control control in this.panelSubPage.Controls)
			{
				this.panelSubPage.Controls.Remove(control);
				control.Dispose();
			}
		}

		private void SetSubPropertyPage(TargetHost target)
		{
			Boolean subpageChanged = false;

			switch (target)
			{
				case TargetHost.QEMU:
					if ((this.pageSubPage is DebugPageSub) == false)
					{
						subpageChanged = true;
						this.pageSubPage = new DebugPageSub();
					}
					break;
				default:
					subpageChanged = true;
					this.pageSubPage = null;
					break;
			}

			if( subpageChanged == true)
			{
				this.panelSubPage.SuspendLayout();

				this.ClearSubPage();
				if (this.pageSubPage != null)
				{
					this.pageSubPage.SetOwner(this);
					this.panelSubPage.Controls.Add(pageSubPage);

					this.pageSubPage.Location = new Point(0, 0);
					this.pageSubPage.Anchor = AnchorStyles.Top;

					this.pageSubPage.Size = new Size(this.ClientSize.Width, this.pageSubPage.Size.Height);
					this.pageSubPage.Anchor = this.pageSubPage.Anchor | AnchorStyles.Left | AnchorStyles.Right;

					if (this.pageSubPage.Size.Height <= this.ClientSize.Height)
					{
						this.pageSubPage.Size = new Size(this.pageSubPage.Size.Width, this.ClientSize.Height);
						this.pageSubPage.Anchor = this.pageSubPage.Anchor | AnchorStyles.Bottom;
					}

					this.panelSubPage.Visible = true;
				} else {
					this.panelSubPage.Visible = false;
				}

				this.panelSubPage.ResumeLayout();
			}

		}

		protected override void FillProperties() {
			base.FillProperties();
			SetSubPropertyPage(BuildPage.CurrentBuildTarget);
			if (pageSubPage != null) {
                pageSubPage.FillProperties(); 
            }
		}

		public override PropertiesBase Properties {
			get	{
				if (pageSubPage != null) {
                    return pageSubPage.Properties; 
                }
				return null;
			}
		}
	}
}
