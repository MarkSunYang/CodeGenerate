using System.ComponentModel;
using System.Windows.Forms;

namespace CodeGenerate.BusinessCode
{
    partial class CodeMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>

		private IContainer components = null;

		private Panel panel1;

		private TextBox txbNamespaces;

		private TextBox txbModelDesc;

		private Label labModelName;

		private Label label3;

		private Label label2;

		private Label label1;

		private TextBox txbPrefix;

		private Label label4;

		private CheckBox ckbController;

		private CheckBox ckbService;

		private CheckBox ckbResponse;

		private CheckBox ckbQuery;

		private CheckBox ckbUpdateRequest;

		private CheckBox ckbMap;

		private CheckBox ckbCreateRequest;

		private Button butGenerate;

		private DataGridView dgvModelContext;

		private Label label5;

		private TextBox txbTableName;

		private DataGridViewTextBoxColumn AttrDesc;

		private DataGridViewTextBoxColumn AttrName;

		private DataGridViewTextBoxColumn AttrDataType;

		private DataGridViewCheckBoxColumn IsCreateRequest;

		private DataGridViewCheckBoxColumn IsUpdateRequest;

		private DataGridViewCheckBoxColumn IsResponse;

		private DataGridViewCheckBoxColumn IsRequired;

		private DataGridViewTextBoxColumn Maximum;

		private DataGridViewTextBoxColumn MinimumValue;

		private void InitializeComponent()
		{
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txbTableName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txbPrefix = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ckbController = new System.Windows.Forms.CheckBox();
            this.ckbService = new System.Windows.Forms.CheckBox();
            this.ckbResponse = new System.Windows.Forms.CheckBox();
            this.ckbQuery = new System.Windows.Forms.CheckBox();
            this.ckbUpdateRequest = new System.Windows.Forms.CheckBox();
            this.ckbMap = new System.Windows.Forms.CheckBox();
            this.ckbCreateRequest = new System.Windows.Forms.CheckBox();
            this.butGenerate = new System.Windows.Forms.Button();
            this.txbNamespaces = new System.Windows.Forms.TextBox();
            this.txbModelDesc = new System.Windows.Forms.TextBox();
            this.labModelName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvModelContext = new System.Windows.Forms.DataGridView();
            this.AttrDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttrName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttrDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsCreateRequest = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsUpdateRequest = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsResponse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsRequired = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Maximum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinimumValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModelContext)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txbTableName);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txbPrefix);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.ckbController);
            this.panel1.Controls.Add(this.ckbService);
            this.panel1.Controls.Add(this.ckbResponse);
            this.panel1.Controls.Add(this.ckbQuery);
            this.panel1.Controls.Add(this.ckbUpdateRequest);
            this.panel1.Controls.Add(this.ckbMap);
            this.panel1.Controls.Add(this.ckbCreateRequest);
            this.panel1.Controls.Add(this.butGenerate);
            this.panel1.Controls.Add(this.txbNamespaces);
            this.panel1.Controls.Add(this.txbModelDesc);
            this.panel1.Controls.Add(this.labModelName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1226, 85);
            this.panel1.TabIndex = 0;
            // 
            // txbTableName
            // 
            this.txbTableName.Location = new System.Drawing.Point(492, 7);
            this.txbTableName.Name = "txbTableName";
            this.txbTableName.Size = new System.Drawing.Size(130, 21);
            this.txbTableName.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(456, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "表名：";
            // 
            // txbPrefix
            // 
            this.txbPrefix.Location = new System.Drawing.Point(320, 7);
            this.txbPrefix.Name = "txbPrefix";
            this.txbPrefix.Size = new System.Drawing.Size(130, 21);
            this.txbPrefix.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(261, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "表名前缀：";
            // 
            // ckbController
            // 
            this.ckbController.AutoSize = true;
            this.ckbController.Location = new System.Drawing.Point(979, 49);
            this.ckbController.Name = "ckbController";
            this.ckbController.Size = new System.Drawing.Size(84, 16);
            this.ckbController.TabIndex = 13;
            this.ckbController.Text = "Controller";
            this.ckbController.UseVisualStyleBackColor = true;
            // 
            // ckbService
            // 
            this.ckbService.AutoSize = true;
            this.ckbService.Location = new System.Drawing.Point(902, 49);
            this.ckbService.Name = "ckbService";
            this.ckbService.Size = new System.Drawing.Size(66, 16);
            this.ckbService.TabIndex = 12;
            this.ckbService.Text = "Service";
            this.ckbService.UseVisualStyleBackColor = true;
            // 
            // ckbResponse
            // 
            this.ckbResponse.AutoSize = true;
            this.ckbResponse.Location = new System.Drawing.Point(1038, 17);
            this.ckbResponse.Name = "ckbResponse";
            this.ckbResponse.Size = new System.Drawing.Size(66, 16);
            this.ckbResponse.TabIndex = 11;
            this.ckbResponse.Text = "返回Dto";
            this.ckbResponse.UseVisualStyleBackColor = true;
            // 
            // ckbQuery
            // 
            this.ckbQuery.AutoSize = true;
            this.ckbQuery.Location = new System.Drawing.Point(961, 17);
            this.ckbQuery.Name = "ckbQuery";
            this.ckbQuery.Size = new System.Drawing.Size(66, 16);
            this.ckbQuery.TabIndex = 10;
            this.ckbQuery.Text = "查询Dto";
            this.ckbQuery.UseVisualStyleBackColor = true;
            // 
            // ckbUpdateRequest
            // 
            this.ckbUpdateRequest.AutoSize = true;
            this.ckbUpdateRequest.Location = new System.Drawing.Point(884, 17);
            this.ckbUpdateRequest.Name = "ckbUpdateRequest";
            this.ckbUpdateRequest.Size = new System.Drawing.Size(66, 16);
            this.ckbUpdateRequest.TabIndex = 9;
            this.ckbUpdateRequest.Text = "修改Dto";
            this.ckbUpdateRequest.UseVisualStyleBackColor = true;
            // 
            // ckbMap
            // 
            this.ckbMap.AutoSize = true;
            this.ckbMap.Location = new System.Drawing.Point(807, 49);
            this.ckbMap.Name = "ckbMap";
            this.ckbMap.Size = new System.Drawing.Size(84, 16);
            this.ckbMap.TabIndex = 8;
            this.ckbMap.Text = "数据库关系";
            this.ckbMap.UseVisualStyleBackColor = true;
            // 
            // ckbCreateRequest
            // 
            this.ckbCreateRequest.AutoSize = true;
            this.ckbCreateRequest.Location = new System.Drawing.Point(807, 17);
            this.ckbCreateRequest.Name = "ckbCreateRequest";
            this.ckbCreateRequest.Size = new System.Drawing.Size(66, 16);
            this.ckbCreateRequest.TabIndex = 7;
            this.ckbCreateRequest.Text = "创建Dto";
            this.ckbCreateRequest.UseVisualStyleBackColor = true;
            // 
            // butGenerate
            // 
            this.butGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.butGenerate.Location = new System.Drawing.Point(1137, 10);
            this.butGenerate.Name = "butGenerate";
            this.butGenerate.Size = new System.Drawing.Size(75, 61);
            this.butGenerate.TabIndex = 6;
            this.butGenerate.Text = "生成";
            this.butGenerate.UseVisualStyleBackColor = true;
            this.butGenerate.Click += new System.EventHandler(this.butGenerate_Click);
            // 
            // txbNamespaces
            // 
            this.txbNamespaces.Location = new System.Drawing.Point(320, 51);
            this.txbNamespaces.Name = "txbNamespaces";
            this.txbNamespaces.Size = new System.Drawing.Size(302, 21);
            this.txbNamespaces.TabIndex = 5;
            // 
            // txbModelDesc
            // 
            this.txbModelDesc.Location = new System.Drawing.Point(74, 50);
            this.txbModelDesc.Name = "txbModelDesc";
            this.txbModelDesc.Size = new System.Drawing.Size(152, 21);
            this.txbModelDesc.TabIndex = 4;
            // 
            // labModelName
            // 
            this.labModelName.AutoSize = true;
            this.labModelName.Location = new System.Drawing.Point(74, 10);
            this.labModelName.Name = "labModelName";
            this.labModelName.Size = new System.Drawing.Size(17, 12);
            this.labModelName.TabIndex = 3;
            this.labModelName.Text = "值";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(261, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "命名空间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "实体描述：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "实体名称：";
            // 
            // dgvModelContext
            // 
            this.dgvModelContext.AllowUserToAddRows = false;
            this.dgvModelContext.AllowUserToDeleteRows = false;
            this.dgvModelContext.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvModelContext.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvModelContext.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvModelContext.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvModelContext.ColumnHeadersHeight = 35;
            this.dgvModelContext.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvModelContext.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AttrDesc,
            this.AttrName,
            this.AttrDataType,
            this.IsCreateRequest,
            this.IsUpdateRequest,
            this.IsResponse,
            this.IsRequired,
            this.Maximum,
            this.MinimumValue});
            this.dgvModelContext.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvModelContext.Location = new System.Drawing.Point(12, 103);
            this.dgvModelContext.Name = "dgvModelContext";
            this.dgvModelContext.RowHeadersVisible = false;
            this.dgvModelContext.RowTemplate.Height = 35;
            this.dgvModelContext.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvModelContext.Size = new System.Drawing.Size(1226, 470);
            this.dgvModelContext.TabIndex = 1;
            // 
            // AttrDesc
            // 
            this.AttrDesc.DataPropertyName = "AttrDesc";
            this.AttrDesc.HeaderText = "属性描述";
            this.AttrDesc.Name = "AttrDesc";
            // 
            // AttrName
            // 
            this.AttrName.DataPropertyName = "AttrName";
            this.AttrName.HeaderText = "属性名称";
            this.AttrName.Name = "AttrName";
            this.AttrName.ReadOnly = true;
            // 
            // AttrDataType
            // 
            this.AttrDataType.DataPropertyName = "AttrDataType";
            this.AttrDataType.HeaderText = "数据类型";
            this.AttrDataType.Name = "AttrDataType";
            this.AttrDataType.ReadOnly = true;
            // 
            // IsCreateRequest
            // 
            this.IsCreateRequest.DataPropertyName = "IsCreateRequest";
            this.IsCreateRequest.HeaderText = "创建参数";
            this.IsCreateRequest.Name = "IsCreateRequest";
            // 
            // IsUpdateRequest
            // 
            this.IsUpdateRequest.DataPropertyName = "IsUpdateRequest";
            this.IsUpdateRequest.HeaderText = "修改参数";
            this.IsUpdateRequest.Name = "IsUpdateRequest";
            this.IsUpdateRequest.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsUpdateRequest.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IsResponse
            // 
            this.IsResponse.DataPropertyName = "IsResponse";
            this.IsResponse.HeaderText = "返回参数";
            this.IsResponse.Name = "IsResponse";
            this.IsResponse.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsResponse.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IsRequired
            // 
            this.IsRequired.DataPropertyName = "IsRequired";
            this.IsRequired.HeaderText = "是否必填";
            this.IsRequired.Name = "IsRequired";
            // 
            // Maximum
            // 
            this.Maximum.DataPropertyName = "MaximumValue";
            this.Maximum.HeaderText = "最大值";
            this.Maximum.Name = "Maximum";
            this.Maximum.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Maximum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MinimumValue
            // 
            this.MinimumValue.DataPropertyName = "MinimumValue";
            this.MinimumValue.HeaderText = "最小值";
            this.MinimumValue.Name = "MinimumValue";
            this.MinimumValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MinimumValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CodeMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(1250, 585);
            this.Controls.Add(this.dgvModelContext);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CodeMain";
            this.Text = "代码生成器";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModelContext)).EndInit();
            this.ResumeLayout(false);

		}
	}
}