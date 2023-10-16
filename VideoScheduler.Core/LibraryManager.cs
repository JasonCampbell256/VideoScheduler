using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VideoScheduler.Domain;

namespace VideoScheduler.Core
{
    public class LibraryManager
    {
        public static VideoLibrary LoadLibrary(string path)
        {
            VideoLibrary library = new VideoLibrary();
            foreach (string folder in System.IO.Directory.GetDirectories(path))
            {
                //get the folder name
                string folderName = System.IO.Path.GetFileName(folder);
                if (string.Equals("Shows", folderName, StringComparison.InvariantCultureIgnoreCase))
                {
                    parseShows(library, folder);
                }
                else if (string.Equals("Movies", folderName, StringComparison.InvariantCultureIgnoreCase))
                {
                    parseMovies(library, folder);
                }
                else
                {
                    //parseOthers(library, folder);
                    ParseFolder(library, folder, folder);
                }
            }

            return library;
        }

        private static AttributeNode BuildAttributeTree(string rootDir, string currentDir)
        {
            var rootDirName = Path.GetFileName(rootDir);
            var relativePath = currentDir.Substring(rootDir.Length).TrimStart(Path.DirectorySeparatorChar);
            var pathParts = relativePath.Split(Path.DirectorySeparatorChar);

            AttributeNode root = new AttributeNode(rootDirName); // Top-level attribute is just the folder name
            AttributeNode current = root;

            foreach (var part in pathParts)
            {
                if (!String.IsNullOrEmpty(part))
                {
                    current.Child = new AttributeNode(part);
                    current = current.Child;
                }
            }

            return root;
        }

        private static void ParseFolder(VideoLibrary library, string rootDir, string currentDir)
        {
            foreach (string file in Directory.GetFiles(currentDir))
            {
                library.Video.Add(new Video(Path.GetFileName(file), file, BuildAttributeTree(rootDir, currentDir)));
            }

            foreach (string folder in Directory.GetDirectories(currentDir))
            {
                ParseFolder(library, rootDir, folder);
            }
        }

        private static void parseMovies(VideoLibrary library, string moviesFolderPath)
        {
            foreach (string movieFile in System.IO.Directory.GetFiles(moviesFolderPath))
            {
                Movie movie = new Movie()
                {
                    FileName = System.IO.Path.GetFileName(movieFile),
                    FilePath = movieFile
                };
                library.Movies.Add(movie);
            }
        }

        private static void parseShows(VideoLibrary library, string showsFolderPath)
        {
            foreach (string showFolder in System.IO.Directory.GetDirectories(showsFolderPath))
            {
                string showName = System.IO.Path.GetFileName(showFolder);
                TvShow show = new TvShow()
                {
                    Title = showName
                };
                foreach (string seasonFolder in System.IO.Directory.GetDirectories(showFolder))
                {
                    if (!isSeasonFormat(System.IO.Path.GetFileName(seasonFolder)))
                    {
                        continue;
                    }
                    string seasonFolderName = System.IO.Path.GetFileName(seasonFolder);
                    int seasonNumber = int.Parse(seasonFolderName.Substring(6));
                    TvShowSeason season = new TvShowSeason(show, seasonNumber);
                    foreach (string episodeFile in System.IO.Directory.GetFiles(seasonFolder))
                    {
                        string nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(episodeFile);
                        if (int.TryParse(nameWithoutExtension, out int _episodeNumber))
                        {
                            TvShowEpisode episode = new TvShowEpisode(season, _episodeNumber, episodeFile);
                            season.Episodes.Add(episode);
                        }

                    }
                    show.Seasons.Add(season);
                }
                library.Shows.Add(show);
            }
        }

        public static void SaveLibrary(VideoLibrary library)
        {
            //TODO update persistant library
            throw new NotImplementedException();
        }

        private static bool isSeasonFormat(string folderName)
        {
            string pattern = @"^Season \d+$";

            return Regex.IsMatch(folderName, pattern);
        }

        public static bool MatchAttributeTree(AttributeNode tree1, AttributeNode tree2)
        {
            if (tree1 == null || tree2 == null)
            {
                return false;
            }

            if (tree1.Value != tree2.Value)
            {
                return false;
            }

            // Match children recursively if they exist
            if (tree1.Child != null && tree2.Child != null)
            {
                return MatchAttributeTree(tree1.Child, tree2.Child);
            }

            // If one child is null and the other is not, they do not match
            if ((tree1.Child == null && tree2.Child != null) ||
                (tree1.Child != null && tree2.Child == null))
            {
                return false;
            }

            // If both children are null, they match
            return true;
        }
    }
}
