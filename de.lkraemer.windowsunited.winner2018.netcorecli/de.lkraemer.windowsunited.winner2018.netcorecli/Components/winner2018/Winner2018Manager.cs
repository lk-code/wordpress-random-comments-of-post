using de.lkraemer.windowsunited.winner2018.netcorecli.Components.winner2018.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace de.lkraemer.windowsunited.winner2018.netcorecli.Components.winner2018
{
    public class Winner2018Manager
    {
        /// <summary>
        /// Bitte ersetze diese Domain durch die Domain deines Wordpress-Blogs.
        /// </summary>
        const string WORDPRESS_API_URL = "https://DEINE-WORDPRESS-DOMAIN.de/wp-json/wp/v2";

        /// <summary>
        /// Der Konstruktor
        /// </summary>
        public Winner2018Manager()
        {

        }

        #region # public methods #

        /// <summary>
        /// Gibt alle Kommentare des Beitrags zurück.
        /// </summary>
        /// <param name="postId">Die ID des Blog-Beitrags</param>
        public async Task<List<Comment>> LoadCommentsByPost(int postId)
        {
            HttpClient http = new HttpClient();

            string url = "{0}/comments?post={1}&per_page=100";

            url = string.Format(WORDPRESS_API_URL, url, postId);

            HttpResponseMessage response = await http.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();

            List<Comment> posts = JsonConvert.DeserializeObject<List<Comment>>(content);

            return posts;
        }
        
        /// <summary>
        /// Entfernt alle Kommentare mit doppelten Autoren und alle Kommentare die nach dem Enddatum geschrieben wurden
        /// </summary>
        /// <param name="comments"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<Comment> FilterComments(List<Comment> comments, DateTime endDate)
        {
            /**
             * Filtern der Kommentare
             */
            // entfernen der doppelten Kommentare und ungültiger Authoren (Gäste, etc.)
            Dictionary<int, Comment> uniqueComments = new Dictionary<int, Comment>();
            foreach (Comment comment in comments)
            {
                // wenn die Liste eindeutiger Kommentare noch keinen Kommentar
                // von diesem Autor beinhaltet, dann füge diesen hinzu.
                if (!uniqueComments.ContainsKey(comment.author) &&
                    comment.author > 0)
                {
                    uniqueComments.Add(comment.author, comment);
                }
            }

            // Nur Kommentare bis zum 23.12 um 23:59:59 zulassen (Also früher bis zum eingegebenen Datum ;))
            List<Comment> dateValidComments = new List<Comment>();
            foreach (KeyValuePair<int, Comment> comment in uniqueComments)
            {
                if (comment.Value.date < endDate)
                {
                    dateValidComments.Add(comment.Value);
                }
            }

            return dateValidComments;
        }

        /// <summary>
        /// Gibt drei zufällige Einträge aus der Liste mit Kommentaren zurück
        /// </summary>
        /// <param name="comments"></param>
        /// <param name="numberOfWinners"></param>
        /// <returns></returns>
        public List<Comment> GetWinnerFromComments(List<Comment> comments, int numberOfWinners)
        {
            List<Comment> winners = new List<Comment>();
            Random random = new Random();
            winners = comments.OrderBy(x => random.Next()).Take(numberOfWinners).ToList<Comment>();

            return winners;
        }

        #endregion
    }
}