using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VideoScheduler.Domain;

namespace VideoScheduler.Core
{
    public class LibraryManager
    {
        private static List<string> vlcSupportedVideoExtensions = new List<string>
        {
            ".mp4", ".mkv", ".avi", ".mov", ".wmv", ".flv", ".mpg", ".mpeg", ".webm",
            ".ogv", ".ogg", ".3gp", ".3g2", ".ts", ".vob", ".asf", ".divx", ".m2ts",
            ".rmvb", ".f4v", ".dvr-ms", ".mxf", ".nsv", ".swf", ".yuv", ".mts", ".fmp4"
        };

        public static VideoLibrary LoadLibrary(string path)
        {
            VideoLibrary library = new VideoLibrary();
            foreach (string folder in System.IO.Directory.GetDirectories(path))
            {
                //get the folder name
                string folderName = System.IO.Path.GetFileName(folder);
                if (string.Equals("Shows", folderName, StringComparison.InvariantCultureIgnoreCase) || string.Equals("TV Shows", folderName, StringComparison.InvariantCultureIgnoreCase))
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
                var showName = System.IO.Path.GetFileName(showFolder);
                TvShow show = new TvShow()
                {
                    Title = showName
                };

                //legacy
                foreach (string showSubFolder in System.IO.Directory.GetDirectories(showFolder))
                {
                    if (isSeasonFormat(System.IO.Path.GetFileName(showSubFolder)))
                    {
                        var seasonFolderName = System.IO.Path.GetFileName(showSubFolder);
                        if (int.TryParse(seasonFolderName.Substring(6), out int seasonNumber)) {

                            TvShowSeason season = new TvShowSeason(show, seasonNumber);
                            foreach (string episodeFile in System.IO.Directory.GetFiles(showSubFolder))
                            {
                                if (!checkExtension(episodeFile))
                                {
                                    continue;
                                }
                                var nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(episodeFile);
                                if (int.TryParse(nameWithoutExtension, out int _episodeNumber))
                                {
                                    TvShowEpisode episode = new TvShowEpisode(season, _episodeNumber, episodeFile);
                                    season.Episodes.Add(episode);
                                }

                            }
                            show.Seasons.Add(season);
                        }
                    }
                }

                scanShowFolders(library, show, showFolder);

                library.Shows.Add(show);
            }
        }

        private static bool checkExtension(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            if (extension == null) return false;
            if (vlcSupportedVideoExtensions.Contains(extension)) return true;
            return false;
        }

        private static void scanShowFolders(VideoLibrary library, TvShow show, string folderpath)
        {
            System.Diagnostics.Debug.WriteLine(folderpath);
            foreach (var subFolder in System.IO.Directory.GetDirectories(folderpath))
            {
                scanShowFolders(library, show, subFolder);
            }

            foreach (var file in System.IO.Directory.GetFiles(folderpath))
            {
                try
                {
                    string nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(file);
                    var match = Regex.Match(nameWithoutExtension, @"s(?<season>\d{1,2})e(?<episode>\d{1,3})", RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        int seasonNumber = int.Parse(match.Groups["season"].Value);
                        int episodeNumber = int.Parse(match.Groups["episode"].Value);

                        var existingSeason = show.Seasons.FirstOrDefault(season => season.SeasonNumber == seasonNumber, null);

                        if (existingSeason == null)
                        {
                            existingSeason = new TvShowSeason(show, seasonNumber);
                            show.Seasons.Add(existingSeason);
                        }

                        var existingEpisode = existingSeason.Episodes.FirstOrDefault(episode => episode.EpisodeNumber == episodeNumber, null);

                        if (existingEpisode == null)
                        {
                            TvShowEpisode episode = new TvShowEpisode(existingSeason, episodeNumber, file);
                            existingSeason.Episodes.Add(episode);
                        }
                    }
                }
                catch
                {
                    continue;
                }
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
