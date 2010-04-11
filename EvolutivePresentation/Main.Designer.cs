namespace EvolutivePresentation
{
    partial class MainForm
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
            this.lblX0 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numIterations = new System.Windows.Forms.NumericUpDown();
            this.numIndividuals = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblX3 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblX2 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblX1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblXX0 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.numSelectionCount2 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numSelectionCount1 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbSelectionMethod = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cmbReplacementMethod = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.numReplacementCount2 = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numReplacementCount1 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtMutation = new System.Windows.Forms.MaskedTextBox();
            this.lblX0.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIndividuals)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSelectionCount2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSelectionCount1)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReplacementCount2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReplacementCount1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblX0
            // 
            this.lblX0.Controls.Add(this.txtOutput);
            this.lblX0.Location = new System.Drawing.Point(298, 16);
            this.lblX0.Name = "lblX0";
            this.lblX0.Size = new System.Drawing.Size(531, 351);
            this.lblX0.TabIndex = 0;
            this.lblX0.TabStop = false;
            this.lblX0.Text = "Output";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblStatus);
            this.groupBox2.Controls.Add(this.btnStart);
            this.groupBox2.Location = new System.Drawing.Point(21, 412);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(254, 82);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Acciones";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 52);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(87, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Iniciar";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtMutation);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.numIterations);
            this.groupBox3.Controls.Add(this.numIndividuals);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(21, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(254, 115);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Parametros";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Mutacion (%)";
            // 
            // numIterations
            // 
            this.numIterations.Location = new System.Drawing.Point(135, 51);
            this.numIterations.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.numIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIterations.Name = "numIterations";
            this.numIterations.Size = new System.Drawing.Size(101, 20);
            this.numIterations.TabIndex = 5;
            this.numIterations.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // numIndividuals
            // 
            this.numIndividuals.Location = new System.Drawing.Point(135, 24);
            this.numIndividuals.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numIndividuals.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numIndividuals.Name = "numIndividuals";
            this.numIndividuals.Size = new System.Drawing.Size(101, 20);
            this.numIndividuals.TabIndex = 3;
            this.numIndividuals.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Iteraciones";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Individuos";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.lblX3);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.lblX2);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.lblX1);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.lblXX0);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(298, 373);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(531, 121);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Solucion";
            // 
            // lblX3
            // 
            this.lblX3.AutoSize = true;
            this.lblX3.Location = new System.Drawing.Point(77, 91);
            this.lblX3.Name = "lblX3";
            this.lblX3.Size = new System.Drawing.Size(0, 13);
            this.lblX3.TabIndex = 7;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(18, 91);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(32, 13);
            this.label16.TabIndex = 6;
            this.label16.Text = "X3 = ";
            // 
            // lblX2
            // 
            this.lblX2.AutoSize = true;
            this.lblX2.Location = new System.Drawing.Point(77, 68);
            this.lblX2.Name = "lblX2";
            this.lblX2.Size = new System.Drawing.Size(0, 13);
            this.lblX2.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(18, 68);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "X2 = ";
            // 
            // lblX1
            // 
            this.lblX1.AutoSize = true;
            this.lblX1.Location = new System.Drawing.Point(77, 48);
            this.lblX1.Name = "lblX1";
            this.lblX1.Size = new System.Drawing.Size(0, 13);
            this.lblX1.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "X1 = ";
            // 
            // lblXX0
            // 
            this.lblXX0.AutoSize = true;
            this.lblXX0.Location = new System.Drawing.Point(77, 25);
            this.lblXX0.Name = "lblXX0";
            this.lblXX0.Size = new System.Drawing.Size(0, 13);
            this.lblXX0.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "X0 = ";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.numSelectionCount2);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.numSelectionCount1);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.cmbSelectionMethod);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(21, 148);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(254, 108);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Seleccion";
            // 
            // numSelectionCount2
            // 
            this.numSelectionCount2.Location = new System.Drawing.Point(141, 82);
            this.numSelectionCount2.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numSelectionCount2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSelectionCount2.Name = "numSelectionCount2";
            this.numSelectionCount2.Size = new System.Drawing.Size(101, 20);
            this.numSelectionCount2.TabIndex = 19;
            this.numSelectionCount2.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Cantidad Metodo2";
            // 
            // numSelectionCount1
            // 
            this.numSelectionCount1.Location = new System.Drawing.Point(141, 51);
            this.numSelectionCount1.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numSelectionCount1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSelectionCount1.Name = "numSelectionCount1";
            this.numSelectionCount1.Size = new System.Drawing.Size(101, 20);
            this.numSelectionCount1.TabIndex = 17;
            this.numSelectionCount1.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Cantidad Metodo1";
            // 
            // cmbSelectionMethod
            // 
            this.cmbSelectionMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectionMethod.FormattingEnabled = true;
            this.cmbSelectionMethod.Items.AddRange(new object[] {
            "Ruleta",
            "Elitismo",
            "Universal",
            "ElitismoRuleta",
            "ElitismoUniversal"});
            this.cmbSelectionMethod.Location = new System.Drawing.Point(141, 22);
            this.cmbSelectionMethod.Name = "cmbSelectionMethod";
            this.cmbSelectionMethod.Size = new System.Drawing.Size(101, 21);
            this.cmbSelectionMethod.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Seleccion";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cmbReplacementMethod);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.numReplacementCount2);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.numReplacementCount1);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Location = new System.Drawing.Point(21, 281);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(254, 114);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Reemplazo";
            // 
            // cmbReplacementMethod
            // 
            this.cmbReplacementMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReplacementMethod.FormattingEnabled = true;
            this.cmbReplacementMethod.Items.AddRange(new object[] {
            "Ruleta",
            "Elitismo",
            "Universal",
            "ElitismoRuleta",
            "ElitismoUniversal"});
            this.cmbReplacementMethod.Location = new System.Drawing.Point(141, 21);
            this.cmbReplacementMethod.Name = "cmbReplacementMethod";
            this.cmbReplacementMethod.Size = new System.Drawing.Size(101, 21);
            this.cmbReplacementMethod.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Reemplazo";
            // 
            // numReplacementCount2
            // 
            this.numReplacementCount2.Location = new System.Drawing.Point(141, 81);
            this.numReplacementCount2.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numReplacementCount2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numReplacementCount2.Name = "numReplacementCount2";
            this.numReplacementCount2.Size = new System.Drawing.Size(101, 20);
            this.numReplacementCount2.TabIndex = 23;
            this.numReplacementCount2.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Cantidad Metodo2";
            // 
            // numReplacementCount1
            // 
            this.numReplacementCount1.Location = new System.Drawing.Point(141, 53);
            this.numReplacementCount1.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numReplacementCount1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numReplacementCount1.Name = "numReplacementCount1";
            this.numReplacementCount1.Size = new System.Drawing.Size(101, 20);
            this.numReplacementCount1.TabIndex = 21;
            this.numReplacementCount1.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Cantidad Metodo1";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Go);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkCompleted);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(21, 24);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(497, 309);
            this.txtOutput.TabIndex = 0;
            // 
            // txtMutation
            // 
            this.txtMutation.Location = new System.Drawing.Point(135, 81);
            this.txtMutation.Mask = "99.9999";
            this.txtMutation.Name = "txtMutation";
            this.txtMutation.Size = new System.Drawing.Size(100, 20);
            this.txtMutation.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 506);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblX0);
            this.MinimumSize = new System.Drawing.Size(738, 396);
            this.Name = "MainForm";
            this.Text = "Optimizacion de Funciones";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.lblX0.ResumeLayout(false);
            this.lblX0.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIndividuals)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSelectionCount2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSelectionCount1)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReplacementCount2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReplacementCount1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox lblX0;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numIndividuals;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numIterations;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown numSelectionCount2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numSelectionCount1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbSelectionMethod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cmbReplacementMethod;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numReplacementCount2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numReplacementCount1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblX3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblX2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblX1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblXX0;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.MaskedTextBox txtMutation;
    }
}