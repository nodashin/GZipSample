using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZipDecompression
{
    class Program
    {
        /// <summary>
        /// GZIPを解凍するサンプル
        /// </summary>
        /// <param name="args">未使用</param>
        static void Main(string[] args)
        {
            try
            {
                Console.Write("解凍するGZIPファイルのパス：");
                var targetFilePath = Console.ReadLine();

                if (targetFilePath.EndsWith(".gz") == false)
                {
                    Console.WriteLine("GZIP以外のファイルが指定されています。");
                    return;
                }

                var decompressedFilePath = targetFilePath.Replace(".gz", "");
                Console.WriteLine("GZIP解凍後のファイルパスは {0} です。", decompressedFilePath);

                if (System.IO.File.Exists(targetFilePath) == false)
                {
                    Console.WriteLine("指定したファイル {0} が見付かりませんでした。", targetFilePath);
                    return;
                }

                //解凍対象ファイル読取ストリームを生成する。
                var readFileStream = new System.IO.FileStream(targetFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                //GZIP解凍ストリームを生成する。
                var decompressionStream = new System.IO.Compression.GZipStream(readFileStream, System.IO.Compression.CompressionMode.Decompress);
                //解凍ファイル出力ストリームを生成する。
                var writeFileStream = new System.IO.FileStream(decompressedFilePath, System.IO.FileMode.Create);

                using (readFileStream)
                using (decompressionStream)
                using (writeFileStream)
                {
                    //1KBずつ順次書き出す。
                    var num = 0;
                    var buf = new byte[1024];
                    while((num = decompressionStream.Read(buf, 0 , buf.Length)) > 0)
                    {
                        writeFileStream.Write(buf, 0, num);
                    }

                    //一撃で解凍しようとしても解凍後のByte数が分からないため、例えば下記処理では正しく書き出されない。
                    ////解凍対象ファイルをバイト配列で取得する。
                    //var fileBytes = System.IO.File.ReadAllBytes(targetFilePath);
                    ////解凍対象ファイルのバイト配列分のバイト配列を生成する。
                    //var decompressedBytes = new byte[fileBytes.Length];
                    ////解答する。
                    //var readByteCount = decompressionStream.Read(decompressedBytes, 0, fileBytes.Length);
                    ////解凍したファイルを出力する。
                    //writeFileStream.Write(decompressedBytes, 0, readByteCount);
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
