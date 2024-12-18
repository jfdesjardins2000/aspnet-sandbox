using System.ComponentModel.DataAnnotations;

namespace NewZealandWalks.API.Models.Contracts
{
    /*
        Dans ASP.NET (en particulier avec ASP.NET Core),
        il est effectivement courant et recommandé de nommer les paramètres des
        méthodes d'action HTTP (comme les méthodes HttpPost)
        avec le suffixe "Contract" ou "Dto" (Data Transfer Object).
        Exemple :
        csharp
        Copy
        [HttpPost]
        public IActionResult Create(UserContract userContract)
        {
            // Logique de création
        }

        [HttpPut]
        public IActionResult Update(UserUpdateContract updateContract)
        {
            // Logique de mise à jour
        }
        Cette convention présente plusieurs avantages :
        1.	Clarifie que l'objet est un contrat/DTO spécifique à l'échange de données
        2.	Distingue clairement les objets de transfert des modèles de domaine
        3.	Indique que l'objet est destiné à la validation et au transfert d'informations
        4.	Facilite la compréhension du code, notamment dans les API
        C'est une pratique recommandée dans les architectures modernes .NET, en particulier avec les approches DDD (Domain-Driven Design) et les architectures propres.
        CopyRetry
        Claude does not have the ability to run the code it generates yet.
     */


    public class RegionCreateContract
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
