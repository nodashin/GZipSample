using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZipCompression
{
    class Program
    {
        /// <summary>
        /// GZIPで圧縮するサンプル
        /// </summary>
        /// <param name="args">未使用</param>
        static void Main(string[] args)
        {
            try
            {
                Console.Write("GZIPに圧縮するファイルのパス：");
                var targetFilePath = Console.ReadLine();

                var gzipFilePath = targetFilePath + ".gz";
                Console.WriteLine("GZIP圧縮後のファイルパスは {0} です。", gzipFilePath);

                if(System.IO.File.Exists(targetFilePath) == false)
                {
                    Console.WriteLine("指定したファイル {0} が見付かりませんでした。", targetFilePath);
                    return;
                }

                //GZIP出力ストリームを生成する。
                var writeFileStream = new System.IO.FileStream(gzipFilePath, System.IO.FileMode.Create);
                //GZIP圧縮ストリームを生成する。
                var compressionStream = new System.IO.Compression.GZipStream(writeFileStream, System.IO.Compression.CompressionMode.Compress);

                using (writeFileStream)
                using (compressionStream)
                {
                    //圧縮対象ファイルをバイト配列で取得する。
                    var fileBytes = System.IO.File.ReadAllBytes(targetFilePath);
                    //圧縮する。
                    compressionStream.Write(fileBytes, 0, fileBytes.Length);
                }

                Console.WriteLine("完了しました！");
            }
            catch (Exception ex)
            {
                Console.WriteLine(Environment.NewLine + ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
