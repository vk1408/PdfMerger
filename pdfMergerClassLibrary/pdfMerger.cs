using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Collections.Generic;
using System;

namespace pdfMergerClassLibrary
{
    public class pdfMerger
    {
        public static string fileOnePath { get; set; }
        public static bool fileOneSelected { get; set; }
        public static string fileTwoPath { get; set; }
        public static bool fileTwoSelected { get; set; }
        public static string outputPath { get; set; }
        public static bool outputPathSelected { get; set; }
        public static string mergedFileName { get; set; } = "merged.pdf";
        public static bool mergedFileNameOK { get; set; }   
        public static bool error { get; set; } = false;
        public static string errorMessage="Done! No error";
        public static void mergePdfFiles ()
        {
            List<string> pdfFiles = new List<string>() { fileOnePath,fileTwoPath };
            PdfDocument outPdf = new PdfDocument();
            try
            {
                foreach (string pdfFile in pdfFiles)
                {
                    PdfDocument pdf = PdfReader.Open(pdfFile, PdfDocumentOpenMode.Import);
                    CopyPages(pdf, outPdf);
                }
                outPdf.Save(outputPath + "\\" + mergedFileName);
            }
            catch (Exception e)
            {
                error = true;
                errorMessage = e.Message;
            }
           

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
