//**************************************************************//
// Paul Wilson -- www.WilsonDotNet.com -- Paul@WilsonDotNet.com //
// Feel free to use and modify -- just leave these credit lines //
// I also always appreciate any other public credit you provide //
//**************************************************************//
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;

namespace Wilson.MasterPages
{
	[ToolboxData("<{0}:MasterPage runat=server></{0}:MasterPage>"),
		ToolboxItem(typeof(WebControlToolboxItem)),
		Designer(typeof(ReadWriteControlDesigner))]
	public class MasterPage : System.Web.UI.HtmlControls.HtmlContainerControl
	{
		private string templateFile;
		private string defaultContent;

		private Control template = null;
		private ContentRegion defaults = new ContentRegion();
		private ArrayList contents = new ArrayList();

		[Category("MasterPage"), Description("Path of Template User Control")] 
		public string TemplateFile {
			get { return this.templateFile; }
			set { this.templateFile = value; }
		}

		[Category("MasterPage"), Description("Control ID for Default Content")] 
		public string DefaultContent {
			get { return this.defaultContent; }
			set { this.defaultContent = value; }
		}

		public MasterPage() {
			this.templateFile = ConfigurationSettings.AppSettings["Wilson.MasterPages.TemplateFile"];
			this.defaultContent = ConfigurationSettings.AppSettings["Wilson.MasterPages.DefaultContent"];
			if (this.defaultContent == null) {
				this.defaultContent = "Content";
			}
		}

		protected override void AddParsedSubObject(object obj) {
			if (obj is Wilson.MasterPages.ContentRegion) {
				this.contents.Add(obj);
			}
			else {
				this.defaults.Controls.Add((Control)obj);
			}
		}

		protected override void OnInit(EventArgs e) {
			this.BuildMasterPage();
			this.BuildContents();
			base.OnInit(e);
		}

		private void BuildMasterPage() {
			if (this.templateFile == "") {
				throw new Exception("TemplateFile Property for MasterPage must be Defined");
			}
			this.template = this.Page.LoadControl(this.templateFile);
			this.template.ID = this.ID + "_Template";
			
			int count = this.template.Controls.Count;
			for (int index = 0; index < count; index++) {
				Control control = this.template.Controls[0];
				this.template.Controls.Remove(control);
				if (control.Visible) {
					this.Controls.Add(control);
				}
			}
			this.Controls.AddAt(0, this.template);
		}

		private void BuildContents() {
			if (this.defaults.HasControls()) {
				this.defaults.ID = this.defaultContent;
				this.contents.Add(this.defaults);
			}

			foreach (ContentRegion content in this.contents) {
				Control region = this.FindControl(content.ID);
				if (region == null || !(region is Wilson.MasterPages.ContentRegion)) {
					throw new Exception("ContentRegion with ID '" + content.ID + "' must be Defined");
				}
				region.Controls.Clear();
				
				int count = content.Controls.Count;
				for (int index = 0; index < count; index++) {
					Control control = content.Controls[0];
					content.Controls.Remove(control);
					region.Controls.Add(control);
				}
			}
		}

		protected override void RenderBeginTag(System.Web.UI.HtmlTextWriter writer) {}
		protected override void RenderEndTag(System.Web.UI.HtmlTextWriter writer) {}
	}
}
