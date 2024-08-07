﻿using iTextSharp.text.pdf;
using iTextSharp.text;

namespace EM.Montador
{
    public abstract class MontadorDePdfAbstrato : MontadorDeRelatorio
    {
        protected override void MonteCabecalhoRelatorio()
        {
            PdfPTable cabecalho = new PdfPTable(1)
            {
                WidthPercentage = 100
            };

            cabecalho.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            cabecalho.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cabecalho.DefaultCell.Border = Rectangle.NO_BORDER;
            cabecalho.SpacingAfter = 10;

            cabecalho.AddCell(new PdfPCell(new Phrase("Cabecalho")));
            Documento.Add(cabecalho);
        }

        protected override void MonteRodaeRelatorio()
        {
            PdfPTable rodape = new PdfPTable(1)
            {
                WidthPercentage = 100
            };

            rodape.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            rodape.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            rodape.DefaultCell.Border = Rectangle.NO_BORDER;
            rodape.SpacingBefore = 10;

            rodape.AddCell(new PdfPCell(new Phrase("Rodapé")));
            Documento.Add(rodape);
        }
    }
}
