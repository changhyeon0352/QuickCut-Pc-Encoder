using System;
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
        }


        private void btnFFmpegHelp_Click(object sender, EventArgs e)
        {
            string appPath = Application.StartupPath;
            string ffmpegPath = Path.Combine(appPath, "FFmpeg", "ffmpeg.exe");

            MessageBox.Show(
                "FFmpeg 설치 방법:\n\n" +
                "1. https://ffmpeg.org/download.html 에서 FFmpeg 다운로드\n" +
                "   - Windows 사용자: 'Windows builds from gyan.dev' 링크 클릭\n" +
                "   - 'ffmpeg-release-essentials.zip' 파일 다운로드\n\n" +
                "2. 다운로드한 ZIP 파일 압축 풀기\n\n" +
                "3. bin 폴더 안의 ffmpeg.exe 파일을 찾아\n" +
                $"   {ffmpegPath} 경로에 복사\n\n" +
                "FFmpeg는 GPL 및 LGPL 라이선스 하에 제공됩니다.\n" +
                "자세한 정보: https://ffmpeg.org/legal.html",
                "FFmpeg 설치 안내",
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

                _settingsManager.SaveSettings();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"설정 저장 중 오류가 발생했습니다:\n\n{ex.Message}",
                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                "모든 설정을 기본값으로 초기화하시겠습니까?",
                "설정 초기화",
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
            }
        }

        private void InitializeComponent()
        {
            
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this.btnFFmpegHelp = new System.Windows.Forms.Button();
            this.chkAutoDeleteShareFiles = new System.Windows.Forms.CheckBox(); // ⭐ 새로 추가
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
            this.grpGeneral.Controls.Add(this.chkNotifyOnComplete);
            this.grpGeneral.Controls.Add(this.chkMinimizeToTray);
            this.grpGeneral.Controls.Add(this.txtShareName);
            this.grpGeneral.Controls.Add(this.lblShareName);
            this.grpGeneral.Location = new System.Drawing.Point(12, 12);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(460, 132); // ⭐ 높이 증가
            this.grpGeneral.TabIndex = 0;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "일반 설정";
            // 
            // btnFFmpegHelp
            // 
            this.btnFFmpegHelp.Location = new System.Drawing.Point(375, 29);
            this.btnFFmpegHelp.Name = "btnFFmpegHelp";
            this.btnFFmpegHelp.Size = new System.Drawing.Size(75, 23);
            this.btnFFmpegHelp.TabIndex = 4;
            this.btnFFmpegHelp.Text = "FFmpeg 도움말";
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
            this.chkAutoDeleteShareFiles.Text = "인코딩 완료 후 Share 폴더 파일 자동 삭제";
            this.chkAutoDeleteShareFiles.UseVisualStyleBackColor = true;
            // 
            // chkNotifyOnComplete
            // 
            this.chkNotifyOnComplete.AutoSize = true;
            this.chkNotifyOnComplete.Location = new System.Drawing.Point(179, 72);
            this.chkNotifyOnComplete.Name = "chkNotifyOnComplete";
            this.chkNotifyOnComplete.Size = new System.Drawing.Size(106, 19);
            this.chkNotifyOnComplete.TabIndex = 3;
            this.chkNotifyOnComplete.Text = "완료 시 알림";
            this.chkNotifyOnComplete.UseVisualStyleBackColor = true;
            // 
            // chkMinimizeToTray
            // 
            this.chkMinimizeToTray.AutoSize = true;
            this.chkMinimizeToTray.Location = new System.Drawing.Point(15, 72);
            this.chkMinimizeToTray.Name = "chkMinimizeToTray";
            this.chkMinimizeToTray.Size = new System.Drawing.Size(158, 19);
            this.chkMinimizeToTray.TabIndex = 2;
            this.chkMinimizeToTray.Text = "트레이로 최소화";
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
            this.lblShareName.Text = "SMB 공유 이름:";
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
        private System.Windows.Forms.Button btnFFmpegHelp;
    }
}