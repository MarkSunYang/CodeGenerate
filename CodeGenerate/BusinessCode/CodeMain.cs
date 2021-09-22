using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGenerate.BusinessCode
{
    public partial class CodeMain : Form
    {

		public CodeMain()
		{
			InitializeComponent();
			Init();
			labModelName.Text = ModelAnalysis._Model.ModelName;
			txbModelDesc.Text = ModelAnalysis._Model.ModelDesc;
			txbNamespaces.Text = ModelAnalysis._Model.Namespace;
			txbPrefix.Text = ModelAnalysis._Model.Prefix;
			txbTableName.Text = ModelAnalysis._Model.TableName;
			ckbCreateRequest.Checked = ModelAnalysis._Model.IsCreateDto;
			ckbController.Checked = ModelAnalysis._Model.IsController;
			ckbMap.Checked = ModelAnalysis._Model.IsMap;
			ckbResponse.Checked = ModelAnalysis._Model.IsResponseDto;
			ckbUpdateRequest.Checked = ModelAnalysis._Model.IsResponseDto;
			ckbService.Checked = ModelAnalysis._Model.IsService;
			ckbQuery.Checked = ModelAnalysis._Model.IsQueryDto;
		}

		public void Init()
		{
			dgvModelContext.DataSource = ModelAnalysis._Model.ModelField;
			dgvModelContext.RowsDefaultCellStyle.BackColor = Color.Bisque;
			dgvModelContext.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
		}

		private void butGenerate_Click(object sender, EventArgs e)
		{
			ModelAnalysis._Model.Prefix = txbPrefix.Text;
			ModelAnalysis._Model.TableName = txbTableName.Text;
			ModelAnalysis._Model.Namespace = txbNamespaces.Text;
			ModelAnalysis._Model.ModelDesc = txbModelDesc.Text;
			CodeingGenerate codeingGenerate = new CodeingGenerate();
			if (ckbMap.Checked)
			{
				codeingGenerate.CodeingMap();
			}
			if (ckbQuery.Checked)
			{
				codeingGenerate.CodeingQuery();
			}
			if (ckbResponse.Checked)
			{
				codeingGenerate.CodeingResponse();
			}
			if (ckbUpdateRequest.Checked)
			{
				codeingGenerate.CodeingUpdate();
			}
			if (ckbCreateRequest.Checked)
			{
				codeingGenerate.CodeingCreate();
			}
			if (ckbService.Checked)
			{
				if (ModelAnalysis._Model.BaseEntities.IndexOf("AggregateRoot") > -1)
				{
					codeingGenerate.CodeingIService();
					codeingGenerate.CodeingService();
				}
				else
				{
					codeingGenerate.CodeingService();
					codeingGenerate.CodeingIService();
				}
			}
			if (ckbController.Checked)
			{
				if (ModelAnalysis._Model.BaseEntities.IndexOf("AggregateRoot") > -1)
				{
					codeingGenerate.CodeingController();
				}
				else
				{
					codeingGenerate.CodeingBaseController();
				}
			}
			MessageBox.Show("生成完毕");
		}
	}
}
