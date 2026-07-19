namespace MJpegCast
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.titleBarPanel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            
            this.stepContainerPanel = new System.Windows.Forms.Panel();
            
            // Step 1 Panel
            this.step1NetworkPanel = new System.Windows.Forms.Panel();
            this.lblStep1Title = new System.Windows.Forms.Label();
            this.lblIp = new System.Windows.Forms.Label();
            this.ipComboBox = new System.Windows.Forms.ComboBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.portNumeric = new System.Windows.Forms.NumericUpDown();
            this.networkDescLabel = new System.Windows.Forms.Label();
            
            // Step 2 Panel
            this.step2SourcePanel = new System.Windows.Forms.Panel();
            this.lblStep2Title = new System.Windows.Forms.Label();
            this.screenComboBox = new System.Windows.Forms.ComboBox();
            
            this.qualityPresetsPanel = new System.Windows.Forms.Panel();
            this.radioLow = new System.Windows.Forms.RadioButton();
            this.radioMedium = new System.Windows.Forms.RadioButton();
            this.radioHigh = new System.Windows.Forms.RadioButton();
            this.radioCustom = new System.Windows.Forms.RadioButton();
            
            this.scaleSliderLabel = new System.Windows.Forms.Label();
            this.scaleTrackBar = new System.Windows.Forms.TrackBar();
            this.fpsSliderLabel = new System.Windows.Forms.Label();
            this.fpsTrackBar = new System.Windows.Forms.TrackBar();
            this.qualitySliderLabel = new System.Windows.Forms.Label();
            this.qualityTrackBar = new System.Windows.Forms.TrackBar();
            
            // Step 3 Panel
            this.step3SharingPanel = new System.Windows.Forms.Panel();
            this.lblStep3Title = new System.Windows.Forms.Label();
            this.statusIndicator = new System.Windows.Forms.Label();
            this.urlTitleLabel = new System.Windows.Forms.Label();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.copyUrlButton = new System.Windows.Forms.Button();
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.lblClients = new System.Windows.Forms.Label();
            this.lblFps = new System.Windows.Forms.Label();
            this.lblBandwidth = new System.Windows.Forms.Label();
            
            // Footer Panel
            this.footerPanel = new System.Windows.Forms.Panel();
            this.exitButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.lblAuthor = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.portNumeric)).BeginInit();
            this.stepContainerPanel.SuspendLayout();
            this.step1NetworkPanel.SuspendLayout();
            this.step2SourcePanel.SuspendLayout();
            this.qualityPresetsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scaleTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualityTrackBar)).BeginInit();
            this.step3SharingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            this.footerPanel.SuspendLayout();
            this.SuspendLayout();

            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MJpegCast - Wizard";
            this.Controls.Add(this.stepContainerPanel);
            this.Controls.Add(this.titleBarPanel);
            this.Controls.Add(this.footerPanel);

            // 
            // titleBarPanel
            // 
            this.titleBarPanel.BackColor = System.Drawing.Color.White;
            this.titleBarPanel.Controls.Add(this.lblTitle);
            this.titleBarPanel.Controls.Add(this.lblSubtitle);
            this.titleBarPanel.Controls.Add(this.btnMinimize);
            this.titleBarPanel.Controls.Add(this.btnClose);
            this.titleBarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBarPanel.Location = new System.Drawing.Point(0, 0);
            this.titleBarPanel.Name = "titleBarPanel";
            this.titleBarPanel.Size = new System.Drawing.Size(800, 48);

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.lblTitle.Location = new System.Drawing.Point(14, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "MJpegCast";

            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSubtitle.Location = new System.Drawing.Point(110, 17);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Text = "—  Stream Setup Wizard";

            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnMinimize.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnMinimize.Location = new System.Drawing.Point(720, 6);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(36, 36);
            this.btnMinimize.Text = "─";
            this.btnMinimize.UseVisualStyleBackColor = false;

            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(254, 226, 226);
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(252, 165, 165);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnClose.Location = new System.Drawing.Point(758, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(36, 36);
            this.btnClose.Text = "✕";
            this.btnClose.UseVisualStyleBackColor = false;

            // 
            // stepContainerPanel
            // 
            this.stepContainerPanel.Controls.Add(this.step1NetworkPanel);
            this.stepContainerPanel.Controls.Add(this.step2SourcePanel);
            this.stepContainerPanel.Controls.Add(this.step3SharingPanel);
            this.stepContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepContainerPanel.Location = new System.Drawing.Point(0, 48);
            this.stepContainerPanel.Name = "stepContainerPanel";
            this.stepContainerPanel.Padding = new System.Windows.Forms.Padding(16);
            this.stepContainerPanel.Size = new System.Drawing.Size(800, 498);

            // 
            // step1NetworkPanel
            // 
            this.step1NetworkPanel.BackColor = System.Drawing.Color.White;
            this.step1NetworkPanel.Controls.Add(this.lblStep1Title);
            this.step1NetworkPanel.Controls.Add(this.lblIp);
            this.step1NetworkPanel.Controls.Add(this.ipComboBox);
            this.step1NetworkPanel.Controls.Add(this.lblPort);
            this.step1NetworkPanel.Controls.Add(this.portNumeric);
            this.step1NetworkPanel.Controls.Add(this.networkDescLabel);
            this.step1NetworkPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.step1NetworkPanel.Location = new System.Drawing.Point(16, 16);
            this.step1NetworkPanel.Name = "step1NetworkPanel";
            this.step1NetworkPanel.Size = new System.Drawing.Size(760, 458);

            // 
            // lblStep1Title
            // 
            this.lblStep1Title.AutoSize = true;
            this.lblStep1Title.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStep1Title.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblStep1Title.Location = new System.Drawing.Point(16, 16);
            this.lblStep1Title.Name = "lblStep1Title";
            this.lblStep1Title.Text = "STEP 1: HOST CONNECTION SETTINGS";

            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblIp.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblIp.Location = new System.Drawing.Point(16, 44);
            this.lblIp.Name = "lblIp";
            this.lblIp.Text = "Network IP Address:";

            // 
            // ipComboBox
            // 
            this.ipComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ipComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ipComboBox.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ipComboBox.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.ipComboBox.Location = new System.Drawing.Point(16, 64);
            this.ipComboBox.Name = "ipComboBox";
            this.ipComboBox.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ipComboBox.Size = new System.Drawing.Size(728, 29);

            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPort.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblPort.Location = new System.Drawing.Point(16, 114);
            this.lblPort.Name = "lblPort";
            this.lblPort.Text = "Server Port:";

            // 
            // portNumeric
            // 
            this.portNumeric.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.portNumeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.portNumeric.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.portNumeric.Location = new System.Drawing.Point(16, 134);
            this.portNumeric.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            this.portNumeric.Minimum = new decimal(new int[] { 1024, 0, 0, 0 });
            this.portNumeric.Name = "portNumeric";
            this.portNumeric.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.portNumeric.Size = new System.Drawing.Size(140, 29);
            this.portNumeric.Value = new decimal(new int[] { 8080, 0, 0, 0 });

            // 
            // networkDescLabel
            // 
            this.networkDescLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.networkDescLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.networkDescLabel.Location = new System.Drawing.Point(16, 196);
            this.networkDescLabel.Name = "networkDescLabel";
            this.networkDescLabel.Size = new System.Drawing.Size(728, 120);
            this.networkDescLabel.Text = "Choose loopback (127.0.0.1) for local testing or a local network IP adapter (e.g. 192.168.x.x) to share your desktop with other devices connected to the same Wi-Fi/Ethernet network.";

            // 
            // step2SourcePanel
            // 
            this.step2SourcePanel.BackColor = System.Drawing.Color.White;
            this.step2SourcePanel.Controls.Add(this.lblStep2Title);
            this.step2SourcePanel.Controls.Add(this.screenComboBox);
            this.step2SourcePanel.Controls.Add(this.qualityPresetsPanel);
            this.step2SourcePanel.Controls.Add(this.scaleSliderLabel);
            this.step2SourcePanel.Controls.Add(this.scaleTrackBar);
            this.step2SourcePanel.Controls.Add(this.fpsSliderLabel);
            this.step2SourcePanel.Controls.Add(this.fpsTrackBar);
            this.step2SourcePanel.Controls.Add(this.qualitySliderLabel);
            this.step2SourcePanel.Controls.Add(this.qualityTrackBar);
            this.step2SourcePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.step2SourcePanel.Location = new System.Drawing.Point(16, 16);
            this.step2SourcePanel.Name = "step2SourcePanel";
            this.step2SourcePanel.Size = new System.Drawing.Size(760, 458);

            // 
            // lblStep2Title
            // 
            this.lblStep2Title.AutoSize = true;
            this.lblStep2Title.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStep2Title.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblStep2Title.Location = new System.Drawing.Point(16, 16);
            this.lblStep2Title.Name = "lblStep2Title";
            this.lblStep2Title.Text = "STEP 2: STREAM SOURCE & QUALITY";

            // 
            // screenComboBox
            // 
            this.screenComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.screenComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.screenComboBox.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.screenComboBox.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.screenComboBox.Location = new System.Drawing.Point(16, 44);
            this.screenComboBox.Name = "screenComboBox";
            this.screenComboBox.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.screenComboBox.Size = new System.Drawing.Size(728, 29);

            // 
            // qualityPresetsPanel
            // 
            this.qualityPresetsPanel.BackColor = System.Drawing.Color.Transparent;
            this.qualityPresetsPanel.Controls.Add(this.radioLow);
            this.qualityPresetsPanel.Controls.Add(this.radioMedium);
            this.qualityPresetsPanel.Controls.Add(this.radioHigh);
            this.qualityPresetsPanel.Controls.Add(this.radioCustom);
            this.qualityPresetsPanel.Location = new System.Drawing.Point(16, 96);
            this.qualityPresetsPanel.Name = "qualityPresetsPanel";
            this.qualityPresetsPanel.Size = new System.Drawing.Size(728, 30);

            // 
            // radioLow
            // 
            this.radioLow.AutoSize = true;
            this.radioLow.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioLow.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.radioLow.Location = new System.Drawing.Point(0, 4);
            this.radioLow.Name = "radioLow";
            this.radioLow.Size = new System.Drawing.Size(100, 19);
            this.radioLow.Text = "Low Quality";

            // 
            // radioMedium
            // 
            this.radioMedium.AutoSize = true;
            this.radioMedium.Checked = true;
            this.radioMedium.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioMedium.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.radioMedium.Location = new System.Drawing.Point(114, 4);
            this.radioMedium.Name = "radioMedium";
            this.radioMedium.Size = new System.Drawing.Size(110, 19);
            this.radioMedium.Text = "Medium Quality";

            // 
            // radioHigh
            // 
            this.radioHigh.AutoSize = true;
            this.radioHigh.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioHigh.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.radioHigh.Location = new System.Drawing.Point(234, 4);
            this.radioHigh.Name = "radioHigh";
            this.radioHigh.Size = new System.Drawing.Size(100, 19);
            this.radioHigh.Text = "High Quality";

            // 
            // radioCustom
            // 
            this.radioCustom.AutoSize = true;
            this.radioCustom.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioCustom.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.radioCustom.Location = new System.Drawing.Point(349, 4);
            this.radioCustom.Name = "radioCustom";
            this.radioCustom.Size = new System.Drawing.Size(125, 19);
            this.radioCustom.Text = "Custom Settings...";

            // 
            // scaleSliderLabel
            // 
            this.scaleSliderLabel.AutoSize = true;
            this.scaleSliderLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.scaleSliderLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.scaleSliderLabel.Location = new System.Drawing.Point(16, 140);
            this.scaleSliderLabel.Name = "scaleSliderLabel";
            this.scaleSliderLabel.Size = new System.Drawing.Size(340, 18);
            this.scaleSliderLabel.Text = "Scale: 75%";

            // 
            // scaleTrackBar
            // 
            this.scaleTrackBar.BackColor = System.Drawing.Color.White;
            this.scaleTrackBar.Location = new System.Drawing.Point(16, 162);
            this.scaleTrackBar.Maximum = 100;
            this.scaleTrackBar.Minimum = 25;
            this.scaleTrackBar.Name = "scaleTrackBar";
            this.scaleTrackBar.Size = new System.Drawing.Size(340, 45);
            this.scaleTrackBar.TickFrequency = 25;
            this.scaleTrackBar.Value = 75;

            // 
            // fpsSliderLabel
            // 
            this.fpsSliderLabel.AutoSize = true;
            this.fpsSliderLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fpsSliderLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.fpsSliderLabel.Location = new System.Drawing.Point(16, 220);
            this.fpsSliderLabel.Name = "fpsSliderLabel";
            this.fpsSliderLabel.Size = new System.Drawing.Size(340, 18);
            this.fpsSliderLabel.Text = "Framerate: 15 FPS";

            // 
            // fpsTrackBar
            // 
            this.fpsTrackBar.BackColor = System.Drawing.Color.White;
            this.fpsTrackBar.Location = new System.Drawing.Point(16, 242);
            this.fpsTrackBar.Maximum = 60;
            this.fpsTrackBar.Minimum = 5;
            this.fpsTrackBar.Name = "fpsTrackBar";
            this.fpsTrackBar.Size = new System.Drawing.Size(340, 45);
            this.fpsTrackBar.TickFrequency = 5;
            this.fpsTrackBar.Value = 15;

            // 
            // qualitySliderLabel
            // 
            this.qualitySliderLabel.AutoSize = true;
            this.qualitySliderLabel.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.qualitySliderLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.qualitySliderLabel.Location = new System.Drawing.Point(404, 140);
            this.qualitySliderLabel.Name = "qualitySliderLabel";
            this.qualitySliderLabel.Size = new System.Drawing.Size(340, 18);
            this.qualitySliderLabel.Text = "JPEG Quality: 60%";

            // 
            // qualityTrackBar
            // 
            this.qualityTrackBar.BackColor = System.Drawing.Color.White;
            this.qualityTrackBar.Location = new System.Drawing.Point(404, 162);
            this.qualityTrackBar.Maximum = 100;
            this.qualityTrackBar.Minimum = 10;
            this.qualityTrackBar.Name = "qualityTrackBar";
            this.qualityTrackBar.Size = new System.Drawing.Size(340, 45);
            this.qualityTrackBar.TickFrequency = 10;
            this.qualityTrackBar.Value = 60;

            // 
            // step3SharingPanel
            // 
            this.step3SharingPanel.BackColor = System.Drawing.Color.White;
            this.step3SharingPanel.Controls.Add(this.lblStep3Title);
            this.step3SharingPanel.Controls.Add(this.statusIndicator);
            this.step3SharingPanel.Controls.Add(this.urlTitleLabel);
            this.step3SharingPanel.Controls.Add(this.urlTextBox);
            this.step3SharingPanel.Controls.Add(this.copyUrlButton);
            this.step3SharingPanel.Controls.Add(this.previewBox);
            this.step3SharingPanel.Controls.Add(this.lblClients);
            this.step3SharingPanel.Controls.Add(this.lblFps);
            this.step3SharingPanel.Controls.Add(this.lblBandwidth);
            this.step3SharingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.step3SharingPanel.Location = new System.Drawing.Point(16, 16);
            this.step3SharingPanel.Name = "step3SharingPanel";
            this.step3SharingPanel.Size = new System.Drawing.Size(760, 458);

            // 
            // lblStep3Title
            // 
            this.lblStep3Title.AutoSize = true;
            this.lblStep3Title.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStep3Title.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblStep3Title.Location = new System.Drawing.Point(16, 12);
            this.lblStep3Title.Name = "lblStep3Title";
            this.lblStep3Title.Text = "STEP 3: DESKTOP SHARING CONTROL";

            // 
            // statusIndicator
            // 
            this.statusIndicator.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.statusIndicator.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.statusIndicator.Location = new System.Drawing.Point(16, 32);
            this.statusIndicator.Name = "statusIndicator";
            this.statusIndicator.Size = new System.Drawing.Size(728, 20);
            this.statusIndicator.Text = "● Streaming Offline";

            // 
            // urlTitleLabel
            // 
            this.urlTitleLabel.AutoSize = true;
            this.urlTitleLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.urlTitleLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.urlTitleLabel.Location = new System.Drawing.Point(16, 62);
            this.urlTitleLabel.Name = "urlTitleLabel";
            this.urlTitleLabel.Text = "Viewer Connection URL:";

            // 
            // urlTextBox
            // 
            this.urlTextBox.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.urlTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.urlTextBox.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.urlTextBox.Location = new System.Drawing.Point(16, 80);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.ReadOnly = true;
            this.urlTextBox.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.urlTextBox.Size = new System.Drawing.Size(570, 29);

            // 
            // copyUrlButton
            // 
            this.copyUrlButton.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.copyUrlButton.FlatAppearance.BorderSize = 0;
            this.copyUrlButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.copyUrlButton.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.copyUrlButton.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.copyUrlButton.Location = new System.Drawing.Point(596, 78);
            this.copyUrlButton.Name = "copyUrlButton";
            this.copyUrlButton.Size = new System.Drawing.Size(148, 30);
            this.copyUrlButton.Text = "Copy URL";
            this.copyUrlButton.UseVisualStyleBackColor = false;

            // 
            // previewBox
            // 
            this.previewBox.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.previewBox.Location = new System.Drawing.Point(16, 126);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(480, 270);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // 
            // lblClients
            // 
            this.lblClients.AutoSize = true;
            this.lblClients.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblClients.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblClients.Location = new System.Drawing.Point(520, 136);
            this.lblClients.Name = "lblClients";
            this.lblClients.Text = "Active Viewers: 0";

            // 
            // lblFps
            // 
            this.lblFps.AutoSize = true;
            this.lblFps.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFps.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblFps.Location = new System.Drawing.Point(520, 176);
            this.lblFps.Name = "lblFps";
            this.lblFps.Text = "Framerate: 0.0 FPS";

            // 
            // lblBandwidth
            // 
            this.lblBandwidth.AutoSize = true;
            this.lblBandwidth.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblBandwidth.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblBandwidth.Location = new System.Drawing.Point(520, 216);
            this.lblBandwidth.Name = "lblBandwidth";
            this.lblBandwidth.Text = "Bandwidth: 0.00 MB/s";

            // 
            // footerPanel
            // 
            this.footerPanel.BackColor = System.Drawing.Color.White;
            this.footerPanel.Controls.Add(this.exitButton);
            this.footerPanel.Controls.Add(this.backButton);
            this.footerPanel.Controls.Add(this.nextButton);
            this.footerPanel.Controls.Add(this.lblAuthor);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 546);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(800, 54);

            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.exitButton.FlatAppearance.BorderSize = 0;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.exitButton.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.exitButton.Location = new System.Drawing.Point(16, 10);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(100, 34);
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;

            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.backButton.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.backButton.Location = new System.Drawing.Point(552, 10);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(100, 34);
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = false;

            // 
            // nextButton
            // 
            this.nextButton.BackColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.nextButton.FlatAppearance.BorderSize = 0;
            this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.nextButton.ForeColor = System.Drawing.Color.White;
            this.nextButton.Location = new System.Drawing.Point(668, 10);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(116, 34);
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = false;

            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblAuthor.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblAuthor.Location = new System.Drawing.Point(220, 20);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(260, 15);
            this.lblAuthor.Text = "MJpegCast v1.0.0 | Developed by Ajay Randhawa";

            ((System.ComponentModel.ISupportInitialize)(this.portNumeric)).EndInit();
            this.stepContainerPanel.ResumeLayout(false);
            this.step1NetworkPanel.ResumeLayout(false);
            this.step1NetworkPanel.PerformLayout();
            this.step2SourcePanel.ResumeLayout(false);
            this.step2SourcePanel.PerformLayout();
            this.qualityPresetsPanel.ResumeLayout(false);
            this.qualityPresetsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scaleTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualityTrackBar)).EndInit();
            this.step3SharingPanel.ResumeLayout(false);
            this.step3SharingPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.footerPanel.ResumeLayout(false);
            this.footerPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel titleBarPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnClose;
        
        private System.Windows.Forms.Panel stepContainerPanel;
        
        private System.Windows.Forms.Panel step1NetworkPanel;
        private System.Windows.Forms.Label lblStep1Title;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.ComboBox ipComboBox;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.NumericUpDown portNumeric;
        private System.Windows.Forms.Label networkDescLabel;
        
        private System.Windows.Forms.Panel step2SourcePanel;
        private System.Windows.Forms.Label lblStep2Title;
        private System.Windows.Forms.ComboBox screenComboBox;
        private System.Windows.Forms.Panel qualityPresetsPanel;
        private System.Windows.Forms.RadioButton radioLow;
        private System.Windows.Forms.RadioButton radioMedium;
        private System.Windows.Forms.RadioButton radioHigh;
        private System.Windows.Forms.RadioButton radioCustom;
        private System.Windows.Forms.Label scaleSliderLabel;
        private System.Windows.Forms.TrackBar scaleTrackBar;
        private System.Windows.Forms.Label fpsSliderLabel;
        private System.Windows.Forms.TrackBar fpsTrackBar;
        private System.Windows.Forms.Label qualitySliderLabel;
        private System.Windows.Forms.TrackBar qualityTrackBar;
        
        private System.Windows.Forms.Panel step3SharingPanel;
        private System.Windows.Forms.Label lblStep3Title;
        private System.Windows.Forms.Label statusIndicator;
        private System.Windows.Forms.Label urlTitleLabel;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Button copyUrlButton;
        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.Label lblClients;
        private System.Windows.Forms.Label lblFps;
        private System.Windows.Forms.Label lblBandwidth;
        
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Label lblAuthor;
    }
}
