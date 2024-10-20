using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;

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
        /// Split selected sourcePdf document into sections. Section represented with array
        /// of page numbers from the original file 
        /// </summary>
        public static void SplitPdfDocument(string file, IEnumerable<DocumentSection> sections, string outputFolder, Action callback = null)
        {
            PdfDocument pdf = PdfReader.Open(file, PdfDocumentOpenMode.Import);
           
            foreach (var section in sections)
            {
                PdfDocument pdfSection = new PdfDocument();
                CopySelectedPages(pdf, pdfSection, section.Pages.ToArray());
                string outputFilePath = Path.Combine(outputFolder, $"{section.Name}.pdf");
                pdfSection.Save(outputFilePath);
            }
            pdf.Close();
            callback?.Invoke();
        }

        public static int GetPageCount(string file)
        {
            PdfDocument pdf = PdfReader.Open(file, PdfDocumentOpenMode.ReadOnly);
            var pageCount = pdf.PageCount;
            pdf.Close();
            return pageCount;
        }

        /// <summary>
        /// Moves selected pages from one file to another. Pages count from 1
        /// </summary>
        private static void CopySelectedPages(PdfDocument sourcePdf, PdfDocument targetPdf, int[] pages)
        {
            foreach (var pageNum in pages)
            {
                int pageIndex = pageNum - 1;

                if (pageIndex >= 0 && pageIndex < sourcePdf.Pages.Count)
                    targetPdf.AddPage(sourcePdf.Pages[pageIndex]);
                else
                    continue;
            }
        }


        private static void CopyPages(PdfDocument from, PdfDocument to)
        {
            for (int i = 0; i < from.PageCount; i++)
                to.AddPage(from.Pages[i]);
            
        }
    }
}
