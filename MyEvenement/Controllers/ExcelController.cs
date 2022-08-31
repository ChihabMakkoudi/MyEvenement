using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using MyEvenement.Models;
using MyEvenement.Data;
using Microsoft.EntityFrameworkCore;

namespace MyEvenement.Controllers
{
    [Controller]
    [Route("Excel")]
    public class ExcelController : Controller
    {
        public MyEvenementContext _context { get; set; }

        public ExcelController(MyEvenementContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var inscriptions = GetInscriptionList();

            return View(inscriptions);
        }

        [HttpGet]
        [Route("ExportInscription")]
        public IActionResult ExportToExcel()
        {
            // Getting the information from our mimic db
            var inscriptions = _context.Inscription
                .Include(i => i.Evenement)
                .Include(i => i.Statut).ToList();

            // Start exporting to Excel
            var stream = new MemoryStream();

            
            using (var xlPackage = new ExcelPackage(stream))
            {
                // Define a worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Inscriptions");

                // Styling
                var customStyle = xlPackage.Workbook.Styles.CreateNamedStyle("CustomStyle");
                customStyle.Style.Font.UnderLine = true;
                customStyle.Style.Font.Color.SetColor(Color.Red);

                // First row
                var startRow = 5;
                var row = startRow;

                worksheet.Cells["A1"].Value = "Liste des Inscription";
                using (var r = worksheet.Cells["A1:G1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(Color.White);
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.DarkGray);
                }

                worksheet.Cells["A3"].Value = "Nom";
                worksheet.Cells["B3"].Value = "Prenom";
                worksheet.Cells["C3"].Value = "Email";
                worksheet.Cells["D3"].Value = "Telephone";
                worksheet.Cells["E3"].Value = "Adress";
                worksheet.Cells["F3"].Value = "Evenement";
                worksheet.Cells["G3"].Value = "Statut";
                worksheet.Cells["A3:G3"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A3:G3"].Style.Fill.BackgroundColor.SetColor(Color.RoyalBlue);

                row = 4;
                foreach (var inscription in inscriptions)
                {
                    worksheet.Cells[row, 1].Value = inscription.Nom;
                    worksheet.Cells[row, 2].Value = inscription.Prenom;
                    worksheet.Cells[row, 3].Value = inscription.Email;
                    worksheet.Cells[row, 4].Value = inscription.Telephone;
                    worksheet.Cells[row, 5].Value = inscription.Adress;
                    worksheet.Cells[row, 6].Value = inscription.Evenement.Nom;
                    worksheet.Cells[row, 7].Value = inscription.Statut.StatutName;

                    row++;
                }

                xlPackage.Workbook.Properties.Title = "Inscription list";
                xlPackage.Workbook.Properties.Author = "MyEvenementAPP";

                xlPackage.Save();
            }

            stream.Position = 0;
            return File(stream, "application/ms-excel", "inscriptions.xlsx");
            
        }

        [HttpGet]
        public IActionResult BatchInscriptionUpload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BatchInscriptionUpload(IFormFile batchInscriptions)
        {
            if (ModelState.IsValid)
            {
                if (batchInscriptions?.Length > 0)
                {
                    // convert to a stream
                    var stream = batchInscriptions.OpenReadStream();

                    List<Inscription> inscriptions = new List<Inscription>();

                    try
                    {
                        using (var package = new ExcelPackage(stream))
                        {
                            var worksheet = package.Workbook.Worksheets.First();
                            var rowCount = worksheet.Dimension.End.Row;

                            for (var row = 2; row <= rowCount; row++)
                            {
                                //lecture des champs
                                try
                                {
                                    var name = worksheet.Cells[row, 1].Value?.ToString();
                                    var email = worksheet.Cells[row, 2].Value?.ToString();
                                    var phone = worksheet.Cells[row, 3].Value?.ToString();

                                    var inscription = new Inscription()
                                    {
                                        Email = email,
                                        Nom = name,
                                        Telephone = phone
                                    };

                                    //ajout a la base de données
                                    //_context.Add(inscription);
                                    inscriptions.Add(inscription);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                // enregistrement dans la base données
                                // _context.SaveChanges();

                            }
                        }

                        return View("Index", inscriptions);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return View();
        }
    }
}
