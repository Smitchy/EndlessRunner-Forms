﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace GMD2Project___endless_running
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox canvas;



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
            this.canvas = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(900, 900);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // Form1
            // 
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.bitmap = new Bitmap(canvas.Width, canvas.Height);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 900);
            this.Controls.Add(this.canvas);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Endless Runner";
            this.KeyDown += new KeyEventHandler(Input.KeysPressedSetter);
            this.KeyUp += new KeyEventHandler(Input.KeysReleasedSetter);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}

