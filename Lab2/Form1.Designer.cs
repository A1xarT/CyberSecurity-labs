
namespace Lab2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Parse_Data = new System.Windows.Forms.Button();
            this.GraphOne = new System.Windows.Forms.Button();
            this.GraphTwo = new System.Windows.Forms.Button();
            this.GraphThree = new System.Windows.Forms.Button();
            this.comparisonCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Parse_Data
            // 
            this.Parse_Data.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Parse_Data.Location = new System.Drawing.Point(28, 17);
            this.Parse_Data.Name = "Parse_Data";
            this.Parse_Data.Size = new System.Drawing.Size(149, 46);
            this.Parse_Data.TabIndex = 0;
            this.Parse_Data.Text = "Parse Data";
            this.Parse_Data.UseVisualStyleBackColor = true;
            this.Parse_Data.Click += new System.EventHandler(this.Parse_Data_Click);
            // 
            // GraphOne
            // 
            this.GraphOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GraphOne.Location = new System.Drawing.Point(393, 17);
            this.GraphOne.Name = "GraphOne";
            this.GraphOne.Size = new System.Drawing.Size(149, 46);
            this.GraphOne.TabIndex = 1;
            this.GraphOne.Text = "By levels";
            this.GraphOne.UseVisualStyleBackColor = true;
            this.GraphOne.Click += new System.EventHandler(this.GraphOne_Click);
            // 
            // GraphTwo
            // 
            this.GraphTwo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GraphTwo.Location = new System.Drawing.Point(548, 17);
            this.GraphTwo.Name = "GraphTwo";
            this.GraphTwo.Size = new System.Drawing.Size(149, 46);
            this.GraphTwo.TabIndex = 2;
            this.GraphTwo.Text = "Expectation";
            this.GraphTwo.UseVisualStyleBackColor = true;
            this.GraphTwo.Click += new System.EventHandler(this.GraphTwo_Click);
            // 
            // GraphThree
            // 
            this.GraphThree.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GraphThree.Location = new System.Drawing.Point(703, 17);
            this.GraphThree.Name = "GraphThree";
            this.GraphThree.Size = new System.Drawing.Size(149, 46);
            this.GraphThree.TabIndex = 3;
            this.GraphThree.Text = "Distribution";
            this.GraphThree.UseVisualStyleBackColor = true;
            this.GraphThree.Click += new System.EventHandler(this.GraphThree_Click);
            // 
            // comparisonCheck
            // 
            this.comparisonCheck.AutoSize = true;
            this.comparisonCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comparisonCheck.Location = new System.Drawing.Point(566, 69);
            this.comparisonCheck.Name = "comparisonCheck";
            this.comparisonCheck.Size = new System.Drawing.Size(131, 28);
            this.comparisonCheck.TabIndex = 4;
            this.comparisonCheck.Text = "Comparison";
            this.comparisonCheck.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.comparisonCheck);
            this.Controls.Add(this.GraphThree);
            this.Controls.Add(this.GraphTwo);
            this.Controls.Add(this.GraphOne);
            this.Controls.Add(this.Parse_Data);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Parse_Data;
        private System.Windows.Forms.Button GraphOne;
        private System.Windows.Forms.Button GraphTwo;
        private System.Windows.Forms.Button GraphThree;
        private System.Windows.Forms.CheckBox comparisonCheck;
    }
}

