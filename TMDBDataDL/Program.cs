using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Net.Http.Headers;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Compression;

namespace MovieDataBaseDataRetrieval
{
    public class ResultsPage
    {
        public int? page { get; set; }
        public int? total_results { get; set; }
        public int? total_pages { get; set; }
        public List<Result> results { get; set; }
    }

    //[Table("BelongsToCollection")]
    public class BelongsToCollection
    {
        [Key]
        public double id { get; set; }
        public string name { get; set; }
        public string poster_path { get; set; }
        public string backdrop_path { get; set; }
        public List<Movie> movies { get; set; }
    }

    //[Table("Genre")]
    public class Genre
    {
        [Key]
        public double id { get; set; }
        public string name { get; set; }
        public List<Movie> movies { get; set; }
    }
    /*
    [Table("MovieGenreLinkTable")]
    public class MovieGenreLinkTable
    {
        [Column(Order = 0), Key, ForeignKey("Movies")]
        public double movieid { get; set; }
        [Column(Order = 1), Key, ForeignKey("Genre")]
        public double genreid { get; set; }
    }
    */
    //[Table("ProductionCompany")]
    public class ProductionCompany
    {
        [Key]
        public double id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
        public List<Movie> movies { get; set; }
    }
    /*
    [Table("MovieProducitonCompanyLinkTable")]
    public class MovieProducitonCompanyLinkTable
    {
        [Column(Order = 0), Key, ForeignKey("Movies")]
        public double movieid { get; set; }
        [Column(Order = 1), Key, ForeignKey("ProductionCompany")]
        public double productioncompanyid { get; set; }
    }
    */
    //[Table("ProductionCountry")]
    public class ProductionCountry
    {
        [Key]
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
        public List<Movie> movies { get; set; }
    }
    /*
    [Table("MovieProducitonCountryLinkTable")]
    public class MovieProducitonCountryLinkTable
    {
        [Column(Order = 0), Key, ForeignKey("Movies")]
        public double movieid { get; set; }
        [Column(Order = 1), Key, ForeignKey("ProductionCountry")]
        public string productioncountryname { get; set; }
    }
    */
    // [Table("SpokenLanguage")]
    public class SpokenLanguage
    {
        [Key]
        public string iso_639_1 { get; set; }
        public string name { get; set; }
        public List<Movie> movies { get; set; }
    }
    /*
    [Table("MovieSpokenLanguageLinkTable")]
    public class MovieSpokenLanguageLinkTable
    {
        [Column(Order = 0), Key, ForeignKey("Movies")]
        public double movieid { get; set; }
        [Column(Order = 1), Key, ForeignKey("SpokenLanguage")]
        public string spokenlanguagename { get; set; }
    }
    */

    //[Table("Movies")]
    public class MovieDataObject
    {
        public bool? adult { get; set; }
        public string backdrop_path { get; set; }
        public BelongsToCollection belongs_to_collection { get; set; }
        public double? budget { get; set; }
        public List<Genre> genres { get; set; }
        public string homepage { get; set; }
        //[Key]
        //[Column(Order = 1)]
        public double id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double? popularity { get; set; }
        public string poster_path { get; set; }
        public List<ProductionCompany> production_companies { get; set; }
        public List<ProductionCountry> production_countries { get; set; }
        public string release_date { get; set; }
        public double? revenue { get; set; }
        public double? runtime { get; set; }
        public List<SpokenLanguage> spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        //[Key]
        //[Column(Order = 2)]
        public string title { get; set; }
        public bool? video { get; set; }
        public double? vote_average { get; set; }
        public double? vote_count { get; set; }

        public Movie ToMovie()
        {
            return new Movie(this);
        }

        /*
        public void PopulateAndSaveContext()
        {
            MoviesContext ctx = new MoviesContext();
            MovieTable movieTable = new MovieTable(this);

            ctx.Movies.Add(movieTable);
            ctx.Genres.AddRange(genres);
            ctx.ProductionCompanys.AddRange(production_companies);
            ctx.ProductionCompanys.AddRange(production_companies);
            ctx.ProductionCompanys.AddRange(production_companies);
            ctx.SaveChanges();

            ctx = new MoviesContext();
            ctx.MovieProductionCountryLinkTables.AddRange((from productionCountry in production_countries select new MovieProducitonCountryLinkTable() { movieid = id, productioncountryname = productionCountry.name }));
            ctx.MovieSpokenLanguagesLinkTables.AddRange((from spokenLanguage in spoken_languages select new MovieSpokenLanguageLinkTable() { movieid = id, spokenlanguagename = spokenLanguage.name }));
            ctx.MovieGenreLinkTables.AddRange((from genre in genres select new MovieGenreLinkTable() { movieid = id, genreid = genre.id }));
            ctx.MovieProductionCompanyLinkTables.AddRange((from productionCompany in production_companies select new MovieProducitonCompanyLinkTable() { movieid = id, productioncompanyid = productionCompany.id }));
            ctx.SaveChanges();
        }
        */
    }

    [Table("Movies")]
    public class Movie
    {
        public Movie(MovieDataObject movie)
        {
            adult = movie.adult;
            backdrop_path = movie.backdrop_path;
            belongs_to_collections = new List<BelongsToCollection>() { };
            if (movie.belongs_to_collection != null)
            {
                belongs_to_collections.Add(movie.belongs_to_collection);
            }

            budget = movie.budget;
            genres = movie.genres;
            homepage = movie.homepage;
            id = movie.id;
            imdb_id = movie.imdb_id;
            original_language = movie.original_language;
            original_title = movie.original_title;
            overview = movie.overview;
            popularity = movie.popularity;
            poster_path = movie.poster_path;
            production_companies = movie.production_companies;
            production_countries = movie.production_countries;
            spoken_languages = movie.spoken_languages;
            release_date = movie.release_date;
            revenue = movie.revenue;
            runtime = movie.runtime;
            status = movie.status;
            tagline = movie.tagline;
            if (movie.title != null)
            {
                title = movie.title;
            }
            else
            {
                title = "NO TITLE PROVIDED";
            }

            video = movie.video;
            vote_average = movie.vote_average;
            vote_count = movie.vote_count;
        }

        public bool? adult { get; set; }
        public string backdrop_path { get; set; }
        public List<BelongsToCollection> belongs_to_collections { get; set; }
        public double? budget { get; set; }
        public List<Genre> genres { get; set; }
        public string homepage { get; set; }
        [Key]
        [Column(Order = 1)]
        public double id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double? popularity { get; set; }
        public string poster_path { get; set; }
        public List<ProductionCompany> production_companies { get; set; }
        public List<ProductionCountry> production_countries { get; set; }
        public string release_date { get; set; }
        public double? revenue { get; set; }
        public double? runtime { get; set; }
        public List<SpokenLanguage> spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        [Key]
        [Column(Order = 2)]
        public string title { get; set; }
        public bool? video { get; set; }
        public double? vote_average { get; set; }
        public double? vote_count { get; set; }
    }

    public class Result
    {
        public double popularity { get; set; }
        public int id { get; set; }
        public bool video { get; set; }
        public int vote_count { get; set; }
        public double vote_average { get; set; }
        public string title { get; set; }
        public string release_date { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public List<int> genre_ids { get; set; }
        public string backdrop_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }
    }

    public class MoviesContext : DbContext
    {
        public MoviesContext() : base()
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ProductionCompany> ProductionCompanys { get; set; }
        public DbSet<ProductionCountry> ProductionCountries { get; set; }
        public DbSet<SpokenLanguage> SpokenLanguages { get; set; }
        public DbSet<BelongsToCollection> BelongsToCollections { get; set; }
        /*public DbSet<MovieGenreLinkTable> MovieGenreLinkTables { get; set; }
        public DbSet<MovieProducitonCompanyLinkTable> MovieProductionCompanyLinkTables { get; set; }
        public DbSet<MovieProducitonCountryLinkTable> MovieProductionCountryLinkTables { get; set; }
        public DbSet<MovieSpokenLanguageLinkTable> MovieSpokenLanguagesLinkTables { get; set; }*/
    }

    class Program
    {
        static HttpClient client = new HttpClient();
        static HttpClient movieClient = new HttpClient();
        //static XmlSerializer xs = new XmlSerializer(typeof(Movie));
        static bool continueAddingMoviesToDb = true;
        static ConcurrentQueue<MovieDataObject> moviesQueue = new ConcurrentQueue<MovieDataObject>();

        /*public static void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        //Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                    }
                }
            }
        }

        static async List<double> GetUpdatedMovieIdsAsync(string path)
        {
            ResultsPage resultsPage;
            HttpResponseMessage response = await client.GetAsync(path);
            List<double> idDeltas;
            if (response.IsSuccessStatusCode)
            {
                Stream streamToReadFrom = await response.Content.ReadAsStreamAsync();

                var fileStream = File.Create("IdDelta.gz");
                streamToReadFrom.Seek(0, SeekOrigin.Begin);
                streamToReadFrom.CopyTo(fileStream);
                fileStream.Close();
                streamToReadFrom.Close();

                idDeltas = new List<double>();
            }
            else
            {
                idDeltas = new List<double>();
            }

            return idDeltas;
        }*/

        static async Task SaveMoviesToDBAsync()
        {
            await Task.Run(() =>
            {
                MoviesContext ctx = new MoviesContext();
                int moviesAdded = 0;
                while (continueAddingMoviesToDb || !moviesQueue.IsEmpty)
                {
                    while (!moviesQueue.IsEmpty || moviesAdded < 1000)
                    {
                        MovieDataObject movieDataObject;
                        if (moviesQueue.TryDequeue(out movieDataObject))
                        {
                            if (movieDataObject.id != 0)
                            {
                                Movie movie = movieDataObject.ToMovie();
                                if (movie.genres != null)
                                {
                                    var newGenres = movie.genres.Where(movieGenre =>
                                        !ctx.Genres.Any(contextGenre => contextGenre.id == movieGenre.id)).ToList();
                                    var newGenreIds = newGenres.Select(newGenre => newGenre.id);
                                    var preexistingGenres = ctx.Genres.Where(contextGenre =>
                                        !newGenreIds.Any(newGenreid => newGenreid == contextGenre.id)).ToList();

                                    newGenres.Union(preexistingGenres).Select(genre =>
                                    {
                                        genre.movies.Add(movie);
                                        return genre;
                                    });

                                    for (int i = 0; i < movie.genres.Count; i++)
                                    {
                                        var movieGenre = movie.genres[i];
                                        foreach (var preexistinggenre in preexistingGenres)
                                        {
                                            if (movieGenre.id == preexistinggenre.id)
                                            {
                                                movie.genres[i] = preexistinggenre;
                                                break;
                                            }
                                        }
                                    }

                                    ctx.Genres.AddRange(newGenres);
                                }

                                if (movie.production_companies != null)
                                {
                                    var newProductionCompanies = movie.production_companies.Where(
                                        movieProductionCompany =>
                                            !ctx.ProductionCompanys.Any(contextProductionCompany =>
                                                contextProductionCompany.id == movieProductionCompany.id)).ToList();
                                    var newProductionCompanyIds =
                                        newProductionCompanies.Select(newProductCompany => newProductCompany.id);
                                    var preexistingProductionCompanies = ctx.ProductionCompanys
                                        .Where(contextProductionCompany =>
                                            !newProductionCompanyIds.Any(newProductionCompanyId =>
                                                newProductionCompanyId == contextProductionCompany.id)).ToList();

                                    newProductionCompanies.Union(preexistingProductionCompanies).Select(
                                        productionCompany =>
                                        {
                                            productionCompany.movies.Add(movie);
                                            return productionCompany;
                                        });

                                    for (int i = 0; i < movie.production_companies.Count; i++)
                                    {
                                        var movieProductionCompany = movie.production_companies[i];
                                        foreach (var preexistingproductioncompany in preexistingProductionCompanies)
                                        {
                                            if (movieProductionCompany.id == preexistingproductioncompany.id)
                                            {
                                                movie.production_companies[i] = preexistingproductioncompany;
                                                break;
                                            }
                                        }
                                    }

                                    ctx.ProductionCompanys.AddRange(newProductionCompanies);
                                }

                                if (movie.production_countries != null)
                                {
                                    var newProductionCountries = movie.production_countries.Where(
                                            movieProductionCountry =>
                                                !ctx.ProductionCountries.Any(contextProductionCountry =>
                                                    contextProductionCountry.iso_3166_1 ==
                                                    movieProductionCountry.iso_3166_1))
                                        .ToList();
                                    var newProductionCountriesIso_3166_1s = newProductionCountries
                                        .Select(newproductionCompany => newproductionCompany.iso_3166_1).ToList();
                                    var preexistingProductionCountries = ctx.ProductionCountries
                                        .Where(contextProductionCountry =>
                                            !newProductionCountriesIso_3166_1s.Any(newProductionCountryIso_3166_1 =>
                                                newProductionCountryIso_3166_1 == contextProductionCountry.iso_3166_1))
                                        .ToList();

                                    newProductionCountries.Union(preexistingProductionCountries).Select(
                                        productionCountry =>
                                        {
                                            productionCountry.movies.Add(movie);
                                            return productionCountry;
                                        });

                                    for (int i = 0; i < movie.production_countries.Count; i++)
                                    {
                                        var movieProductionCountry = movie.production_countries[i];
                                        foreach (var preexistingproductioncountry in preexistingProductionCountries)
                                        {
                                            if (movieProductionCountry.iso_3166_1 ==
                                                preexistingproductioncountry.iso_3166_1)
                                            {
                                                movie.production_countries[i] = preexistingproductioncountry;
                                                break;
                                            }
                                        }
                                    }

                                    ctx.ProductionCountries.AddRange(newProductionCountries);
                                }

                                if (movie.spoken_languages != null)
                                {
                                    var newSpokenLanguages = movie.spoken_languages.Where(movieSpokenLanguage =>
                                        !ctx.SpokenLanguages.Any(contextSpokenLanguage =>
                                            contextSpokenLanguage.iso_639_1 == movieSpokenLanguage.iso_639_1)).ToList();
                                    var newSpokenLanguagesIso_639_1s = newSpokenLanguages
                                        .Select(newSpokenLanguage => newSpokenLanguage.iso_639_1).ToList();
                                    var preexistingSpokenLanguages = ctx.SpokenLanguages.Where(contextSpokenLanguage =>
                                        !newSpokenLanguagesIso_639_1s.Any(newSpokenLanguagesIso_639_1 =>
                                            newSpokenLanguagesIso_639_1 == contextSpokenLanguage.iso_639_1)).ToList();

                                    newSpokenLanguages.Union(preexistingSpokenLanguages).Select(spokenLanguage =>
                                    {
                                        spokenLanguage.movies.Add(movie);
                                        return spokenLanguage;
                                    });

                                    for (int i = 0; i < movie.spoken_languages.Count; i++)
                                    {
                                        var movieSpokenLanguage = movie.spoken_languages[i];
                                        foreach (var preexistingSpokenLanguage in preexistingSpokenLanguages)
                                        {
                                            if (movieSpokenLanguage.iso_639_1 == preexistingSpokenLanguage.iso_639_1)
                                            {
                                                movie.spoken_languages[i] = preexistingSpokenLanguage;
                                                break;
                                            }
                                        }
                                    }

                                    ctx.SpokenLanguages.AddRange(newSpokenLanguages);
                                }

                                if (movie.belongs_to_collections != null)
                                {
                                    var newBelongsToCollections = movie.belongs_to_collections.Where(
                                        movieBelongsToCollection =>
                                            !ctx.BelongsToCollections.Any(contextBelongsToCollection =>
                                                contextBelongsToCollection.id == movieBelongsToCollection.id)).ToList();
                                    var newBelongsToCollectionsIds = newBelongsToCollections
                                        .Select(newBelongsToCollection => newBelongsToCollection.id).ToList();
                                    var preexistingBelongsToCollections = ctx.BelongsToCollections.Where(
                                        contextBelongsToCollection =>
                                            !newBelongsToCollectionsIds.Any(newBelongsToCollectionsId =>
                                                newBelongsToCollectionsId == contextBelongsToCollection.id)).ToList();

                                    newBelongsToCollections.Union(preexistingBelongsToCollections).Select(
                                        belongsToCollection =>
                                        {
                                            belongsToCollection.movies.Add(movie);
                                            return belongsToCollection;
                                        });

                                    for (int i = 0; i < movie.belongs_to_collections.Count; i++)
                                    {
                                        var movieBelongsToCollection = movie.belongs_to_collections[i];
                                        foreach (var preexistingBelongsToCollection in preexistingBelongsToCollections)
                                        {
                                            if (movieBelongsToCollection.id == preexistingBelongsToCollection.id)
                                            {
                                                movie.belongs_to_collections[i] = preexistingBelongsToCollection;
                                                break;
                                            }
                                        }
                                    }

                                    ctx.BelongsToCollections.AddRange(newBelongsToCollections);
                                }
                                //missing.Union(overlap).Select(item => item.updatestring = "Updated").ToList();

                                //Console.Write("Adding movie to context");
                                ctx.Movies.Add(movie);
                                ctx.SaveChanges();
                            }
                        }
                    }

                    //Console.WriteLine("Saving changes to db!!!");
                    // Save context, reset movies added to 0
                }
            });
        }

        static async Task<ResultsPage> GetMovieResultsPageAsync(string path)
        {
            ResultsPage resultsPage;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                resultsPage = await response.Content.ReadAsAsync<ResultsPage>();
            }
            else
            {
                resultsPage = new ResultsPage();
            }
            return resultsPage;
        }

        static async Task<MovieDataObject> GetMovieAsync(string path)
        {
            MovieDataObject movie;
            //Console.WriteLine($"Movie Path: {path}");
            HttpResponseMessage response = await movieClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                movie = await response.Content.ReadAsAsync<MovieDataObject>();
            }
            else
            {
                movie = new MovieDataObject();
            }
            return movie;
        }

        /*static void ShowResultsPage(ResultsPage page)
        {
            Console.WriteLine($"Total Results: {page.total_results}\tTotal Pages: " +
                              $"{page.total_pages}\tPage Num: {page.page}");
        }*/

        static async Task RetrieveWriteMovie(Result res, string apiKey)
        {
            var getMovieString = String.Format("movie/{0}?api_key={1}&language=en-US%27", res.id, apiKey);
            try
            {
                MovieDataObject movie = await GetMovieAsync(getMovieString);

                //Console.WriteLine("Enqueing movie");
                moviesQueue.Enqueue(movie);
                /*foreach (var genre in movie.genres)
                {
                    if (genre.movies == null)
                    {
                        genre.movies = new List<Movie>();
                    }

                    genre.movies.Add(movie);
                }
                

                MoviesContext ctx = new MoviesContext();
                ctx.Movies.Add(movie);
                ctx.SaveChanges();
                */

                //Console.WriteLine(movie.revenue);
                // movie.PopulateAndSaveContext();
                //Console.WriteLine("Saved");
                //Console.WriteLine($"Movie: {movie.title}, ID: {movie.id}");
                /*using (System.IO.FileStream file = System.IO.File.Create("C:\\Temp\\MovieInfo\\" + movie.title + "_" + movie.id + ".xml"))
                {
                    // Serialize the file
                    xs.Serialize(file, movie);
                }*/
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Movie failure: {res.title}");
                Console.WriteLine($"Exception is: {ex.Message}");
            }
        }

        static async Task RunAsync(int page_num, string apiKey)
        {
            try
            {
                var resPageString = String.Format("discover/movie?api_key={0}&page={1}", apiKey, page_num);
                ResultsPage page = await GetMovieResultsPageAsync(resPageString);
                //ShowResultsPage(page);

                //Console.WriteLine($"Num Pages: {page.total_pages}, Page Number: {page.page}");

                //Movie movie = await GetMovieAsync("movie/337167?api_key=493443bebf07f4d8e08d527ca2c84d2a&language=en-US%27");
                /*Parallel.ForEach(page.results, res =>
                    {
                        RetrieveWriteMovie(res).GetAwaiter().GetResult();
                    });
                */
                foreach (var res in page.results)
                {
                    RetrieveWriteMovie(res, apiKey).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Results page failure: {page_num}");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void Main(string[] args)
        {
            string apiKey = args[0];
            /*DbModelBuilder modelBuilder = new DbModelBuilder();
            modelBuilder.Entity<Genre>().HasMany(t);*/

            client.BaseAddress = new Uri("https://api.themoviedb.org/4/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            movieClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
            movieClient.DefaultRequestHeaders.Accept.Clear();
            movieClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            /*
             * Parallel.For(1, 17935, index =>
                {
                    RunAsync(index).GetAwaiter().GetResult();
                });
             */
            var saveMoviesAwaiter = SaveMoviesToDBAsync().GetAwaiter();

            for (int page = 1; page < 17935; page++)
            {
                RunAsync(page, apiKey).GetAwaiter().GetResult();
            }
            //var tasks = Enumerable.Range(1, 17935).Select(i => RunAsync(i).GetAwaiter());

            continueAddingMoviesToDb = false;
            saveMoviesAwaiter.GetResult();
            Console.WriteLine("Finished");
        }
    }
}
