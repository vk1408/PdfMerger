using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Collections.Generic;
using System;
using System.Linq;

namespace PdfMerger.Core
{
    public static class PdfEditor
    {
        /// <summary>
        /// Merge selected files to output file
        /// </summary>
        public static void MergeFiles(string[] files, string outputFile, Action callback=null)
        {
            PdfDocument outPdf = new PdfDocument();
            foreach (string file in files)
            {
                PdfDocument pdf = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                CopyPages(pdf, outPdf);
                pdf.Close();
            }
            outPdf.Save(outputFile);
            callback?.Invoke();
        }
        

        /// <summary>
        /// Moves selected pages from one file to another. Pages count from 1
        /// </summary>
        public static void MovePages(string sourceFile, string targetFile, IEnumerable<int> pages)
        {
            PdfDocument sourcePdf = PdfReader.Open(sourceFile, PdfDocumentOpenMode.Import);

            PdfDocument targetPdf = new PdfDocument();

            foreach (var pageNum in pages)
            {
                int pageIndex = pageNum - 1;

                if (pageIndex > 0 && pageIndex < sourcePdf.Pages.Count)
                    targetPdf.AddPage(sourcePdf.Pages[pageIndex]);
                else
                    continue;
            }

            targetPdf.Save(targetFile);
            sourcePdf.Close();

        }

        /// <summary>
        /// Split selected sourcePdf document into sections
        /// </summary>
        public static void SplitPdfDocument(PdfDocument pdf, Dictionary<int,IEnumerable<int>> sections) 
        { 
            // for each section create a separate pdf file with given pages from source pdf
        
        }


        private static void CopyPages(PdfDocument from, PdfDocument to)
        {
            for (int i = 0; i < from.PageCount; i++)
            {
                to.AddPage(from.Pages[i]);
            }
        }
    }
}
