namespace CSystem.TeaFuncUI
{
    partial class ClassDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cancelButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.usualProNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.capabilityNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.placeTextBox = new System.Windows.Forms.TextBox();
            this.timeTextBox = new System.Windows.Forms.TextBox();
            this.categoryTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.usualProNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.capabilityNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(206, 192);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 21);
            this.cancelButton.TabIndex = 21;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(21, 192);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(179, 21);
            this.addButton.TabIndex = 20;
            this.addButton.Text = "确定";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 27;
            this.label6.Text = "平时成绩权重";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "最大人数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "地点";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 24;
            this.label3.Text = "时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "大类";
            // 
            // usualProNumericUpDown
            // 
            this.usualProNumericUpDown.DecimalPlaces = 2;
            this.usualProNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.usualProNumericUpDown.Location = new System.Drawing.Point(106, 157);
            this.usualProNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.usualProNumericUpDown.Name = "usualProNumericUpDown";
            this.usualProNumericUpDown.Size = new System.Drawing.Size(175, 21);
            this.usualProNumericUpDown.TabIndex = 19;
            this.usualProNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // capabilityNumericUpDown
            // 
            this.capabilityNumericUpDown.Location = new System.Drawing.Point(106, 130);
            this.capabilityNumericUpDown.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.capabilityNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.capabilityNumericUpDown.Name = "capabilityNumericUpDown";
            this.capabilityNumericUpDown.Size = new System.Drawing.Size(175, 21);
            this.capabilityNumericUpDown.TabIndex = 18;
            this.capabilityNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // placeTextBox
            // 
            this.placeTextBox.Location = new System.Drawing.Point(106, 103);
            this.placeTextBox.Name = "placeTextBox";
            this.placeTextBox.Size = new System.Drawing.Size(175, 21);
            this.placeTextBox.TabIndex = 17;
            // 
            // timeTextBox
            // 
            this.timeTextBox.Location = new System.Drawing.Point(106, 76);
            this.timeTextBox.Name = "timeTextBox";
            this.timeTextBox.Size = new System.Drawing.Size(175, 21);
            this.timeTextBox.TabIndex = 16;
            // 
            // categoryTextBox
            // 
            this.categoryTextBox.Location = new System.Drawing.Point(106, 49);
            this.categoryTextBox.Name = "categoryTextBox";
            this.categoryTextBox.Size = new System.Drawing.Size(175, 21);
            this.categoryTextBox.TabIndex = 15;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(106, 22);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(175, 21);
            this.nameTextBox.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "课程名称";
            // 
            // ClassDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 230);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.usualProNumericUpDown);
            this.Controls.Add(this.capabilityNumericUpDown);
            this.Controls.Add(this.placeTextBox);
            this.Controls.Add(this.timeTextBox);
            this.Controls.Add(this.categoryTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label1);
            this.Name = "ClassDialog";
            this.Text = "新增课程";
            ((System.ComponentModel.ISupportInitialize)(this.usualProNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.capabilityNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown usualProNumericUpDown;
        private System.Windows.Forms.NumericUpDown capabilityNumericUpDown;
        private System.Windows.Forms.TextBox placeTextBox;
        private System.Windows.Forms.TextBox timeTextBox;
        private System.Windows.Forms.TextBox categoryTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label1;
    }
}