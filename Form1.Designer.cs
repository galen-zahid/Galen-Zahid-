namespace FsmGraph
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private ScottPlot.FormsPlot formsPlot1;
        private ScottPlot.FormsPlot formsPlot2;
        private ScottPlot.FormsPlot formsPlot3;
        private System.Windows.Forms.Label infoLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.formsPlot2 = new ScottPlot.FormsPlot();
            this.formsPlot3 = new ScottPlot.FormsPlot();
            this.infoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.Location = new System.Drawing.Point(12, 12);
            this.formsPlot1.Size = new System.Drawing.Size(760, 180);
            // 
            // formsPlot2
            // 
            this.formsPlot2.Location = new System.Drawing.Point(12, 210);
            this.formsPlot2.Size = new System.Drawing.Size(760, 180);
            // 
            // formsPlot3
            // 
            this.formsPlot3.Location = new System.Drawing.Point(12, 408);
            this.formsPlot3.Size = new System.Drawing.Size(760, 180);
            // 
            // infoLabel
            // 
            this.infoLabel.Location = new System.Drawing.Point(800, 20);
            this.infoLabel.Size = new System.Drawing.Size(300, 600);
            this.infoLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1120, 610);
            this.Controls.Add(this.formsPlot1);
            this.Controls.Add(this.formsPlot2);
            this.Controls.Add(this.formsPlot3);
            this.Controls.Add(this.infoLabel);
            this.Name = "Form1";
            this.Text = "FSM Monitoring Graph";
            this.ResumeLayout(false);
        }
    }
}
