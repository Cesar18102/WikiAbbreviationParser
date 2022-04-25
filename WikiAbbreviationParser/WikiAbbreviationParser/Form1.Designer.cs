
namespace WikiAbbreviationParser
{
    partial class Form1
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
            this.LogTextBox = new System.Windows.Forms.RichTextBox();
            this.RootCategoryTextBox = new System.Windows.Forms.TextBox();
            this.RootCategoryTitleLabel = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LogTextBox
            // 
            this.LogTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LogTextBox.Location = new System.Drawing.Point(0, 217);
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.Size = new System.Drawing.Size(800, 233);
            this.LogTextBox.TabIndex = 0;
            this.LogTextBox.Text = "";
            // 
            // RootCategoryTextBox
            // 
            this.RootCategoryTextBox.Location = new System.Drawing.Point(114, 12);
            this.RootCategoryTextBox.Name = "RootCategoryTextBox";
            this.RootCategoryTextBox.Size = new System.Drawing.Size(219, 20);
            this.RootCategoryTextBox.TabIndex = 1;
            // 
            // RootCategoryTitleLabel
            // 
            this.RootCategoryTitleLabel.AutoSize = true;
            this.RootCategoryTitleLabel.Location = new System.Drawing.Point(12, 15);
            this.RootCategoryTitleLabel.Name = "RootCategoryTitleLabel";
            this.RootCategoryTitleLabel.Size = new System.Drawing.Size(96, 13);
            this.RootCategoryTitleLabel.TabIndex = 2;
            this.RootCategoryTitleLabel.Text = "Root category title:";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(12, 38);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(321, 23);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.RootCategoryTitleLabel);
            this.Controls.Add(this.RootCategoryTextBox);
            this.Controls.Add(this.LogTextBox);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox LogTextBox;
        private System.Windows.Forms.TextBox RootCategoryTextBox;
        private System.Windows.Forms.Label RootCategoryTitleLabel;
        private System.Windows.Forms.Button StartButton;
    }
}