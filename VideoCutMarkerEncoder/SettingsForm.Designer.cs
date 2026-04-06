using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using VideoCutMarkerEncoder.Models;

namespace VideoCutMarkerEncoder
{
    public partial class SettingsForm : Form
    {
        private readonly SettingsManager _settingsManager;
        private AppSettings _originalSettings;

        public SettingsForm(SettingsManager settingsManager)
        {
            InitializeComponent();

            _settingsManager = settingsManager;

            // 원본 설정 복사
            _originalSettings = _settingsManager.Settings;

            // UI에 설정 표시
            LoadSettingsToUI();
        }

        private void LoadSettingsToUI()
        {

            // 일반 설정
            txtShareName.Text = _originalSettings.ShareName;
            chkMinimizeToTray.Checked = _originalSettings.MinimizeToTray;
            chkNotifyOnComplete.Checked = _originalSettings.NotifyOnComplete;

            // ⭐ 자동 삭제 설정
            chkAutoDeleteShareFiles.Checked = _originalSettings.AutoDeleteShareFiles;
            chkAutoDeleteSmbSourceFile.Checked = _originalSettings.AutoDeleteSmbSourceFile;
        }


        private void btnFFmpegHelp_Click(object sender, EventArgs e)
        {
            string appPath = Application.StartupPath;
            string ffmpegPath = Path.Combine(appPath, "FFmpeg");

            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.gyan.dev/ffmpeg/builds/",
                UseShellExecute = true
            });
            MessageBox.Show(
                "FFmpeg is required to run this application.\n\n" +
                "How to install:\n\n" +
                "1. Download 'ffmpeg-release-essentials.zip' from the page that just opened\n\n" +
                "2. Extract the ZIP file\n\n" +
                "3. Copy all files from the 'bin' folder to:\n" +
                $"   {ffmpegPath}\n\n" +
                "Restart the application after copying the files.",
                "FFmpeg Not Found",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {

                // 설정 저장
                _settingsManager.Settings.ShareName = txtShareName.Text;
                _settingsManager.Settings.MinimizeToTray = chkMinimizeToTray.Checked;
                _settingsManager.Settings.NotifyOnComplete = chkNotifyOnComplete.Checked;

                // ⭐ 자동 삭제 설정 저장
                _settingsManager.Settings.AutoDeleteShareFiles = chkAutoDeleteShareFiles.Checked;
                _settingsManager.Settings.AutoDeleteSmbSourceFile = chkAutoDeleteSmbSourceFile.Checked;
                _settingsManager.SaveSettings();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save settings:\n\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnResetDefault_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Reset all settings to default?",
                "Reset settings",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // 기본 설정값 (앱 폴더 기준)
                string appFolder = Application.StartupPath;

                txtShareName.Text = "VideoCutMarker";
                chkMinimizeToTray.Checked = false;
                chkNotifyOnComplete.Checked = true;

                // ⭐ 자동 삭제도 기본값으로
                chkAutoDeleteShareFiles.Checked = true;
                chkAutoDeleteSmbSourceFile.Checked = false;
            }
        }

        private void InitializeComponent()
        {
            
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this.btnFFmpegHelp = new System.Windows.Forms.Button();
            this.chkAutoDeleteShareFiles = new System.Windows.Forms.CheckBox(); // ⭐ 새로 추가
            this.chkAutoDeleteSmbSourceFile = new System.Windows.Forms.CheckBox();
            this.chkNotifyOnComplete = new System.Windows.Forms.CheckBox();
            this.chkMinimizeToTray = new System.Windows.Forms.CheckBox();
            this.txtShareName = new System.Windows.Forms.TextBox();
            this.lblShareName = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnResetDefault = new System.Windows.Forms.Button();
            this.grpGeneral.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // grpGeneral
            // 
            this.grpGeneral.Controls.Add(this.btnFFmpegHelp);
            this.grpGeneral.Controls.Add(this.chkAutoDeleteShareFiles); // ⭐ 새로 추가
            this.grpGeneral.Controls.Add(this.chkAutoDeleteSmbSourceFile);
            this.grpGeneral.Controls.Add(this.chkNotifyOnComplete);
            this.grpGeneral.Controls.Add(this.chkMinimizeToTray);
            this.grpGeneral.Controls.Add(this.txtShareName);
            this.grpGeneral.Controls.Add(this.lblShareName);
            this.grpGeneral.Location = new System.Drawing.Point(12, 12);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(460, 155); // ⭐ 높이 증가
            this.grpGeneral.TabIndex = 0;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // btnFFmpegHelp
            // 
            this.btnFFmpegHelp.Location = new System.Drawing.Point(375, 29);
            this.btnFFmpegHelp.Name = "btnFFmpegHelp";
            this.btnFFmpegHelp.Size = new System.Drawing.Size(75, 23);
            this.btnFFmpegHelp.TabIndex = 4;
            this.btnFFmpegHelp.Text = "FFmpeg Guide";
            this.btnFFmpegHelp.UseVisualStyleBackColor = true;
            this.btnFFmpegHelp.Click += new System.EventHandler(this.btnFFmpegHelp_Click);
            // 
            // chkAutoDeleteShareFiles ⭐ 새로 추가된 체크박스
            // 
            this.chkAutoDeleteShareFiles.AutoSize = true;
            this.chkAutoDeleteShareFiles.Location = new System.Drawing.Point(15, 97);
            this.chkAutoDeleteShareFiles.Name = "chkAutoDeleteShareFiles";
            this.chkAutoDeleteShareFiles.Size = new System.Drawing.Size(250, 19);
            this.chkAutoDeleteShareFiles.TabIndex = 5;
            this.chkAutoDeleteShareFiles.Text = "Auto delete files in share folder after encoding";
            this.chkAutoDeleteShareFiles.UseVisualStyleBackColor = true;
            //
            // smb인코딩한 파일 자동 휴지통
            //
            this.chkAutoDeleteSmbSourceFile.AutoSize = true;
            this.chkAutoDeleteSmbSourceFile.Location = new System.Drawing.Point(15, 120);
            this.chkAutoDeleteSmbSourceFile.Name = "chkAutoDeleteSmbSourceFile";
            this.chkAutoDeleteSmbSourceFile.Size = new System.Drawing.Size(250, 19);
            this.chkAutoDeleteSmbSourceFile.TabIndex = 6;
            this.chkAutoDeleteSmbSourceFile.Text = "Auto delete SMB source file after encoding (Recycle Bin)";
            this.chkAutoDeleteSmbSourceFile.UseVisualStyleBackColor = true;
            // 
            // chkNotifyOnComplete
            // 
            this.chkNotifyOnComplete.AutoSize = true;
            this.chkNotifyOnComplete.Location = new System.Drawing.Point(179, 72);
            this.chkNotifyOnComplete.Name = "chkNotifyOnComplete";
            this.chkNotifyOnComplete.Size = new System.Drawing.Size(106, 19);
            this.chkNotifyOnComplete.TabIndex = 3;
            this.chkNotifyOnComplete.Text = "Notify when done";
            this.chkNotifyOnComplete.UseVisualStyleBackColor = true;
            // 
            // chkMinimizeToTray
            // 
            this.chkMinimizeToTray.AutoSize = true;
            this.chkMinimizeToTray.Location = new System.Drawing.Point(15, 72);
            this.chkMinimizeToTray.Name = "chkMinimizeToTray";
            this.chkMinimizeToTray.Size = new System.Drawing.Size(158, 19);
            this.chkMinimizeToTray.TabIndex = 2;
            this.chkMinimizeToTray.Text = "Minimize to tray";
            this.chkMinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // txtShareName
            // 
            this.txtShareName.Location = new System.Drawing.Point(155, 29);
            this.txtShareName.Name = "txtShareName";
            this.txtShareName.Size = new System.Drawing.Size(214, 23);
            this.txtShareName.TabIndex = 1;
            this.txtShareName.ReadOnly = true;
            // 
            // lblShareName
            // 
            this.lblShareName.AutoSize = true;
            this.lblShareName.Location = new System.Drawing.Point(15, 32);
            this.lblShareName.Name = "lblShareName";
            this.lblShareName.Size = new System.Drawing.Size(134, 15);
            this.lblShareName.TabIndex = 0;
            this.lblShareName.Text = "SMB Share Name:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(235, 310); // ⭐ 위치 조정
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(316, 310); // ⭐ 위치 조정
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnResetDefault
            // 
            this.btnResetDefault.Location = new System.Drawing.Point(397, 310); // ⭐ 위치 조정
            this.btnResetDefault.Name = "btnResetDefault";
            this.btnResetDefault.Size = new System.Drawing.Size(75, 23);
            this.btnResetDefault.TabIndex = 4;
            this.btnResetDefault.Text = "Default";
            this.btnResetDefault.UseVisualStyleBackColor = true;
            this.btnResetDefault.Click += new System.EventHandler(this.btnResetDefault_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 345); // ⭐ 폼 높이 증가
            this.Controls.Add(this.btnResetDefault);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpGeneral);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setting";
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox grpGeneral;
        private System.Windows.Forms.Label lblShareName;
        private System.Windows.Forms.TextBox txtShareName;
        
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnResetDefault;
        private System.Windows.Forms.CheckBox chkMinimizeToTray;
        private System.Windows.Forms.CheckBox chkNotifyOnComplete;
        private System.Windows.Forms.CheckBox chkAutoDeleteShareFiles; // ⭐ 새로 추가
        private System.Windows.Forms.CheckBox chkAutoDeleteSmbSourceFile;
        private System.Windows.Forms.Button btnFFmpegHelp;
    }
}