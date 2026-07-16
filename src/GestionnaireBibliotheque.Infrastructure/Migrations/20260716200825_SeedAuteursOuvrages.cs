using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionnaireBibliotheque.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAuteursOuvrages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) Auteurs
            migrationBuilder.Sql(@"
INSERT INTO Auteurs (Nom, Prenom) VALUES
(N'Austen', N'Jane'),
(N'Shakespeare', N'William'),
(N'Hugo', N'Victor'),
(N'Tolstoy', N'Leo'),
(N'Dostoïevski', N'Fyodor'),
(N'Dickens', N'Charles'),
(N'Twain', N'Mark'),
(N'de Balzac', N'Honoré'),
(N'Flaubert', N'Gustave'),
(N'Stendhal', N''),
(N'Christie', N'Agatha'),
(N'Orwell', N'George'),
(N'Tolkien', N'J.R.R.'),
(N'Lewis', N'C.S.'),
(N'Woolf', N'Virginia'),
(N'de Beauvoir', N'Simone'),
(N'Camus', N'Albert'),
(N'Sartre', N'Jean-Paul'),
(N'Duras', N'Marguerite'),
(N'Süskind', N'Patrick'),
(N'García Márquez', N'Gabriel'),
(N'Borges', N'Jorge Luis'),
(N'Cortázar', N'Julio'),
(N'Vargas Llosa', N'Mario'),
(N'Murakami', N'Haruki'),
(N'Allende', N'Isabel'),
(N'Atwood', N'Margaret'),
(N'Morrison', N'Toni'),
(N'Adichie', N'Chimamanda Ngozi'),
(N'King', N'Stephen');
");

            // 2) Ouvrages (liés à leur auteur par Nom + Prénom)
            migrationBuilder.Sql(@"
INSERT INTO Ouvrages (Titre, AuteurId)
SELECT v.Titre, a.Id
FROM (VALUES
 (N'Orgueil et Préjugés', N'Austen', N'Jane'),
 (N'Emma', N'Austen', N'Jane'),
 (N'Raison et Sentiments', N'Austen', N'Jane'),
 (N'Northanger Abbey', N'Austen', N'Jane'),
 (N'Persuasion', N'Austen', N'Jane'),
 (N'Hamlet', N'Shakespeare', N'William'),
 (N'Roméo et Juliet', N'Shakespeare', N'William'),
 (N'Macbeth', N'Shakespeare', N'William'),
 (N'A Midsummer Night''s Dream', N'Shakespeare', N'William'),
 (N'Les Misérables', N'Hugo', N'Victor'),
 (N'Notre-Dame de Paris', N'Hugo', N'Victor'),
 (N'Quatre-vingt-treize', N'Hugo', N'Victor'),
 (N'Guerre et Paix', N'Tolstoy', N'Leo'),
 (N'Anna Karénine', N'Tolstoy', N'Leo'),
 (N'La Mort d''Ivan Ilitch', N'Tolstoy', N'Leo'),
 (N'Crime et Châtiment', N'Dostoïevski', N'Fyodor'),
 (N'Les Frères Karamazov', N'Dostoïevski', N'Fyodor'),
 (N'L''Idiot', N'Dostoïevski', N'Fyodor'),
 (N'Oliver Twist', N'Dickens', N'Charles'),
 (N'David Copperfield', N'Dickens', N'Charles'),
 (N'Un Conte de Noël', N'Dickens', N'Charles'),
 (N'Les Grandes Espérances', N'Dickens', N'Charles'),
 (N'Nicolas Nickleby', N'Dickens', N'Charles'),
 (N'Les Aventures de Tom Sawyer', N'Twain', N'Mark'),
 (N'Les Aventures de Huckleberry Finn', N'Twain', N'Mark'),
 (N'Le Père Goriot', N'de Balzac', N'Honoré'),
 (N'Eugénie Grandet', N'de Balzac', N'Honoré'),
 (N'La Cousine Bette', N'de Balzac', N'Honoré'),
 (N'Illusions Perdues', N'de Balzac', N'Honoré'),
 (N'Madame Bovary', N'Flaubert', N'Gustave'),
 (N'Salammbô', N'Flaubert', N'Gustave'),
 (N'L''Éducation sentimentale', N'Flaubert', N'Gustave'),
 (N'Le Rouge et le Noir', N'Stendhal', N''),
 (N'La Chartreuse de Parme', N'Stendhal', N''),
 (N'Meurtre sur le Nil', N'Christie', N'Agatha'),
 (N'Mort sur le Nil', N'Christie', N'Agatha'),
 (N'10 Petits Nègres', N'Christie', N'Agatha'),
 (N'Le Crime de l''Orient-Express', N'Christie', N'Agatha'),
 (N'La Succession', N'Christie', N'Agatha'),
 (N'Evil Under the Sun', N'Christie', N'Agatha'),
 (N'1984', N'Orwell', N'George'),
 (N'La Ferme des Animaux', N'Orwell', N'George'),
 (N'Le Hobbit', N'Tolkien', N'J.R.R.'),
 (N'La Communauté de l''Anneau', N'Tolkien', N'J.R.R.'),
 (N'Les Deux Tours', N'Tolkien', N'J.R.R.'),
 (N'Le Retour du Roi', N'Tolkien', N'J.R.R.'),
 (N'Le Lion, la Sorcière Blanche et l''Armoire Magique', N'Lewis', N'C.S.'),
 (N'Prince Caspian', N'Lewis', N'C.S.'),
 (N'Le Neveu du Magicien', N'Lewis', N'C.S.'),
 (N'Mrs Dalloway', N'Woolf', N'Virginia'),
 (N'Orlando', N'Woolf', N'Virginia'),
 (N'La Traversée des apparences', N'Woolf', N'Virginia'),
 (N'Le Deuxième Sexe', N'de Beauvoir', N'Simone'),
 (N'Mémoires d''une jeune fille rangée', N'de Beauvoir', N'Simone'),
 (N'L''Étranger', N'Camus', N'Albert'),
 (N'La Peste', N'Camus', N'Albert'),
 (N'Le Mythe de Sisyphe', N'Camus', N'Albert'),
 (N'La Nausée', N'Sartre', N'Jean-Paul'),
 (N'Huis clos', N'Sartre', N'Jean-Paul'),
 (N'Un barrage contre le Pacifique', N'Duras', N'Marguerite'),
 (N'L''Amant', N'Duras', N'Marguerite'),
 (N'Le Ravissement de Lol V. Stein', N'Duras', N'Marguerite'),
 (N'Le Parfum', N'Süskind', N'Patrick'),
 (N'Cent ans de solitude', N'García Márquez', N'Gabriel'),
 (N'L''Amour au temps du choléra', N'García Márquez', N'Gabriel'),
 (N'Ficciones', N'Borges', N'Jorge Luis'),
 (N'El Aleph', N'Borges', N'Jorge Luis'),
 (N'Marelle', N'Cortázar', N'Julio'),
 (N'Les Cronopes et les Fameux', N'Cortázar', N'Julio'),
 (N'La Ville et les Chiens', N'Vargas Llosa', N'Mario'),
 (N'Pantaleón et les Visiteuses', N'Vargas Llosa', N'Mario'),
 (N'Kafka sur le rivage', N'Murakami', N'Haruki'),
 (N'1Q84', N'Murakami', N'Haruki'),
 (N'Norwegian Wood', N'Murakami', N'Haruki'),
 (N'Les Chroniques de l''Oiseau à Ressort', N'Murakami', N'Haruki'),
 (N'La Maison des esprits', N'Allende', N'Isabel'),
 (N'Paula', N'Allende', N'Isabel'),
 (N'Aphrodite', N'Allende', N'Isabel'),
 (N'La Servante écarlate', N'Atwood', N'Margaret'),
 (N'Le Conte de la servante', N'Atwood', N'Margaret'),
 (N'Oryx et Crake', N'Atwood', N'Margaret'),
 (N'Beloved', N'Morrison', N'Toni'),
 (N'Song of Solomon', N'Morrison', N'Toni'),
 (N'Hibiscus bleu', N'Adichie', N'Chimamanda Ngozi'),
 (N'Americanah', N'Adichie', N'Chimamanda Ngozi'),
 (N'Demi d''une certaine affinité', N'Adichie', N'Chimamanda Ngozi'),
 (N'Ça', N'King', N'Stephen'),
 (N'Shining', N'King', N'Stephen'),
 (N'Le Sursaut', N'King', N'Stephen'),
 (N'Misery', N'King', N'Stephen'),
 (N'La Ligne verte', N'King', N'Stephen'),
 (N'La Tour Sombre (série)', N'King', N'Stephen')
) AS v(Titre, Nom, Prenom)
JOIN Auteurs a ON a.Nom = v.Nom AND a.Prenom = v.Prenom;
");

            // 3) Exemplaires : entre 1 et 10 par ouvrage (état Disponible = 0, code-barre généré).
            migrationBuilder.Sql(@"
;WITH Nombres AS (
    SELECT n FROM (VALUES (1),(2),(3),(4),(5),(6),(7),(8),(9),(10)) AS x(n)
)
INSERT INTO Exemplaires (OuvrageId, CodeBarre, EtatDisponibilite)
SELECT o.Id, NEWID(), 0
FROM Ouvrages o
JOIN Nombres num ON num.n <= ((o.Id % 10) + 1)
WHERE NOT EXISTS (SELECT 1 FROM Exemplaires e WHERE e.OuvrageId = o.Id);
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert : retire les exemplaires, ouvrages et auteurs ajoutés par ce seed.
            migrationBuilder.Sql(@"
DELETE FROM Exemplaires;
DELETE FROM Ouvrages;
DELETE FROM Auteurs;
");
        }
    }
}
