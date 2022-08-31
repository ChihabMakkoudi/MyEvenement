using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace MyEvenement.Models
{
    public class FileDocument
    {
        public int FileDocumentID { get; set; }
        public string Nom { get ; set;}
        public string ContentType { get ; set;}
        public string Extention { get; set; }
        public long Taille {get; set;}
        public byte[] Fichier { get ; set;}
        public string References { get ; set;}

        public FileDocument()
        {
        }
        public FileDocument(string nom, string contentType, string extention, long taille, byte[] fichier, string references)
        {
            Nom = nom;
            ContentType = contentType;
            Extention = extention;
            Taille = taille;
            Fichier = fichier;
            References = references;
        }
        public FileDocument(IFormFile file)
        {
            this.Nom = file.FileName;
            this.ContentType = file.ContentType;
            this.Extention = file.Name.Split(".")[^1];
            this.Taille = file.Length;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyToAsync(memoryStream);
                this.Fichier = memoryStream.ToArray();
            }
        }
        async public Task Set(IFormFile file)
        {
            
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                this.Fichier = memoryStream.ToArray();
                this.Nom = file.FileName;
                this.ContentType = file.ContentType;
                this.Extention = file.FileName.Split(".")[^1];
                this.Taille = file.Length;
            }
        }
    }
}
