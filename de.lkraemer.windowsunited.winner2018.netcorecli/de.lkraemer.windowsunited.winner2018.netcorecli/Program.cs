using de.lkraemer.windowsunited.winner2018.netcorecli.Components.winner2018;
using de.lkraemer.windowsunited.winner2018.netcorecli.Components.winner2018.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace de.lkraemer.windowsunited.winner2018.netcorecli
{
    class Program
    {
        static void Main(string[] args)
        {
            mainProcess();
        }

        /// <summary>
        /// Der eigentliche Prozess der Anwendung.
        /// </summary>
        private static void mainProcess()
        {
            setCli();

            /**
             * Informationen vom Benutzer bekommen 
             */
            int postId = 0;
            int numberOfWinners = 0;
            DateTime endDate = DateTime.Now;

            postId = getPostIdFromCli();
            if (postId <= 0)
            {
                // Der Benutzer hat keine Eingabe getätigt.
                setErrorCliColor();
                Console.WriteLine("Die Beitrags-ID muss größer als 0 sein");
                setDefaultCliColor();

                if (applicationEndQuestion() == true)
                {
                    return;
                }
                else
                {
                    mainProcess();
                    return;
                }
            }

            numberOfWinners = getNumberOfWinnerFromCli();
            if (numberOfWinners <= 0)
            {
                // Der Benutzer hat keine Eingabe getätigt.
                setErrorCliColor();
                Console.WriteLine("Die Anzahl der Gewinner muss mindestens 1 sein");
                setDefaultCliColor();

                if (applicationEndQuestion() == true)
                {
                    return;
                }
                else
                {
                    mainProcess();
                    return;
                }
            }

            endDate = getEndDateFromCli();

            /**
             * Auslesen der Kommentare und Verarbeitung
             */
            Winner2018Manager winnerManager = new Winner2018Manager();
            List<Comment> comments = winnerManager.LoadCommentsByPost(postId).Result;
            comments = winnerManager.FilterComments(comments, endDate);
            List<Comment> winners = winnerManager.GetWinnerFromComments(comments, numberOfWinners);

            /**
             * Ausgabe der Gewinner :D
             */
            setDefaultCliColor();
            Console.Clear();

            Console.WriteLine("Folgende Community-Mitglieder haben gewonnen:");
            Console.WriteLine(Environment.NewLine);
            foreach(Comment comment in winners)
            {
                Console.WriteLine(string.Format("{0} (Id: {1})", comment.author_name, comment.author) + Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(Environment.NewLine);

            if (applicationEndQuestion() == true)
            {
                return;
            }
            else
            {
                mainProcess();
                return;
            }
        }

        #region # cli-methods #

        /// <summary>
        /// Liefert das End-Datum vom Benutzer als DateTime-Wert.
        /// </summary>
        /// <returns></returns>
        private static DateTime getEndDateFromCli()
        {
            DateTime endTime = DateTime.Now;
            bool success = false;

            Console.Write("Das End-Datum (dd.mm.yyyy): ");
            string endTimeValue = Console.ReadLine();

            try
            {
                endTime = DateTime.ParseExact(endTimeValue, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                success = true;
            }
            catch (ArgumentNullException err)
            {
                // Der Benutzer hat keine Eingabe getätigt.
                setErrorCliColor();
                Console.WriteLine("Es muss etwas eingegeben werden.");
                setDefaultCliColor();
            }
            catch (FormatException err)
            {
                // Der Benutzer hat keine gültige Zahl eingegeben.
                setErrorCliColor();
                Console.WriteLine("Es muss ein gültiges Datum im genannten Format eingegeben werden.");
                setDefaultCliColor();
            }
            catch (Exception err)
            {
                // Irgendein anderer Fehler. just to be sure :)
                setErrorCliColor();
                Console.WriteLine("Unbekannter Fehler: " + err.Message);
                setDefaultCliColor();
            }

            if (success)
            {
                return endTime;
            }
            else
            {
                Console.Write(Environment.NewLine + Environment.NewLine);

                return getEndDateFromCli();
            }
        }

        /// <summary>
        /// Liefert die Beitrags-Id vom Benutzer als Int-Wert.
        /// </summary>
        /// <returns></returns>
        private static int getPostIdFromCli()
        {
            int postId = 0;
            bool success = false;

            Console.Write("Die Post-ID: ");
            string postIdValue = Console.ReadLine();

            try
            {
                postId = Int32.Parse(postIdValue);

                success = true;
            }
            catch (ArgumentNullException err)
            {
                // Der Benutzer hat keine Eingabe getätigt.
                setErrorCliColor();
                Console.WriteLine("Es muss etwas eingegeben werden.");
                setDefaultCliColor();
            }
            catch (FormatException err)
            {
                // Der Benutzer hat keine gültige Zahl eingegeben.
                setErrorCliColor();
                Console.WriteLine("Es muss eine gültige Zahl eingegeben werden.");
                setDefaultCliColor();
            }
            catch (OverflowException err)
            {
                // Die eingegebene Zahl ist zu groß.
                setErrorCliColor();
                Console.WriteLine("Die eingegebene Zahl ist zu groß.");
                setDefaultCliColor();
            }
            catch (Exception err)
            {
                // Irgendein anderer Fehler. just to be sure :)
                setErrorCliColor();
                Console.WriteLine("Unbekannter Fehler: " + err.Message);
                setDefaultCliColor();
            }

            if (success)
            {
                return postId;
            }
            else
            {
                Console.Write(Environment.NewLine + Environment.NewLine);

                return getPostIdFromCli();
            }
        }

        /// <summary>
        /// Liefert die Anzahl der gewünschten Gewinner als Int-Wert.
        /// </summary>
        /// <returns></returns>
        private static int getNumberOfWinnerFromCli()
        {
            int numberOfWinner = 0;
            bool success = false;

            Console.Write("Die Anzahl der Gewinner: ");
            string numberOfWinnerValue = Console.ReadLine();

            try
            {
                numberOfWinner = Int32.Parse(numberOfWinnerValue);

                success = true;
            }
            catch (ArgumentNullException err)
            {
                // Der Benutzer hat keine Eingabe getätigt.
                setErrorCliColor();
                Console.WriteLine("Es muss etwas eingegeben werden.");
                setDefaultCliColor();
            }
            catch (FormatException err)
            {
                // Der Benutzer hat keine gültige Zahl eingegeben.
                setErrorCliColor();
                Console.WriteLine("Es muss eine gültige Zahl eingegeben werden.");
                setDefaultCliColor();
            }
            catch (OverflowException err)
            {
                // Die eingegebene Zahl ist zu groß.
                setErrorCliColor();
                Console.WriteLine("Die eingegebene Zahl ist zu groß.");
                setDefaultCliColor();
            }
            catch (Exception err)
            {
                // Irgendein anderer Fehler. just to be sure :)
                setErrorCliColor();
                Console.WriteLine("Unbekannter Fehler: " + err.Message);
                setDefaultCliColor();
            }

            if (success)
            {
                return numberOfWinner;
            }
            else
            {
                Console.Write(Environment.NewLine + Environment.NewLine);

                return getNumberOfWinnerFromCli();
            }
        }

        #endregion

        #region # helper methods #

        /// <summary>
        /// Fragt den Benutzer, ob dieser das Programm beenden möchte. Gibt true zurück wenn ja.
        /// </summary>
        private static bool applicationEndQuestion()
        {
            setDefaultCliColor();

            Console.Write("Möchten Sie das Programm beenden? (j/n) : ");
            ConsoleKeyInfo endAnswer = Console.ReadKey();

            if(endAnswer.KeyChar == 'j')
            {
                return true;
            } else
            {
                return false;
            }
        }

        /// <summary>
        /// Settzt das CLI zurück.
        /// </summary>
        private static void setCli()
        {
            setDefaultCliColor();
            Console.Clear();
        }

        /// <summary>
        /// Setzt das Standard-Farbschema.
        /// </summary>
        private static void setDefaultCliColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Setzt das Fehler-Farbschema.
        /// </summary>
        private static void setErrorCliColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
        }

        #endregion
    }
}
