using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMVP
{
    class TrackImporter
    {
        private string s;   //The string extracted from the file.
        private int p = 0;  //A pointer to a character in the string.
        private int size;   //The number of vertices in the matrix being read.
        private int[,] m;   //The matrix containing the track.

        /// <summary>
        /// TrackImporter is used to import tracks stored as .txt files.
        /// </summary>
        /// <param name="file"> The full path of the file (ex. C:\Documents\track.txt). </param>
        public TrackImporter(string file)
        {
            //Read the file:
            System.IO.StreamReader sr = new System.IO.StreamReader(file);
            s = sr.ReadToEnd();
            sr.Close();

            //Get size of the matrix and create an array accordingly:
            size = Convert.ToInt32(getNextWord());
            m = new int[3, size];

            //Extract data from matrix:
            for(int i = 0; i < size; i++)
            {
                m[0, i] = Convert.ToInt32(getNextWord());
                m[1, i] = Convert.ToInt32(getNextWord());
                m[2, i] = Convert.ToInt32(getNextWord());
            }
            
        }
        
        /// <summary>
        /// Returns the next word following the character pointed to by 'p'. This will also advance 'p' to the beginning of the next word.
        /// </summary>
        /// <returns></returns>
        private string getNextWord()
        {
            StringBuilder sb = new StringBuilder();

            while (s[p] != ' ' && s[p] != '\n')
            {
                sb.Append(s[p]);
                p++;
            }

            while ((p < s.Length) && (s[p] == ' ' || s[p] == '\n'))
            {
                p++;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns the extracted matrix as an 3 * X int array, where X is the number of vertices.
        /// </summary>
        /// <returns></returns>
        public int[,] getMatrix()
        {
            return m;
        }
    }
}
