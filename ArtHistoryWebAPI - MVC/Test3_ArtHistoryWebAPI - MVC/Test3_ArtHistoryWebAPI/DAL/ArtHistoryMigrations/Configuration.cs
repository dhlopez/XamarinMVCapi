namespace Test3_ArtHistoryWebAPI.DAL.ArtHistoryMigrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;
    using Test3_ArtHistory.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Test3_ArtHistory.DAL.ArtHistoryEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DAL\ArtHistoryMigrations";
        }

        /// Wrapper for SaveChanges adding the Validation Messages to the generated exception
        /// </summary>
        /// <param name="context">The context.</param>
        /// Just replace calls to context.SaveChanges() with SaveChanges(context) in your seed method.
        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
            catch (Exception e)
            {
                throw new Exception(
                     "Seed Failed - errors follow:\n" +
                     e.InnerException.InnerException.Message.ToString(), e
                 ); // Add the original exception as the innerException
            }
        }

        protected override void Seed(Test3_ArtHistory.DAL.ArtHistoryEntities context)
        {
            var movements = new List<Movement>
            {
                new Movement { Name="Early and High Renaissance", Period="1400–1550", Characteristics="Rebirth of the classical ideals from Ancient Rome and Greece." },
                new Movement { Name="Mannerism", Period="1527-1580", Characteristics="Exaggerated or mannered styles of art valuing a personal and idealized response to beauty over the classical ideal of ‘truth to nature.’" },
                new Movement { Name="Baroque", Period="1600–1750", Characteristics="Identified by realistic subjects that depict spectacular action and generate powerful emotions, a reaction against the artificial stylization of Mannerism."  },
                new Movement { Name="Neoclassical", Period="1750–1850", Characteristics="With a focus on historical accuracy, looked back to the art of Antiquity as the model and well served the revolutionary enlightenment of the time." },
                new Movement { Name="Romanticism", Period="1780–1850", Characteristics="Valued the expression of emotion over the control of Classicism." }
            };
            movements.ForEach(d => context.Movements.AddOrUpdate(n => n.Name, d));
            SaveChanges(context);

            var artists = new List<Artist>
            {
                new Artist { Name="Raphael", WholeName="Raffaello Sanzio", Country="Italy", YearOfBirth="1483"},
                new Artist { Name="Leonardo da Vinci", WholeName="Leonardo di ser Piero da Vinci", Country="Italy", YearOfBirth="1452"},
                new Artist { Name="Rembrandt", WholeName="Rembrandt Harmenszoon van Rijn", Country="Netherlands", YearOfBirth="1606"},
                new Artist { Name="Vermeer", WholeName="Johannes Vermeer", Country="Dutch Republic", YearOfBirth="1632"},
                new Artist { Name="David", WholeName="Jacques-Louis David", Country="France", YearOfBirth="1748"},
                new Artist { Name="Caspar Friedrich", WholeName="Caspar David Friedrich", Country="Germany", YearOfBirth="1774"}
            };
            artists.ForEach(d => context.Artists.AddOrUpdate(n => n.Name, d));
            SaveChanges(context);

            var paintings = new List<Painting>
            { 
                new Painting { Name="School of Athens", Dated="1510",
                    MovementID=(context.Movements.Where(d=>d.Name=="Early and High Renaissance").SingleOrDefault().ID),
                    ArtistID=(context.Artists.Where(d=>d.Name=="Raphael").SingleOrDefault().ID)},
                new Painting { Name="Mona Lisa", Dated="1519",
                    MovementID=(context.Movements.Where(d=>d.Name=="Early and High Renaissance").SingleOrDefault().ID),
                    ArtistID=(context.Artists.Where(d=>d.Name=="Leonardo da Vinci").SingleOrDefault().ID)},
                new Painting { Name="The Night Watch", Dated="1510",
                    MovementID=(context.Movements.Where(d=>d.Name=="Baroque").SingleOrDefault().ID),
                    ArtistID=(context.Artists.Where(d=>d.Name=="Rembrandt").SingleOrDefault().ID)},
                new Painting { Name="Girl with a Pearl Earing", Dated="1665",
                    MovementID=(context.Movements.Where(d=>d.Name=="Baroque").SingleOrDefault().ID),
                    ArtistID=(context.Artists.Where(d=>d.Name=="Vermeer").SingleOrDefault().ID)},
                new Painting { Name="Oath of the Horatii", Dated="1784",
                    MovementID=(context.Movements.Where(d=>d.Name=="Neoclassical").SingleOrDefault().ID),
                    ArtistID=(context.Artists.Where(d=>d.Name=="David").SingleOrDefault().ID)},
                new Painting { Name="Wanderer Above the Sea of Fog", Dated="1818",
                    MovementID=(context.Movements.Where(d=>d.Name=="Romanticism").SingleOrDefault().ID),
                    ArtistID=(context.Artists.Where(d=>d.Name=="Caspar Friedrich").SingleOrDefault().ID)}

            };
            paintings.ForEach(d => context.Paintings.AddOrUpdate(n => n.Name, d));
            SaveChanges(context);
        }
    }
}
