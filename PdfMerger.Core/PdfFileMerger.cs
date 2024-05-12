using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Collections.Generic;
using System;

namespace PdfMerger.Core
{
    public static class PdfFileMerger
    {
        public static void MergePdfFiles(string fileOnePath, string fileTwoPath, string outputFilePath)
        {
            List<string> pdfFiles = new List<string>() { fileOnePath, fileTwoPath };
            PdfDocument outPdf = new PdfDocument();
          
            foreach (string pdfFile in pdfFiles)
            {
                PdfDocument pdf = PdfReader.Open(pdfFile, PdfDocumentOpenMode.Import);
                CopyPages(pdf, outPdf);
                pdf.Close();
            }
            outPdf.Save(outputFilePath);
        }
        public static void CopyPages(PdfDocument from, PdfDocument to)
        {
            for (int i = 0; i < from.PageCount; i++)
            {
                to.AddPage(from.Pages[i]);
            }
        }
    }
}
