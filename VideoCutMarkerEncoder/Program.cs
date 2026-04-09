using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using VideoCutMarkerEncoder;
using VideoCutMarkerEncoder.Services;

namespace VideoCutMarker.Desktop
{
    static class Program
    {
        /// <summary>
        /// 애플리케이션 메인 진입점
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // 애플리케이션 경로
                string appPath = Application.StartupPath;

                // 필요한 폴더 생성
                CreateRequiredFolders(appPath);

                // FFmpeg 확인
                CheckFFmpeg(appPath);

                // 메인 폼 실행
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"애플리케이션 시작 중 오류가 발생했습니다:\n\n{ex.Message}",
                    "오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// 필요한 폴더 생성
        /// </summary>
        private static void CreateRequiredFolders(string appPath)
        {
            // 필수 폴더 생성
            string[] folders = {
                Path.Combine(appPath, "Share"),
                Path.Combine(appPath, "Output"),
                Path.Combine(appPath, "Config"),
                Path.Combine(appPath, "FFmpeg")
            };

            foreach (string folder in folders)
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }
        }

        /// <summary>
        /// FFmpeg 확인 및 안내
        /// </summary>
        private static void CheckFFmpeg(string appPath)
        {
            string ffmpegPath = Path.Combine(appPath, "FFmpeg", "ffmpeg.exe");

            if (!File.Exists(ffmpegPath))
            {
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
                    $"   {Path.GetDirectoryName(ffmpegPath)}\n\n" +
                    "Restart the application after copying the files.",
                    "FFmpeg Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
    }
}